using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Areas Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AreasController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AreaMapper _mapper = new AreaMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public AreasController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Area-s
        /// </summary>
        /// <returns>List of Areas</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.AreaView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.AreaView>>> GetAreas()
        {
            return Ok((await _bll.Areas.GetAllForViewAsync()).Select(e => _mapper.MapAreaView(e)));
        }

        /// <summary>
        /// Get a single Area
        /// </summary>
        /// <param name="id">Area Id</param>
        /// <returns>Area object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Area))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Area>> GetArea(Guid id)
        {
            var area = await _bll.Areas.FirstOrDefaultForViewAsync(id);
            
            if (area == null)
            {
                return NotFound(new {message = "Area not found!"});
            }

            return Ok(_mapper.MapAreaView(area));
        }

        /// <summary>
        /// Update the Area
        /// </summary>
        /// <param name="id">Area id</param>
        /// <param name="area">Area object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutArea(Guid id, V1DTO.Area area)
        {
            if (id != area.Id)
            {
                return BadRequest(new {message = "The id and area.id do not match!"});
            }

            await _bll.Areas.UpdateAsync(_mapper.Map(area));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Area 
        /// </summary>
        /// <param name="area">Area object to create</param>
        /// <returns>Created Area object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Area))]
        public async Task<ActionResult<V1DTO.Area>> PostArea(V1DTO.Area area)
        {
            var bllEntity = _mapper.Map(area);
            _bll.Areas.Add(bllEntity);
            await _bll.SaveChangesAsync();
            area.Id = bllEntity.Id;
            
            return CreatedAtAction("GetArea",
                new { id = area.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                area);
        }

        /// <summary>
        /// Delete an Area
        /// </summary>
        /// <param name="id">Area Id</param>
        /// <returns>Deleted Area object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Area))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Area>> DeleteArea(Guid id)
        {
            var area = await _bll.Areas.FirstOrDefaultAsync(id);
            if (area == null)
            {
                return NotFound(new {message = "Area not found"});
            }

            await _bll.Areas.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(area);
        }
    }
}
