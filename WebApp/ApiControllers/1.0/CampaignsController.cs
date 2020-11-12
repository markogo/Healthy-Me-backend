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
    /// Bills Api Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CampaignMapper _mapper = new CampaignMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public CampaignsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get a list of all the Campaign-s
        /// </summary>
        /// <returns>List of Campaigns</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Campaign>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Campaign>>> GetCampaigns()
        {
            return Ok((await _bll.Campaigns.GetAllAsync()).Select(e => _mapper.Map(e)));

        }

        /// <summary>
        /// Get a single Campaign
        /// </summary>
        /// <param name="id">Campaign Id</param>
        /// <returns>Campaign object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Campaign))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Campaign>> GetCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id);

            if (campaign == null)
            {
                return NotFound(new {message = "Campaign not found"});
            }

            return Ok(_mapper.Map(campaign));

        }

        /// <summary>
        /// Update the Campaign
        /// </summary>
        /// <param name="id">Campaign id</param>
        /// <param name="campaign">Campaign object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCampaign(Guid id, V1DTO.Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return BadRequest(new {message = "The id and campaign.id do not match!"});
            }

            await _bll.Campaigns.UpdateAsync(_mapper.Map(campaign));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Campaign 
        /// </summary>
        /// <param name="campaign">Campaign object to create</param>
        /// <returns>Created Campaign object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Campaign))]
        public async Task<ActionResult<V1DTO.Campaign>> PostCampaign(V1DTO.Campaign campaign)
        {
            var bllEntity = _mapper.Map(campaign);
            _bll.Campaigns.Add(bllEntity);
            await _bll.SaveChangesAsync();
            campaign.Id = bllEntity.Id;
            
            return CreatedAtAction("GetCampaign",
                new {id = campaign.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"},
                campaign);
        }

        /// <summary>
        /// Delete a Campaign
        /// </summary>
        /// <param name="id">Campaign Id</param>
        /// <returns>Deleted Campaign object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Campaign))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Campaign>> DeleteCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id);
            if (campaign == null)
            {
                return NotFound(new {message = "Campaign not found!"});
            }

            await _bll.Campaigns.RemoveAsync(campaign);
            await _bll.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
