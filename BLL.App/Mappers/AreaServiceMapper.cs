using AutoMapper;
using Contracts.BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class AreaServiceMapper : BaseMapper<DALAppDTO.Area, BLLAppDTO.Area>, IAreaServiceMapper
    {
        public AreaServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.AreaView, BLLAppDTO.AreaView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Town, BLLAppDTO.Town>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.AreaView MapAreaView(DALAppDTO.AreaView inObject)
        {
            return Mapper.Map<BLLAppDTO.AreaView>(inObject);
        }

        
    }
}