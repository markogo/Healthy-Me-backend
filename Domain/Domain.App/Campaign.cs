using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Campaign : DomainEntityIdMetadata
    {
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        [MaxLength(512)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(1024)]
        public string? Comment { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
    
}