using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class FoodServiceMapper : BaseMapper<DALAppDTO.Food, BLLAppDTO.Food>, IFoodServiceMapper
    {
        public FoodServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.FoodType, BLLAppDTO.FoodType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.FoodView, BLLAppDTO.FoodView>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.FoodView MapFoodView(DALAppDTO.FoodView inObject)
        {
            return Mapper.Map<BLLAppDTO.FoodView>(inObject);
        }
        
    }
}