using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class PaymentType : DomainEntityIdMetadata
    {
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}