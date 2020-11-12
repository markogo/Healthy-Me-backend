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
    public class AreaRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Area, DAL.App.DTO.Area>, IAreaRepository
    {
        public AreaRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Area, DAL.App.DTO.Area>())
        {
        }

        public override async Task<IEnumerable<Area>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Town);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Area> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(a => a.Town)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<AreaView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(a => a.Town)
                .Select(a => new AreaView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Town = a.Town!.Name,
                }).ToListAsync();
        }

        public virtual async Task<AreaView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(a => a.Town)
                .Where(r => r.Id == id)
                .Select(a => new AreaView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Town = a.Town!.Name,
                })
                .FirstOrDefaultAsync();
        }
    }
}