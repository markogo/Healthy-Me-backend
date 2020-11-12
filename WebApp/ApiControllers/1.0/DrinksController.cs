using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Drinks Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class DrinksController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly DrinkMapper _mapper = new DrinkMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public DrinksController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Drink-s
        /// </summary>
        /// <returns>List of Drinks</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.DrinkView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.DrinkView>>> GetDrinks()
        {
            return Ok((await _bll.Drinks.GetAllForViewAsync()).Select(e => _mapper.MapDrinkView(e)));
        }

        /// <summary>
        /// Get a single Drink
        /// </summary>
        /// <param name="id">Drink Id</param>
        /// <returns>Drink object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Drink))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Drink>> GetDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultForViewAsync(id);
            
            if (drink == null)
            {
                return NotFound(new {message = "Drink not found"});
            }

            return Ok(_mapper.MapDrinkView(drink));
        }

        /// <summary>
        /// Update the Drink
        /// </summary>
        /// <param name="id">Drink id</param>
        /// <param name="drink">Drink object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDrink(Guid id, V1DTO.Drink drink)
        {
            if (id != drink.Id)
            {
                return BadRequest(new {message = "The id and drink.id do not match!"});
            }

            await _bll.Drinks.UpdateAsync(_mapper.Map(drink));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Drink 
        /// </summary>
        /// <param name="drink">Drink object to create</param>
        /// <returns>Created Drink object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Drink))]
        public async Task<ActionResult<V1DTO.Drink>> PostDrink(V1DTO.Drink drink)
        {
            var bllEntity = _mapper.Map(drink);
            _bll.Drinks.Add(bllEntity);
            await _bll.SaveChangesAsync();
            drink.Id = bllEntity.Id;
            
            return CreatedAtAction("GetDrink",
                new { id = drink.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                drink);
        }

        /// <summary>
        /// Delete a Drink
        /// </summary>
        /// <param name="id">Drink Id</param>
        /// <returns>Deleted Drink object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Drink))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Drink>> DeleteDrink(Guid id)
        {
            var drink = await _bll.Drinks.FirstOrDefaultAsync(id);
            if (drink == null)
            {
                return NotFound( new {message = "Drink not found"});
            }

            await _bll.Drinks.RemoveAsync(drink);
            await _bll.SaveChangesAsync();

            return Ok(drink);
        }
    }
}
