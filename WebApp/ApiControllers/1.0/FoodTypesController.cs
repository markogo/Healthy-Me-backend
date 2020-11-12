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
    /// FoodTypes Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class FoodTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly FoodTypeMapper _mapper = new FoodTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public FoodTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Food type-s
        /// </summary>
        /// <returns>List of FoodTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.FoodType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FoodType>>> GetFoodTypes()
        {
            return Ok((await _bll.FoodTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Food type
        /// </summary>
        /// <param name="id">FoodType Id</param>
        /// <returns>FoodType object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.FoodType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.FoodType>> GetFoodType(Guid id)
        {
            var foodType = await _bll.FoodTypes.FirstOrDefaultAsync(id);
            
            if (foodType == null)
            {
                return NotFound(new {message = "Food Type not found"});
            }

            return Ok(_mapper.Map(foodType));
        }

        /// <summary>
        /// Update the Food type
        /// </summary>
        /// <param name="id">FoodType id</param>
        /// <param name="foodType">FoodType object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutFoodType(Guid id, V1DTO.FoodType foodType)
        {
            if (id != foodType.Id)
            {
                return BadRequest(new {message = "The id and foodType.id do not match!"});
            }

            await _bll.FoodTypes.UpdateAsync(_mapper.Map(foodType));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Food type 
        /// </summary>
        /// <param name="foodType">FoodType object to create</param>
        /// <returns>Created FoodType object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.FoodType))]
        public async Task<ActionResult<V1DTO.FoodType>> PostFoodType(V1DTO.FoodType foodType)
        {
            var bllEntity = _mapper.Map(foodType);
            _bll.FoodTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            foodType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetFoodType",
                new { id = foodType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                foodType);
        }

        /// <summary>
        /// Delete an Food type
        /// </summary>
        /// <param name="id">FoodType Id</param>
        /// <returns>Deleted FoodType object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.FoodType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.FoodType>> DeleteFoodType(Guid id)
        {
            var foodType = await _bll.FoodTypes.FirstOrDefaultAsync(id);
            if (foodType == null)
            {
                return NotFound(new {message = "Food Type not found"});
            }

            await _bll.FoodTypes.RemoveAsync(foodType);
            await _bll.SaveChangesAsync();

            return Ok(foodType);
        }
    }
}
