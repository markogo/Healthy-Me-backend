using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Prices Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PriceMapper _mapper = new PriceMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get a list of all the Price-s
        /// </summary>
        /// <returns>List of Prices</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PriceView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PriceView>>> GetPrices()
        {
            return Ok((await _bll.Prices.GetAllForViewAsync()).Select(e => _mapper.MapPriceView(e)));
        }

        /// <summary>
        /// Get a single Price
        /// </summary>
        /// <param name="id">Price Id</param>
        /// <returns>Price object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultForViewAsync(id);
            
            if (price == null)
            {
                return NotFound(new {message = "Price not found"});
            }

            return Ok(_mapper.MapPriceView(price));
        }

        /// <summary>
        /// Update the Price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="price">Price object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPrice(Guid id, V1DTO.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest(new {message = "The id and price.id do not match!"});
            }

            await _bll.Prices.UpdateAsync(_mapper.Map(price));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Price 
        /// </summary>
        /// <param name="price">Price object to create</param>
        /// <returns>Created Price object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Price))]
        public async Task<ActionResult<V1DTO.Price>> PostPrice(V1DTO.Price price)
        {
            var bllEntity = _mapper.Map(price);
            _bll.Prices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            price.Id = bllEntity.Id;
            
            return CreatedAtAction("GetPrice",
                new { id = price.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                price);
        }

        /// <summary>
        /// Delete an Price
        /// </summary>
        /// <param name="id">Price Id</param>
        /// <returns>Deleted Price object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Price>> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound(new {message = "Price not found"});
            }

            await _bll.Prices.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(price);
        }
    }
}
