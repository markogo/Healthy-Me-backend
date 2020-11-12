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
    public class AreaService : BaseEntityService<IAppUnitOfWork, IAreaRepository, IAreaServiceMapper, DAL.App.DTO.Area,
        BLL.App.DTO.Area>, IAreaService
    {
        public AreaService(IAppUnitOfWork uow) : base(uow, uow.Areas, new AreaServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<AreaView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapAreaView(e));
        }
        
        public virtual async Task<AreaView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapAreaView(await Repository.FirstOrDefaultForViewAsync(id));
        }

    }
}