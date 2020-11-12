using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class OrderItem : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public int Quantity { get; set; }
        
        public Guid? FoodId { get; set; }

        public Guid? IngredientId { get; set; }

        public Guid? DrinkId { get; set; }
        
        public Guid OrderId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}