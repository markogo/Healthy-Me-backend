using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Restaurant : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;
        
        public Guid AreaId { get; set; } = default!;
    }
}