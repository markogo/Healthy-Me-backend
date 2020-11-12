using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class FoodMapper : BaseMapper<BLL.App.DTO.Food, Food>
    {
        public FoodMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.FoodView, FoodView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public FoodView MapFoodView(BLL.App.DTO.FoodView inObject)
        {
            return Mapper.Map<FoodView>(inObject);
        }
        
    }
}