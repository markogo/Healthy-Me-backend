using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class RestaurantMapper : BaseMapper<BLL.App.DTO.Restaurant, Restaurant>
    {
        
        public RestaurantMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.RestaurantView, RestaurantView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public RestaurantView MapRestaurantView(BLL.App.DTO.RestaurantView inObject)
        {
            return Mapper.Map<RestaurantView>(inObject);
        }
        
    }
}