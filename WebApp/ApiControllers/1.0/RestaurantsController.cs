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
    /// Restaurants Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class RestaurantsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RestaurantMapper _mapper = new RestaurantMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public RestaurantsController(IAppBLL bll)
        {
            _bll = bll; 
        }

        /// <summary>
        /// Get a list of all the Restaurant-s
        /// </summary>
        /// <returns>List of Restaurants</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RestaurantView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.RestaurantView>>> GetRestaurants()
        {
            return Ok((await _bll.Restaurants.GetAllForViewAsync()).Select(e => _mapper.MapRestaurantView(e)));
        }

        /// <summary>
        /// Get a single Restaurant
        /// </summary>
        /// <param name="id">Restaurant Id</param>
        /// <returns>Restaurant object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Restaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Restaurant>> GetRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultForViewAsync(id);
            
            if (restaurant == null)
            {
                return NotFound(new {message = "Restaurant not found"});
            }

            return Ok(_mapper.MapRestaurantView(restaurant));
        }

        /// <summary>
        /// Update the Restaurant
        /// </summary>
        /// <param name="id">Restaurant id</param>
        /// <param name="restaurant">Restaurant object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRestaurant(Guid id, V1DTO.Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest(new {message = "The id and restaurant.id do not match!"});
            }

            await _bll.Restaurants.UpdateAsync(_mapper.Map(restaurant));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Restaurant 
        /// </summary>
        /// <param name="restaurant">Restaurant object to create</param>
        /// <returns>Created Restaurant object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Restaurant))]
        public async Task<ActionResult<V1DTO.Restaurant>> PostRestaurant(V1DTO.Restaurant restaurant)
        {
            var bllEntity = _mapper.Map(restaurant);
            _bll.Restaurants.Add(bllEntity);
            await _bll.SaveChangesAsync();
            restaurant.Id = bllEntity.Id;
            
            return CreatedAtAction("GetRestaurant",
                new { id = restaurant.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                restaurant);
        }

        /// <summary>
        /// Delete a Restaurant
        /// </summary>
        /// <param name="id">Restaurant Id</param>
        /// <returns>Deleted Restaurant object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Restaurant))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Restaurant>> DeleteRestaurant(Guid id)
        {
            var restaurant = await _bll.Restaurants.FirstOrDefaultAsync(id);
            if (restaurant == null)
            {
                return NotFound(new {message = "Restaurant not found"});
            }

            await _bll.Restaurants.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(restaurant);
        }
    }
}
