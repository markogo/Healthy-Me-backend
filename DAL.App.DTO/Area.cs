using System;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;


namespace DAL.App.DTO
{
    public class Area : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public string Name { get; set; } = default!;
        
        public Guid TownId { get; set; } = default!;
        public Town? Town { get; set; }
    }
}