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
    public class RestaurantService : BaseEntityService<IAppUnitOfWork, IRestaurantRepository, IRestaurantServiceMapper, DAL.App.DTO.Restaurant, BLL.App.DTO.Restaurant>
        , IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork uow) : base(uow, uow.Restaurants, new RestaurantServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<RestaurantView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapRestaurantView(e));
        }
        
        public virtual async Task<RestaurantView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapRestaurantView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}