using BLL.App.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using DAL.App.EF;
using ee.itcollege.magord.healthyfood.BLL.Base;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }
        
        public IAreaService Areas =>
            GetService<IAreaService>(() => new AreaService(UnitOfWork));
        
        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UnitOfWork));

        public IDrinkService Drinks =>
            GetService<IDrinkService>(() => new DrinkService(UnitOfWork));

        public IFoodService Foods =>
            GetService<IFoodService>(() => new FoodService(UnitOfWork));

        public IFoodTypeService FoodTypes =>
            GetService<IFoodTypeService>(() => new FoodTypeService(UnitOfWork));

        public IIngredientService Ingredients =>
            GetService<IIngredientService>(() => new IngredientService(UnitOfWork));

        public IOrderService Orders =>
            GetService<IOrderService>(() => new OrderService(UnitOfWork));
        
        public IOrderItemService OrderItems =>
            GetService<IOrderItemService>(() => new OrderItemService(UnitOfWork));

        public IOrderTypeService OrderTypes =>
            GetService<IOrderTypeService>(() => new OrderTypeService(UnitOfWork));

        public IPaymentTypeService PaymentTypes =>
            GetService<IPaymentTypeService>(() => new PaymentTypeService(UnitOfWork));

        public IPriceService Prices =>
            GetService<IPriceService>(() => new PriceService(UnitOfWork));

        public IRestaurantService Restaurants =>
            GetService<IRestaurantService>(() => new RestaurantService(UnitOfWork));
        
        public ITownService Towns =>
            GetService<ITownService>(() => new TownService(UnitOfWork));
        
    }
}