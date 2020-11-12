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
    public class IngredientRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Ingredient, DAL.App.DTO.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Ingredient, DAL.App.DTO.Ingredient>())
        {
        }

        public override async Task<IEnumerable<Ingredient>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Food)
                .Include(i => i.Price);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Ingredient> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Food)
                .Include(i => i.Price);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<IngredientView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(i => i.Food)
                .Include(i => i.Price)
                .Select(a => new IngredientView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Food = a.Food!.Name,
                    Price = a.Price!.Value,
                }).ToListAsync();
        }

        public virtual async Task<IngredientView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(i => i.Food)
                .Include(i => i.Price)
                .Where(r => r.Id == id)
                .Select(a => new IngredientView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Food = a.Food!.Name,
                    Price = a.Price!.Value,
                })
                .FirstOrDefaultAsync();
        }
    }
}