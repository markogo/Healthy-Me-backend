using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderItemMapper : BaseMapper<BLL.App.DTO.OrderItem, OrderItem>
    {
        
        public OrderItemMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OrderItemView, OrderItemView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public OrderItemView MapOrderItemView(BLL.App.DTO.OrderItemView inObject)
        {
            return Mapper.Map<OrderItemView>(inObject);
        }
        
    }
}