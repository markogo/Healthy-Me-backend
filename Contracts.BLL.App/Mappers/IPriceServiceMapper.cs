using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IPriceServiceMapper : IBaseMapper<DALAppDTO.Price, BLLAppDTO.Price>
    {
        BLLAppDTO.PriceView MapPriceView(DALAppDTO.PriceView inObject);
        
    }
}