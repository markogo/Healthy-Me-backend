using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;
        
        public int Number { get; set; } = default!;

        public bool Completed { get; set; }
        
        public string TimeCreated { get; set; } = default!;
        
        public Guid RestaurantId { get; set; }
        
        public Guid OrderTypeId { get; set; }
        
        public Guid PaymentTypeId { get; set; }
        
        public Guid AppUserId { get; set; }
    }
}