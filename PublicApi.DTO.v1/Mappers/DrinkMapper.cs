using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class DrinkMapper : BaseMapper<BLL.App.DTO.Drink, Drink>
    {
        public DrinkMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DrinkView, DrinkView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public DrinkView MapDrinkView(BLL.App.DTO.DrinkView inObject)
        {
            return Mapper.Map<DrinkView>(inObject);
        }
        
    }
}