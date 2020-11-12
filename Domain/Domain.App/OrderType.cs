using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class OrderType : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        [MaxLength(1024)] public string? Comment { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }

}