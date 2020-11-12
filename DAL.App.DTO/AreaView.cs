using System;

namespace DAL.App.DTO
{
    public class AreaView
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public string Town { get; set; } = default!;
    }
}