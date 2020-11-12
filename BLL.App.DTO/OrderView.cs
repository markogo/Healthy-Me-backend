using System;

namespace BLL.App.DTO
{
    public class OrderView
    {
        public Guid Id { get; set; } = default!;

        public string OrderStatus { get; set; } = default!;

        public int Number { get; set; } = default!;
        
        public bool Completed { get; set; }

        public string TimeCreated { get; set; } = default!;
        
        public string Restaurant { get; set; } = default!;
        
        public string Area { get; set; } = default!;
        
        public string Town { get; set; } = default!;

        public string OrderType { get; set; } = default!;
        
        public string PaymentType { get; set; } = default!;
    }
}