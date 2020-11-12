using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Campaign : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;
        
        public string To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;
        
        [MaxLength(1024)] public string? Comment { get; set; }
    }
}