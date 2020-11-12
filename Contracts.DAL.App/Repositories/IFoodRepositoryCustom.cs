using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IFoodRepositoryCustom : IFoodRepositoryCustom<FoodView>
    {
    }
    
    public interface IFoodRepositoryCustom<TFoodView>
    {
        Task<IEnumerable<TFoodView>> GetAllForViewAsync();
        Task<TFoodView> FirstOrDefaultForViewAsync(Guid id);
    }
}