using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class Restaurant : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        [MaxLength(512)] [MinLength(1)] public string Address { get; set; } = default!;

        public string OpenedFrom { get; set; } = default!;

        public string ClosedFrom { get; set; } = default!;

        public Guid AreaId { get; set; } = default!;
        public Area? Area { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}