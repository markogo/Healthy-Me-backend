using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        
        public string UserName { get; set; } = default!;
    }
}