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
    public class PriceRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Price, DAL.App.DTO.Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Price, DAL.App.DTO.Price>())
        {
        }

        public override async Task<IEnumerable<Price>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.Campaign);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        
        public override async Task<Price> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.Campaign)
                .Where(p => p.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<PriceView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(a => a.Campaign)
                .Select(a => new PriceView()
                {
                    Id = a.Id,
                    From = a.From,
                    To = a.To,
                    Value = a.Value,
                    Campaign = a.Campaign!.Name,
                }).ToListAsync();
        }

        public virtual async Task<PriceView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(a => a.Campaign)
                .Where(r => r.Id == id)
                .Select(a => new PriceView()
                {
                    Id = a.Id,
                    From = a.From,
                    To = a.To,
                    Value = a.Value,
                    Campaign = a.Campaign!.Name,
                })
                .FirstOrDefaultAsync();
        }
    }
}