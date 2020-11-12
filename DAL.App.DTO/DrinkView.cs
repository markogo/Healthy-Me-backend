using System;

namespace DAL.App.DTO
{
    public class DrinkView
    {
        public Guid Id { get; set; } = default!;
        
        public float Size { get; set; } = default!;

        public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;

        public decimal Price { get; set; } = default!;
    }
}