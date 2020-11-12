using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderMapper : BaseMapper<BLL.App.DTO.Order, Order>
    {
        
        public OrderMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderView, OrderView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public OrderView MapOrderView(BLL.App.DTO.OrderView inObject)
        {
            return Mapper.Map<OrderView>(inObject);
        }
        
    }
}