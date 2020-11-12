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
    /// Payment Types Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentTypeMapper _mapper = new PaymentTypeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of all the Payment type-s
        /// </summary>
        /// <returns>List of PaymentTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PaymentType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PaymentType>>> GetPaymentTypes()
        {
            return Ok((await _bll.PaymentTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Payment type
        /// </summary>
        /// <param name="id">PaymentType Id</param>
        /// <returns>PaymentType object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PaymentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            
            if (paymentType == null)
            {
                return NotFound(new {message = "Payment type not found"});
            }

            return Ok(_mapper.Map(paymentType));
        }

        /// <summary>
        /// Update the Payment type
        /// </summary>
        /// <param name="id">PaymentType id</param>
        /// <param name="paymentType">PaymentType object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPaymentType(Guid id, V1DTO.PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest(new {message = "The id and paymentType.id do not match!"});
            }

            await _bll.PaymentTypes.UpdateAsync(_mapper.Map(paymentType), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Payment type 
        /// </summary>
        /// <param name="paymentType">PaymentType object to create</param>
        /// <returns>Created PaymentType object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.PaymentType))]
        public async Task<ActionResult<V1DTO.PaymentType>> PostPaymentType(V1DTO.PaymentType paymentType)
        {
            var bllEntity = _mapper.Map(paymentType);
            _bll.PaymentTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            paymentType.Id = bllEntity.Id;
            
            return CreatedAtAction("GetPaymentType",
                new { id = paymentType.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                paymentType);
        }

        /// <summary>
        /// Delete an Payment type
        /// </summary>
        /// <param name="id">PaymentType Id</param>
        /// <returns>Deleted PaymentType object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.PaymentType))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.PaymentType>> DeletePaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id, User.UserId());
            if (paymentType == null)
            {
                return NotFound(new {message = "Payment type not found"});
            }

            await _bll.PaymentTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(paymentType);
        }
    }
}
