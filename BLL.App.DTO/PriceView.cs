using System;

namespace BLL.App.DTO
{
    public class PriceView
    {
        public Guid Id { get; set; } = default!;
        
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; }

        public string? Campaign { get; set; }
    }
}