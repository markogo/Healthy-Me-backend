using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;

namespace BLL.App.DTO
{
    public class Restaurant : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;

        public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public Guid AreaId { get; set; } = default!;
        public Area? Area { get; set; }
    }
}