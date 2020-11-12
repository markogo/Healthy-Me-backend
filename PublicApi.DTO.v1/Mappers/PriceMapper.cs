using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PriceMapper : BaseMapper<BLL.App.DTO.Price, Price>
    {
        public PriceMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PriceView, PriceView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public PriceView MapPriceView(BLL.App.DTO.PriceView inObject)
        {
            return Mapper.Map<PriceView>(inObject);
        }
        
    }
}