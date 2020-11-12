using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class AreaMapper : BaseMapper<BLL.App.DTO.Area, Area>
    {
        public AreaMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.AreaView, AreaView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public AreaView MapAreaView(BLL.App.DTO.AreaView inObject)
        {
            return Mapper.Map<AreaView>(inObject);
        }
 
    }
}