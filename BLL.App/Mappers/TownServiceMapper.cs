using AutoMapper;
using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;
using MapperConfiguration = AutoMapper.MapperConfiguration;

namespace BLL.App.Mappers
{
    public class TownServiceMapper : BaseMapper<DALAppDTO.Town, BLLAppDTO.Town>, ITownServiceMapper
    {
        public TownServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}