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
    /// Foods Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class FoodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly FoodMapper _mapper = new FoodMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public FoodsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Food-s
        /// </summary>
        /// <returns>List of Foods</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FoodView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FoodView>>> GetFoods()
        {
            return Ok((await _bll.Foods.GetAllForViewAsync()).Select(e => _mapper.MapFoodView(e)));
        }

        /// <summary>
        /// Get a single Food
        /// </summary>
        /// <param name="id">Food Id</param>
        /// <returns>Food object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Food))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Food>> GetFood(Guid id)
        {
            var food = await _bll.Foods.FirstOrDefaultForViewAsync(id);
            
            if (food == null)
            {
                return NotFound(new {message = "Food not found"});
            }

            return Ok(_mapper.MapFoodView(food));
        }

        /// <summary>
        /// Update the Food
        /// </summary>
        /// <param name="id">Food id</param>
        /// <param name="food">Food object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutFood(Guid id, V1DTO.Food food)
        {
            if (id != food.Id)
            {
                return BadRequest(new {message = "The id and food.id do not match!"});
            }

            await _bll.Foods.UpdateAsync(_mapper.Map(food));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Food 
        /// </summary>
        /// <param name="food">Food object to create</param>
        /// <returns>Created Food object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Food))]
        public async Task<ActionResult<V1DTO.Food>> PostFood(V1DTO.Food food)
        {
            var bllEntity = _mapper.Map(food);
            _bll.Foods.Add(bllEntity);
            await _bll.SaveChangesAsync();
            food.Id = bllEntity.Id;
            
            return CreatedAtAction("GetFood",
                new { id = food.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                food);
        }

        /// <summary>
        /// Delete a Food
        /// </summary>
        /// <param name="id">Food Id</param>
        /// <returns>Deleted Food object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Food))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Food>> DeleteFood(Guid id)
        {
            var food = await _bll.Foods.FirstOrDefaultAsync(id);
            if (food == null)
            {
                return NotFound(new {message = "Food not found"});
            }

            await _bll.Foods.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(food);
        }
    }
}
