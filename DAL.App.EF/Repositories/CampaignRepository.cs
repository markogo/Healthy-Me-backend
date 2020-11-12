using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class CampaignRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Campaign, DAL.App.DTO.Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Campaign, DAL.App.DTO.Campaign>())
        {
        }
    }
}