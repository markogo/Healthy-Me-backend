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
    public class IngredientService : BaseEntityService<IAppUnitOfWork, IIngredientRepository, IIngredientServiceMapper, DAL.App.DTO.Ingredient,
        BLL.App.DTO.Ingredient>, IIngredientService
    {
        public IngredientService(IAppUnitOfWork uow) : base(uow, uow.Ingredients, new IngredientServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<IngredientView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapIngredientView(e));
        }
        
        public virtual async Task<IngredientView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapIngredientView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}