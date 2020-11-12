using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class PaymentType : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)] [MinLength(1)] public string Name { get; set; } = default!;
    }
}