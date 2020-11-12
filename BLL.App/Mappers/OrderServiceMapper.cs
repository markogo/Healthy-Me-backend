using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper : BaseMapper<DALAppDTO.Order, BLLAppDTO.Order>, IOrderServiceMapper
    {
        public OrderServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Restaurant, DALAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.OrderType, DALAppDTO.OrderType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.PaymentType, DALAppDTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<BLLAppDTO.Identity.AppUser, DALAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderView, BLLAppDTO.OrderView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderType, BLLAppDTO.OrderType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentType, BLLAppDTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.OrderView MapOrderView(DALAppDTO.OrderView inObject)
        {
            return Mapper.Map<BLLAppDTO.OrderView>(inObject);
        }
        
    }
}