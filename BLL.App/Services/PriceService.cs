using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.magord.healthyfood.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PriceService : BaseEntityService<IAppUnitOfWork, IPriceRepository, IPriceServiceMapper, DAL.App.DTO.Price,
        BLL.App.DTO.Price>, IPriceService
    {
        public PriceService(IAppUnitOfWork uow) : base(uow, uow.Prices, new PriceServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<PriceView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPriceView(e));
        }
        
        public virtual async Task<PriceView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapPriceView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}