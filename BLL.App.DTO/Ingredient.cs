using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Ingredient : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public Guid FoodId { get; set; } = default!;
        public Food? Food { get; set; }
        
        public Guid PriceId { get; set; } = default!;
        public Price? Price { get; set; }
    }
}