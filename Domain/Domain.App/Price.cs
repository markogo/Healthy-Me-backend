using System;
using System.Collections.Generic;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Price : DomainEntityIdMetadata
    {
        public decimal Value { get; set; } = default!;
        
        public string From { get; set; } = default!;
        
        public string To { get; set; } = default!;
        
        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }
        
        public ICollection<Ingredient>? Ingredients { get; set; } = default!;
        
        public ICollection<Food>? Foods { get; set; }  = default!;
        
        public ICollection<Drink>? Drinks { get; set; }  = default!;
    }
    
}