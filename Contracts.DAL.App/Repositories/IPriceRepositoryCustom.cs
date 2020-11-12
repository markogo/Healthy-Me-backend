using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPriceRepositoryCustom : IPriceRepositoryCustom<PriceView>
    {
    }
    
    public interface IPriceRepositoryCustom<TPriceView>
    {
        Task<IEnumerable<TPriceView>> GetAllForViewAsync();
        Task<TPriceView> FirstOrDefaultForViewAsync(Guid id);
    }
}