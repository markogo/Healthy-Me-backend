using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IOrderServiceMapper : IBaseMapper<DALAppDTO.Order, BLLAppDTO.Order>
    {
        BLLAppDTO.OrderView MapOrderView(DALAppDTO.OrderView inObject);
        
    }
}