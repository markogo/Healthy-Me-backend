using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace PublicApi.DTO.v1
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;
        
        public Guid? CampaignId { get; set; }
    }
}