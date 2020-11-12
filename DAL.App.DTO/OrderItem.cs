using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO
{
    public class OrderItem : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
     
        public int Quantity { get; set; } = default!;
        
        public Guid? FoodId { get; set; }
        public Food? Food { get; set; }

        public Guid? IngredientId { get; set; }
        public Ingredient? Ingredient { get; set; }

        public Guid? DrinkId { get; set; }
        public Drink? Drink { get; set; }
        
        public Guid OrderId { get; set; } = default!;
        public Order? Order { get; set; }
        
        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }
    }
}