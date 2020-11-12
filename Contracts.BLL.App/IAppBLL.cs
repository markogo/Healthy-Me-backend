using Contracts.BLL.App.Services;
using ee.itcollege.magord.healthyfood.Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAreaService Areas { get; }
        public ICampaignService Campaigns { get; }
        public IDrinkService Drinks { get; }
        public IFoodService Foods { get; }
        public IFoodTypeService FoodTypes { get; }
        public IIngredientService Ingredients { get; }
        public IOrderService Orders { get; }
        public IOrderItemService OrderItems { get; }
        public IOrderTypeService OrderTypes { get; }
        public IPaymentTypeService PaymentTypes { get; }
        public IPriceService Prices { get; }
        public IRestaurantService Restaurants { get; }
        public ITownService Towns { get; }
    }
}