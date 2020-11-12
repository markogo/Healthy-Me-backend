using System;

namespace BLL.App.DTO
{
    public class IngredientView
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public string Food { get; set; } = default!;

        public decimal Price { get; set; } = default!;
    }
}