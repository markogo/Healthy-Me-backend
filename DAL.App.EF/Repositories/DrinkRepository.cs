using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class DrinkRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Drink, DAL.App.DTO.Drink>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Drink, DAL.App.DTO.Drink>())
        {
        }
        
        public override async Task<IEnumerable<Drink>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.Price);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Drink> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.Price)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<DrinkView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(c => c.Price)
                .Select(a => new DrinkView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Amount = a.Amount,
                    Price = a.Price!.Value,
                }).ToListAsync();
        }

        public virtual async Task<DrinkView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(c => c.Price)
                .Where(r => r.Id == id)
                .Select(a => new DrinkView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Size = a.Size,
                    Amount = a.Amount,
                    Price = a.Price!.Value,
                })
                .FirstOrDefaultAsync();
        }
    }
}