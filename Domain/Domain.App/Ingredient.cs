using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Ingredient : DomainEntityIdMetadata
    { 
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
    
}