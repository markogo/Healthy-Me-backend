using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IDrinkServiceMapper : IBaseMapper<DALAppDTO.Drink, BLLAppDTO.Drink>
    {
        BLLAppDTO.DrinkView MapDrinkView(DALAppDTO.DrinkView inObject);
        
    }
}