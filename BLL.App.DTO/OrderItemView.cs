using System;

namespace BLL.App.DTO
{
    public class OrderItemView
    {
        public Guid Id { get; set; } = default!;
     
        public int Quantity { get; set; } = default!;
        
        public string? Food { get; set; }

        public string? Ingredient { get; set; }

        public string? Drink { get; set; }
        
        public int Order { get; set; } = default!;
        
        public Guid OrderId { get; set; }
        
        public decimal FoodPrice { get; set; }
        
        public decimal DrinkPrice { get; set; }
        
        public decimal IngredientPrice { get; set; }
        
        public string OrderType { get; set; } = default!;
        public string PaymentType { get; set; } = default!;
        public string Restaurant { get; set; } = default!;
    }
}