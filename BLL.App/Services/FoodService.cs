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
    public class FoodService : BaseEntityService<IAppUnitOfWork, IFoodRepository, IFoodServiceMapper, DAL.App.DTO.Food,
        BLL.App.DTO.Food>, IFoodService
    {
        public FoodService(IAppUnitOfWork uow) : base(uow, uow.Foods, new FoodServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<FoodView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapFoodView(e));
        }
        
        public virtual async Task<FoodView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapFoodView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}