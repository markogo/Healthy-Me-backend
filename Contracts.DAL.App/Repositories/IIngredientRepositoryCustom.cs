using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IIngredientRepositoryCustom : IIngredientRepositoryCustom<IngredientView>
    {
    }
    
    public interface IIngredientRepositoryCustom<TIngredientView>
    {
        Task<IEnumerable<TIngredientView>> GetAllForViewAsync();
        Task<TIngredientView> FirstOrDefaultForViewAsync(Guid id);
    }
}