using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderItemRepositoryCustom : IOrderItemRepositoryCustom<OrderItemView>
    {
    }
    
    public interface IOrderItemRepositoryCustom<TOrderItemView>
    {
        Task<IEnumerable<TOrderItemView>> GetAllForViewAsync(Guid? orderId, object? userId = null, bool noTracking = true);
        Task<TOrderItemView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true);
    }
}