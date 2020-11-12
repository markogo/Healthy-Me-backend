using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class RestaurantServiceMapper : BaseMapper<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>, IRestaurantServiceMapper
    {
        public RestaurantServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.RestaurantView, BLLAppDTO.RestaurantView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Area, BLLAppDTO.Area>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Town, BLLAppDTO.Town>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.RestaurantView MapRestaurantView(DALAppDTO.RestaurantView inObject)
        {
            return Mapper.Map<BLLAppDTO.RestaurantView>(inObject);
        }
        
    }
}