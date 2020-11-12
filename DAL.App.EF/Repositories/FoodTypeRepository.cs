using Contracts.DAL.App.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class FoodTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.FoodType, DAL.App.DTO.FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.FoodType, DAL.App.DTO.FoodType>())
        {
        }
    }
}