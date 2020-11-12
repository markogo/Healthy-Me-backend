using System;

namespace BLL.App.DTO
{
    public class FoodView
    {
        public Guid Id { get; set; }
        
        public string? Description { get; set; }
        
        public string Name { get; set; } = default!;
        
        public int Amount { get; set; } = default!;
        
        public string Size { get; set; } = default!;
        
        public string FoodType { get; set; } = default!;
        public decimal Price { get; set; } = default!;
    }
}