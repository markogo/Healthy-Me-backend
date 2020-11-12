using BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class FoodTypeService : BaseEntityService<IAppUnitOfWork, IFoodTypeRepository, IFoodTypeServiceMapper, DAL.App.DTO.FoodType,
        BLL.App.DTO.FoodType>, IFoodTypeService
    {
        public FoodTypeService(IAppUnitOfWork uow) : base(uow, uow.FoodTypes, new FoodTypeServiceMapper())
        {
        }
    }
}