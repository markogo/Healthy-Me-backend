using System.Threading.Tasks;
using BLL.App.DTO;
using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IBaseEntityService<Order>, IOrderRepositoryCustom<OrderView>
    {
        Task<Order> AddNewOrder(Order order);
    }
}