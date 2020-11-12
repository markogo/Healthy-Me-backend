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
    public class DrinkService : BaseEntityService<IAppUnitOfWork, IDrinkRepository, IDrinkServiceMapper, DAL.App.DTO.Drink,
        BLL.App.DTO.Drink>, IDrinkService
    {
        public DrinkService(IAppUnitOfWork uow) : base(uow, uow.Drinks, new DrinkServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<DrinkView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapDrinkView(e));
        }
        
        public virtual async Task<DrinkView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapDrinkView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}