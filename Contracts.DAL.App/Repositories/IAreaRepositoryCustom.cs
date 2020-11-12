using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IAreaRepositoryCustom : IAreaRepositoryCustom<AreaView>
    {
    }
    
    public interface IAreaRepositoryCustom<TAreaView>
    {
        Task<IEnumerable<TAreaView>> GetAllForViewAsync();
        Task<TAreaView> FirstOrDefaultForViewAsync(Guid id);
    }

}