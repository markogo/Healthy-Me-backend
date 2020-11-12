using System;
using ee.itcollege.magord.healthyfood.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IFoodService : IBaseEntityService<Food>, IFoodRepositoryCustom<FoodView>
    {
    }
}