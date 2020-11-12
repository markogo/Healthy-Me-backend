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
    /// Towns Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class TownsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly TownMapper _mapper = new TownMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public TownsController(IAppBLL bll)
        {
            _bll = bll; 
        }

        /// <summary>
        /// Get a list of all the Town-s
        /// </summary>
        /// <returns>List of Towns</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Town>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Town>>> GetTowns()
        {
            return Ok((await _bll.Towns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Town
        /// </summary>
        /// <param name="id">Town Id</param>
        /// <returns>Town object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Town))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Town>> GetTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id);
            
            if (town == null)
            {
                return NotFound(new {message = "Town not found"});
            }

            return Ok(_mapper.Map(town));
        }

        /// <summary>
        /// Update the Town
        /// </summary>
        /// <param name="id">Town id</param>
        /// <param name="town">Town object</param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTown(Guid id, V1DTO.Town town)
        {
            if (id != town.Id)
            {
                return BadRequest(new {message = "The id and town.id do not match!"});
            }

            await _bll.Towns.UpdateAsync(_mapper.Map(town));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Town 
        /// </summary>
        /// <param name="town">Town object to create</param>
        /// <returns>Created Town object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Town))]
        public async Task<ActionResult<V1DTO.Town>> PostTown(V1DTO.Town town)
        {
            var bllEntity = _mapper.Map(town);
            _bll.Towns.Add(bllEntity);
            await _bll.SaveChangesAsync();
            town.Id = bllEntity.Id;
            
            return CreatedAtAction("GetTown",
                new { id = town.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                town);
        }

        /// <summary>
        /// Delete an Town
        /// </summary>
        /// <param name="id">Town Id</param>
        /// <returns>Deleted Town object</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Town))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<V1DTO.Town>> DeleteTown(Guid id)
        {
            var town = await _bll.Towns.FirstOrDefaultAsync(id);
            if (town == null)
            {
                return NotFound(new {message = "Town not found"});
            }

            await _bll.Towns.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(town);
        }
    }
}
