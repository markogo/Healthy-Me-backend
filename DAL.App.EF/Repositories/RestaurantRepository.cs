﻿using System;
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
    public class RestaurantRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Restaurant, DAL.App.DTO.Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Restaurant, DAL.App.DTO.Restaurant>())
        {
        }
        

        public override async Task<IEnumerable<DAL.App.DTO.Restaurant>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Area)
                .ThenInclude(r => r!.Town);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<DAL.App.DTO.Restaurant> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Area)
                .ThenInclude(r => r!.Town)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<RestaurantView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(a => a.Area)
                .ThenInclude(r => r!.Town)
                .Select(a => new RestaurantView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    OpenedFrom = a.OpenedFrom,
                    ClosedFrom = a.ClosedFrom,
                    Area = a.Area!.Name,
                    Town = a.Area.Town!.Name
                }).ToListAsync();
        }

        public virtual async Task<RestaurantView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(a => a.Area)
                .Where(r => r.Id == id)
                .Select(a => new RestaurantView()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    OpenedFrom = a.OpenedFrom,
                    ClosedFrom = a.ClosedFrom,
                    Area = a.Area!.Name,
                    Town = a.Area.Town!.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}
