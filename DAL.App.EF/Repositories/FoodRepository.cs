using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Food, DAL.App.DTO.Food>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Food, DAL.App.DTO.Food>())
        {
        }

        public override async Task<IEnumerable<Food>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(f => f.FoodType)
                .Include(f => f.Price);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Food> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(f => f.FoodType)
                .Include(f => f.Price)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<FoodView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(f => f.FoodType)
                .Include(f => f.Price)
                .Select(a => new FoodView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Amount = a.Amount,
                    Description = a.Description,
                    FoodType = a.FoodType!.Name,
                    Price = a.Price!.Value,
                }).ToListAsync();
        }

        public virtual async Task<FoodView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(f => f.FoodType)
                .Include(f => f.Price)
                .Where(r => r.Id == id)
                .Select(a => new FoodView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Amount = a.Amount,
                    Description = a.Description,
                    FoodType = a.FoodType!.Name,
                    Price = a.Price!.Value,
                })
                .FirstOrDefaultAsync();
        }
    }
}