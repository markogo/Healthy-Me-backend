using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1.Identity
{
    public class AppRole : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MinLength(1)]
        [MaxLength(256)]
        [Required]
        public string Name { get; set; } = default!;
    }
}