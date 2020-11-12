using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class OrderType : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        public string? Comment { get; set; }
    }
}