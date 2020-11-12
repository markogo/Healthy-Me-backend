using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderItemServiceMapper : BaseMapper<DALAppDTO.OrderItem, BLLAppDTO.OrderItem>, IOrderItemServiceMapper
    {
        public OrderItemServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Food, BLLAppDTO.Food>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentType, BLLAppDTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderItemView, BLLAppDTO.OrderItemView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Ingredient, BLLAppDTO.Ingredient>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Drink, BLLAppDTO.Drink>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Price, BLLAppDTO.Price>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.OrderItemView MapOrderItemView(DALAppDTO.OrderItemView inObject)
        {
            return Mapper.Map<BLLAppDTO.OrderItemView>(inObject);
        }
        
    }
}