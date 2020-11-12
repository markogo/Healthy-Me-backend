using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface ICampaignServiceMapper : IBaseMapper<DALAppDTO.Campaign, BLLAppDTO.Campaign>
    {
        
    }
}