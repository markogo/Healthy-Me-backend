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
    /// OrderItems Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class OrderItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OrderItemMapper _mapper = new OrderItemMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderItemsController(IAppBLL bll)
        {
            _bll = bll; 
        }

        /// <summary>
        /// Get a list of all the Order Item-s
        /// </summary>
        /// <returns>List of OrderItems</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OrderItemView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.OrderItemView>>> GetOrderItems(Guid? orderId)
        {
            if (orderId == null)
            {
                return Ok((await _bll.OrderItems.GetAllForViewAsync(orderId ,User.UserId())).Select(e => _mapper.MapOrderItemView(e)));
            }
            
            if (!await _bll.Orders.ExistsAsync(orderId!.Value))
            {
                return NotFound(new {message = $"Order with id {orderId} was not found"});
            }
            
            return Ok((await _bll.OrderItems.GetAllForViewAsync(orderId ,User.UserId())).Select(e => _mapper.MapOrderItemView(e)));
        }

        /// <summary>
        /// Get a single Order Item
        /// </summary>
        /// <param name="id">Order Item Id</param>
        /// <returns>Order Item object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.OrderItem>> GetOrderItem(Guid id)
        {
            var orderItem = await _bll.OrderItems.FirstOrDefaultForViewAsync(id, User.UserId());
            
            if (orderItem == null)
            {
                return NotFound(new {message = "Order Item not found"});
            }

            return Ok(_mapper.MapOrderItemView(orderItem));
        }

        /// <summary>
        /// Update the Order Item
        /// </summary>
        /// <param name="id">Order Item id</param>
        /// <param name="orderItem">Order Item object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutOrderItem(Guid id, V1DTO.OrderItem orderItem)
        {
            orderItem.AppUserId = User.UserId();
            
            if (id != orderItem.Id)
            {
                return BadRequest(new {message = "The id and orderItem.id do not match!"});
            }

            await _bll.OrderItems.UpdateAsync(_mapper.Map(orderItem), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Order Item 
        /// </summary>
        /// <param name="orderItem">Order Item object to create</param>
        /// <returns>Created Order Item object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.OrderItem))]
        public async Task<ActionResult<V1DTO.OrderItem>> PostOrderItem(V1DTO.OrderItem orderItem)
        {
            orderItem.AppUserId = User.UserId();
            
            var bllEntity = _mapper.Map(orderItem);
            
            _bll.OrderItems.Add(bllEntity);
            
            await _bll.SaveChangesAsync();

            orderItem.Id = bllEntity.Id;
            
            return CreatedAtAction("GetOrderItem",
                new { id = orderItem.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                orderItem);
        }

        /// <summary>
        /// Delete an Order Item
        /// </summary>
        /// <param name="id">Order Item Id</param>
        /// <returns>Deleted Order Item object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.OrderItem>> DeleteOrderItem(Guid id)
        {
            var orderItem = await _bll.OrderItems.FirstOrDefaultAsync(id, User.UserId());
            if (orderItem == null)
            {
                return NotFound(new {message = "Order Item not found"});
            }

            await _bll.OrderItems.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(orderItem);
        }
        
        /// <summary>
        /// Delete all Order Items
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OrderItem))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.OrderItem>> DeleteAllOrderItems()
        {
            var orderItems = await _bll.OrderItems.GetAllAsync(User.UserId());

            foreach (var orderItem in orderItems)
            {
                await _bll.OrderItems.RemoveAsync(orderItem.Id);
            }
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
