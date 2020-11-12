using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IAreaRepository Areas => GetRepository<IAreaRepository>(() => new AreaRepository(UowDbContext));
        public ICampaignRepository Campaigns => GetRepository<ICampaignRepository>(() => new CampaignRepository(UowDbContext));
        public IDrinkRepository Drinks => GetRepository<IDrinkRepository>(() => new DrinkRepository(UowDbContext));
        public IFoodRepository Foods => GetRepository<IFoodRepository>(() => new FoodRepository(UowDbContext));
        public IFoodTypeRepository FoodTypes => GetRepository<IFoodTypeRepository>(() => new FoodTypeRepository(UowDbContext));
        public IIngredientRepository Ingredients => GetRepository<IIngredientRepository>(() => new IngredientRepository(UowDbContext));
        public IOrderRepository Orders => GetRepository<IOrderRepository>(() => new OrderRepository(UowDbContext));
        public IOrderItemRepository OrderItems => GetRepository<IOrderItemRepository>(() => new OrderItemRepository(UowDbContext));
        public IOrderTypeRepository OrderTypes => GetRepository<IOrderTypeRepository>(() => new OrderTypeRepository(UowDbContext));
        public IPaymentTypeRepository PaymentTypes => GetRepository<IPaymentTypeRepository>(() => new PaymentTypeRepository(UowDbContext));
        public IPriceRepository Prices => GetRepository<IPriceRepository>(() => new PriceRepository(UowDbContext));
        public IRestaurantRepository Restaurants => GetRepository<IRestaurantRepository>(() => new RestaurantRepository(UowDbContext));
        public ITownRepository Towns => GetRepository<ITownRepository>(() => new TownRepository(UowDbContext));
    }
}