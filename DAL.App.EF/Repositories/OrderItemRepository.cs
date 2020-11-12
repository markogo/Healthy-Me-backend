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
    public class OrderItemRepository :
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.OrderItem, DAL.App.DTO.OrderItem>,
        IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.App.OrderItem, DAL.App.DTO.OrderItem>())
        {
        }

        public override async Task<IEnumerable<OrderItem>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.AppUser)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<OrderItem> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.AppUser)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        public virtual async Task<IEnumerable<OrderItemView>> GetAllForViewAsync(Guid? orderId, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);

            if (orderId == null)
            {
                return await query
                    .Include(o => o.Food)
                    .ThenInclude(o => o!.Price)
                    .Include(o => o.Ingredient)
                    .ThenInclude(o => o!.Price)
                    .Include(o => o.Order)
                    .ThenInclude(o => o!.PaymentType)
                    .Include(o => o.Order)
                    .ThenInclude(o => o!.OrderType)
                    .Include(o => o.Order)
                    .ThenInclude(o => o!.Restaurant)
                    .Include(o => o.Drink)
                    .ThenInclude(o => o!.Price)
                    .Select(a => new OrderItemView()
                    {
                        Id = a.Id,
                        Food = a.Food!.Name,
                        Ingredient = a.Ingredient!.Name,
                        Drink = a.Drink!.Name,
                        Order = a.Order!.Number,
                        OrderId = a.Order.Id,
                        Quantity = a.Quantity,
                        DrinkPrice = a.Drink.Price!.Value,
                        FoodPrice = a.Food.Price!.Value,
                        IngredientPrice = a.Ingredient.Price!.Value,
                        OrderType = a.Order.OrderType!.Name,
                        PaymentType = a.Order.PaymentType!.Name,
                        Restaurant = a.Order.Restaurant!.Address
                    }).ToListAsync();
            }

            return await query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Order)
                .ThenInclude(o => o!.OrderType)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Order)
                .ThenInclude(o => o!.PaymentType)
                .Where(o => o.OrderId == orderId)
                .Select(a => new OrderItemView()
                {
                    Id = a.Id,
                    Food = a.Food!.Name,
                    Ingredient = a.Ingredient!.Name,
                    Drink = a.Drink!.Name,
                    Order = a.Order!.Number,
                    OrderId = a.Order.Id,
                    Quantity = a.Quantity,
                    DrinkPrice = a.Drink.Price!.Value,
                    FoodPrice = a.Food.Price!.Value,
                    IngredientPrice = a.Ingredient.Price!.Value,
                    OrderType = a.Order.OrderType!.Name,
                    PaymentType = a.Order.PaymentType!.Name,
                    Restaurant = a.Order.Restaurant!.Address
                }).ToListAsync();
        }

        public virtual async Task<OrderItemView> FirstOrDefaultForViewAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            return await query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Order)
                .ThenInclude(o => o!.OrderType)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                .Where(r => r.Id == id)
                .Select(a => new OrderItemView()
                {
                    Id = a.Id,
                    Food = a.Food!.Name,
                    Ingredient = a.Ingredient!.Name,
                    Drink = a.Drink!.Name,
                    Order = a.Order!.Number,
                    Quantity = a.Quantity,
                    DrinkPrice = a.Drink.Price!.Value,
                    FoodPrice = a.Food.Price!.Value,
                    IngredientPrice = a.Ingredient.Price!.Value,
                    OrderType = a.Order.OrderType!.Name,
                })
                .FirstOrDefaultAsync();
        }
    }
}