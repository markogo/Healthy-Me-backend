using Contracts.DAL.App.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class OrderTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.OrderType, DAL.App.DTO.OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.OrderType, DAL.App.DTO.OrderType>())
        {
        }
    }
}