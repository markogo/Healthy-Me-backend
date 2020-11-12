using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class IngredientMapper : BaseMapper<BLL.App.DTO.Ingredient, Ingredient>
    {
        
        public IngredientMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.IngredientView, IngredientView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public IngredientView MapIngredientView(BLL.App.DTO.IngredientView inObject)
        {
            return Mapper.Map<IngredientView>(inObject);
        }
    }
}