using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.magord.healthyfood.Domain.Base;

namespace Domain.App
{
    public class FoodType : DomainEntityIdMetadata
    {
        [MaxLength(256)] [MinLength(1)] public string Name { get; set; } = default!;

        public ICollection<Food>? Foods { get; set; }
    }

}