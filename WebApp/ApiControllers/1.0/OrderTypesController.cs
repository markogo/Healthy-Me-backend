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
    /// OrderTypes Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrderTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderTypeMapper _mapper = new OrderTypeMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Order types-s
        /// </summary>
        /// <returns>List of OrderTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderType>>> GetOrderTypes()
        {
            return Ok((await _bll.OrderTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Order type
        /// </summary>
        /// <param name="id">OrderType Id</param>
        /// <returns>OrderType object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.OrderType>> GetOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id);

            if (orderType == null)
            {
                return NotFound(new {message = "Order Type not found"});
            }

            return Ok(_mapper.Map(orderType));
        }

        /// <summary>
        /// Update the Order type
        /// </summary>
        /// <param name="id">OrderType id</param>
        /// <param name="orderType">OrderType object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOrderType(Guid id, V1DTO.OrderType orderType)
        {
            if (id != orderType.Id)
            {
                return BadRequest(new {message = "The id and orderType.id do not match!"});
            }

            await _bll.OrderTypes.UpdateAsync(_mapper.Map(orderType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Order type 
        /// </summary>
        /// <param name="orderType">OrderType object to create</param>
        /// <returns>Created OrderType object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.OrderType))]
        public async Task<ActionResult<V1DTO.OrderType>> PostOrderType(V1DTO.OrderType orderType)
        {
            var bllEntity = _mapper.Map(orderType);
            _bll.OrderTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            orderType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetOrderType",
                new { id = orderType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                orderType);
        }

        /// <summary>
        /// Delete an Order type
        /// </summary>
        /// <param name="id">OrderType Id</param>
        /// <returns>Deleted OrderType object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.OrderType>> DeleteOrderType(Guid id)
        {
            var orderType = await _bll.OrderTypes.FirstOrDefaultAsync(id);
            if (orderType == null)
            {
                return NotFound(new {message = "Order Type not found"});
            }

            await _bll.OrderTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(orderType);
        }
    }
}
