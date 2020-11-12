﻿using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Mappers;
using DALAppDTO=DAL.App.DTO;
using BLLAppDTO=BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface IAreaServiceMapper : IBaseMapper<DALAppDTO.Area, BLLAppDTO.Area>
    {
        BLLAppDTO.AreaView MapAreaView(DALAppDTO.AreaView inObject);
        
    }
}