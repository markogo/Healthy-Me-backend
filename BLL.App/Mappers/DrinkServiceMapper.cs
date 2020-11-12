using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class DrinkServiceMapper : BaseMapper<DALAppDTO.Drink, BLLAppDTO.Drink>, IDrinkServiceMapper
    {
        public DrinkServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Price, DALAppDTO.Price>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.DrinkView, BLLAppDTO.DrinkView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.DrinkView MapDrinkView(DALAppDTO.DrinkView inObject)
        {
            return Mapper.Map<BLLAppDTO.DrinkView>(inObject);
        }
        
    }
}