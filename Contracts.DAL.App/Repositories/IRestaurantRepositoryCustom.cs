using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRestaurantRepositoryCustom : IRestaurantRepositoryCustom<RestaurantView>
    {
    }
    
    public interface IRestaurantRepositoryCustom<TRestaurantView>
    {
        Task<IEnumerable<TRestaurantView>> GetAllForViewAsync();
        Task<TRestaurantView> FirstOrDefaultForViewAsync(Guid id);
    }
}