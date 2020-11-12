using BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OrderTypeService : BaseEntityService<IAppUnitOfWork, IOrderTypeRepository, IOrderTypeServiceMapper, DAL.App.DTO.OrderType,
        BLL.App.DTO.OrderType>, IOrderTypeService
    {
        public OrderTypeService(IAppUnitOfWork uow) : base(uow, uow.OrderTypes, new OrderTypeServiceMapper())
        {
        }
    }
}