using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IRestaurantServiceMapper : IBaseMapper<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>
    {
        BLLAppDTO.RestaurantView MapRestaurantView(DALAppDTO.RestaurantView inObject);
        
    }
}