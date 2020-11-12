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
    /// Orders Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderMapper _mapper = new OrderMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OrdersController(IAppBLL bll)
        {
            _bll = bll; 
        }

        /// <summary>
        /// Get a list of all the Order-s
        /// </summary>
        /// <returns>List of Orders</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderView>>> GetOrders()
        {
            return Ok((await _bll.Orders.GetAllForViewAsync(User.UserId())).Select(e => _mapper.MapOrderView(e)));
        }

        /// <summary>
        /// Get a single Order
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Order object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Order>> GetOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id, User.UserId());
            
            if (order == null)
            {
                return NotFound(new {message = "Order not found"});
            }

            return Ok(_mapper.Map(order));
        }

        /// <summary>
        /// Update the Order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="order">Order object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOrder(Guid id, V1DTO.Order order)
        {
            order.AppUserId = User.UserId();
            
            if (id != order.Id)
            {
                return BadRequest(new {message = "The id and order.id do not match!"});
            }

            await _bll.Orders.UpdateAsync(_mapper.Map(order), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Order 
        /// </summary>
        /// <param name="order">Order object to create</param>
        /// <returns>Created Order object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Order))]
        public async Task<ActionResult<V1DTO.Order>> PostOrder(V1DTO.Order order)
        {
            order.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(order);
            await _bll.Orders.AddNewOrder(bllEntity);
            await _bll.SaveChangesAsync();
            order.Id = bllEntity.Id;
            
            return CreatedAtAction("GetOrder",
                new { id = order.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                order);
        }

        /// <summary>
        /// Delete an Order with all its OrderItems
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <returns>Deleted Order object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Order))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Order>> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id, User.UserId());
            if (order == null)
            {
                return NotFound(new {message = "Order not found"});
            }

            await _bll.Orders.RemoveAsync(order, User.UserId());
            await _bll.SaveChangesAsync();

            return Ok(order);
        }
    }
}
