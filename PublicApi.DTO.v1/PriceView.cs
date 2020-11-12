using System;

namespace PublicApi.DTO.v1
{
    public class PriceView
    {
        public Guid Id { get; set; }
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;

        public string? Campaign { get; set; }

    }
}