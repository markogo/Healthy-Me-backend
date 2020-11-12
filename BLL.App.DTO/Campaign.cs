using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    
    public class Campaign : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string From { get; set; } = default!;
        
        public string To { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public string? Comment { get; set; }
    }
}