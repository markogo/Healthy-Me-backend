using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Food : DomainEntityIdMetadata
    {
        [MaxLength(1024)]
        public string? Description { get; set; }

        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public string Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
}