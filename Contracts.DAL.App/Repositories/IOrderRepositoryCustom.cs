using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepositoryCustom : IOrderRepositoryCustom<OrderView>
    {
    }
    
    public interface IOrderRepositoryCustom<TOrderView>
    {
        Task<IEnumerable<TOrderView>> GetAllForViewAsync(object? userId = null, bool noTracking = true);
        Task<TOrderView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true);
    }
}