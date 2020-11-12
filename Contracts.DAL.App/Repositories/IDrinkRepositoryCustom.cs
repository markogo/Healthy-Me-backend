using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDrinkRepositoryCustom : IDrinkRepositoryCustom<DrinkView>
    {
    }
    
    public interface IDrinkRepositoryCustom<TDrinkView>
    {
        Task<IEnumerable<TDrinkView>> GetAllForViewAsync();
        Task<TDrinkView> FirstOrDefaultForViewAsync(Guid id);
    }
}