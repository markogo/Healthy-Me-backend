using System;
using BLL.App.DTO.Identity;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Order : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;

        public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;

        public bool Completed { get; set; }

        public string TimeCreated { get; set; } = default!;
        
        public Guid RestaurantId { get; set; } = default!;
        public Restaurant? Restaurant { get; set; }

        public Guid OrderTypeId { get; set; } = default!;
        public OrderType? OrderType { get; set; }

        public Guid AppUserId { get; set; } = default!;
        public AppUser? AppUser { get; set; }

        public Guid PaymentTypeId { get; set; } = default!;
        public PaymentType? PaymentType { get; set; }
    }
}