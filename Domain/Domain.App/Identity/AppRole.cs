using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId 
    {
    }
}