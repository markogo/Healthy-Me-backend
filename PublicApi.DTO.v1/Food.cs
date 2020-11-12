using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Food : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(1024)] public string? Description { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public int Amount { get; set; } = default!;
        
        public string Size { get; set; } = default!;
        
        public Guid FoodTypeId { get; set; } = default!;
        
        public Guid PriceId { get; set; } = default!;
    }
}