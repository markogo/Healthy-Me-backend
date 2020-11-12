using System;

namespace BLL.App.DTO
{
    public class RestaurantView
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public string Area { get; set; } = default!;
        
        public string Town { get; set; } = default!;
    }
}