using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class IngredientServiceMapper : BaseMapper<DALAppDTO.Ingredient, BLLAppDTO.Ingredient>, IIngredientServiceMapper
    {
        public IngredientServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Food, BLLAppDTO.Food>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.IngredientView, BLLAppDTO.IngredientView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.IngredientView MapIngredientView(DALAppDTO.IngredientView inObject)
        {
            return Mapper.Map<BLLAppDTO.IngredientView>(inObject);
        }
    }
}