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
    public class PaymentTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.PaymentType, DAL.App.DTO.PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.PaymentType, DAL.App.DTO.PaymentType>())
        {
        }
    }
}