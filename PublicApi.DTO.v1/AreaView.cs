using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class AreaView
    {
        public Guid Id { get; set; }
        
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;
        
        public string Town { get; set; } = default!;
    }
}