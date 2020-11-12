using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Drink : DomainEntityIdMetadata
    {
        public float Size { get; set; } = default!;

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;
        
        public ICollection<OrderItem>? OrderItems { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
        
        
    }
}