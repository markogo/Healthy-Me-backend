using Contracts.DAL.App.Repositories;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IAreaRepository Areas { get; }
        ICampaignRepository Campaigns { get; }
        IDrinkRepository Drinks { get; }
        IFoodRepository Foods { get; }
        IFoodTypeRepository FoodTypes { get; }
        IIngredientRepository Ingredients { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        IOrderTypeRepository OrderTypes { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IPriceRepository Prices { get; }
        IRestaurantRepository Restaurants { get; }
        ITownRepository Towns { get; }
    }
}