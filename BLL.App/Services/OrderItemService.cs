using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderItemService : BaseEntityService<IAppUnitOfWork, IOrderItemRepository, IOrderItemServiceMapper, DAL.App.DTO.OrderItem,
        BLL.App.DTO.OrderItem>, IOrderItemService
    {
        public OrderItemService(IAppUnitOfWork uow) : base(uow, uow.OrderItems, new OrderItemServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<OrderItemView>> GetAllForViewAsync(Guid? orderId, object? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllForViewAsync(orderId, userId, noTracking)).Select(e => Mapper.MapOrderItemView(e));
        }
        
        public virtual async Task<OrderItemView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            return Mapper.MapOrderItemView(await Repository.FirstOrDefaultForViewAsync(id, userId));
        }
    }
}