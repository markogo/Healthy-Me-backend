using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class FoodView
    {
        public Guid Id { get; set; }
        
        [MaxLength(1024)] public string? Description { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public int Amount { get; set; } = default!;
        
        public string Size { get; set; } = default!;
        
        public string FoodType { get; set; } = default!;
        
        public decimal Price { get; set; } = default!;
    }
}