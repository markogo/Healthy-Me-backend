using AutoMapper;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class DALMapper<TLeftObject, TRightObject> : BaseMapper<TLeftObject, TRightObject>
        where TLeftObject : class?, new()
        where TRightObject : class?, new()
    {
        public DALMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Price, Domain.App.Price>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Restaurant, Domain.App.Restaurant>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.OrderType, Domain.App.OrderType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.PaymentType, Domain.App.PaymentType>();
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.OrderItem, DAL.App.DTO.OrderItem>();
            MapperConfigurationExpression.CreateMap<Domain.App.Price, DAL.App.DTO.Price>();
            MapperConfigurationExpression.CreateMap<Domain.App.PaymentType, DAL.App.DTO.PaymentType>();
            MapperConfigurationExpression.CreateMap<Domain.App.OrderType, DAL.App.DTO.OrderType>();
            MapperConfigurationExpression.CreateMap<Domain.App.Restaurant, DAL.App.DTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<Domain.App.Campaign, DAL.App.DTO.Campaign>();
            MapperConfigurationExpression.CreateMap<Domain.App.Ingredient, DAL.App.DTO.Ingredient>();
            MapperConfigurationExpression.CreateMap<Domain.App.Order, DAL.App.DTO.Order>();
            MapperConfigurationExpression.CreateMap<Domain.App.Drink, DAL.App.DTO.Drink>();
            MapperConfigurationExpression.CreateMap<Domain.App.Food, DAL.App.DTO.Food>();
            MapperConfigurationExpression.CreateMap<Domain.App.FoodType, DAL.App.DTO.FoodType>();
            MapperConfigurationExpression.CreateMap<Domain.App.Area, DAL.App.DTO.Area>();
            MapperConfigurationExpression.CreateMap<Domain.App.Town, DAL.App.DTO.Town>();
            MapperConfigurationExpression.CreateMap<Domain.App.Identity.AppUser, DAL.App.DTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}