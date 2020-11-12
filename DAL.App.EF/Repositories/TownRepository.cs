using Contracts.DAL.App.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;


namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Town, DAL.App.DTO.Town>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Town, DAL.App.DTO.Town>())
        {
        }
    }
}
