using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IOrderItemServiceMapper : IBaseMapper<DALAppDTO.OrderItem, BLLAppDTO.OrderItem>
    {
        BLLAppDTO.OrderItemView MapOrderItemView(DALAppDTO.OrderItemView inObject);
    }
}