using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Order : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(256)] [MinLength(1)] public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public bool Completed { get; set; }

        public string TimeCreated { get; set; } = default!;

        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }
        
        public Guid OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }
        
        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
        
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}