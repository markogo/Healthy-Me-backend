using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PriceServiceMapper : BaseMapper<DALAppDTO.Price, BLLAppDTO.Price>, IPriceServiceMapper
    {
        public PriceServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Campaign, BLLAppDTO.Campaign>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PriceView, BLLAppDTO.PriceView>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.PriceView MapPriceView(DALAppDTO.PriceView inObject)
        {
            return Mapper.Map<BLLAppDTO.PriceView>(inObject);
        }
    }
}