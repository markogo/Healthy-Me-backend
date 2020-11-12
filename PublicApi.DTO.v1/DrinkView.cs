using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class DrinkView
    {
        public Guid Id { get; set; }
        
        public float Size { get; set; } = default!;
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public int Amount { get; set; } = default!;
        
        public decimal Price { get; set; } = default!;
    }
}