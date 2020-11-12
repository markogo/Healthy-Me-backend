using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}