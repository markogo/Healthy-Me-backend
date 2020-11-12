using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Food : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string? Description { get; set; }

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public string Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        public FoodType? FoodType { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
}