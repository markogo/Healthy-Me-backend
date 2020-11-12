using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class RestaurantView
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;
        
        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;
        
        public string Area { get; set; } = default!;
        
        public string Town { get; set; } = default!;
    }
}