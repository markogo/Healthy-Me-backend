using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class IngredientView
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public string Food { get; set; } = default!;
        
        public decimal Price { get; set; } = default!;
    }
}