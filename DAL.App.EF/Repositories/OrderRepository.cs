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
    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Order, DAL.App.DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.Order, DAL.App.DTO.Order>())
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Restaurant)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Include(o => o.PaymentType);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Order> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Restaurant)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Include(o => o.PaymentType)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<OrderView>> GetAllForViewAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            return await query 
                    .Include(o => o.Restaurant)
                    .ThenInclude(o => o!.Area)
                    .ThenInclude(o => o!.Town)
                    .Include(o => o.OrderType)
                    .Include(o => o.PaymentType)
                    .OrderBy(o => o.TimeCreated)
                .Select(a => new OrderView()
                {
                    Id = a.Id,
                    Number = a.Number,
                    OrderStatus = a.OrderStatus,
                    TimeCreated = a.TimeCreated,
                    OrderType = a.OrderType!.Name,
                    Restaurant = a.Restaurant!.Address,
                    Area = a.Restaurant.Area!.Name,
                    Town = a.Restaurant.Area.Town!.Name,
                    Completed = a.Completed,
                    PaymentType = a.PaymentType!.Name
                }).ToListAsync();
        }

        public virtual async Task<OrderView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            return await query
                .Include(o => o.Restaurant)
                .ThenInclude(o => o!.Area)
                .ThenInclude(o => o!.Town)
                .Include(o => o.OrderType)
                .Include(o => o.PaymentType)
                .Where(r => r.Id == id)
                .Select(a => new OrderView()
                {
                    Id = a.Id,
                    Number = a.Number,
                    OrderStatus = a.OrderStatus,
                    TimeCreated = a.TimeCreated,
                    OrderType = a.OrderType!.Name,
                    Restaurant = a.Restaurant!.Name,
                    Area = a.Restaurant.Area!.Name,
                    Town = a.Restaurant.Area.Town!.Name,
                    Completed = a.Completed,
                    PaymentType = a.PaymentType!.Name
                })
                .FirstOrDefaultAsync();
        }
    }
}