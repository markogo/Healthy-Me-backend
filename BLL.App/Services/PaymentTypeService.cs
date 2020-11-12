using BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PaymentTypeService : BaseEntityService<IAppUnitOfWork, IPaymentTypeRepository, IPaymentTypeServiceMapper, DAL.App.DTO.PaymentType,
        BLL.App.DTO.PaymentType>, IPaymentTypeService
    {
        public PaymentTypeService(IAppUnitOfWork uow) : base(uow, uow.PaymentTypes, new PaymentTypeServiceMapper())
        {
        }
    }
}