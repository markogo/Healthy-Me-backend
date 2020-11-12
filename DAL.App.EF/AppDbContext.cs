using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ee.itcollege.magord.healthyfood.Contracts.Domain.Base;
using Domain.App;
using Domain.App.Identity;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly IUserNameProvider _userNameProvider;

        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Town> Towns { get; set; } = default!;
        public DbSet<Area> Areas { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
        public DbSet<OrderType> OrderTypes { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
        public DbSet<Ingredient> Ingredients { get; set; } = default!;
        public DbSet<FoodType> FoodTypes { get; set; } = default!;
        public DbSet<Food> Foods { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<Campaign> Campaigns { get; set; } = default!;

        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();
        
        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)
        {
            _userNameProvider = userNameProvider;
        }
        
        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Disabling cascade delete
            
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            // Enable cascade delete on Order->OrderItem
            builder.Entity<Order>()
                .HasMany(s => s.OrderItems)
                .WithOne(l => l.Order!)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Indexes
            builder.Entity<Order>().HasIndex(i => i.CreatedAt);
            builder.Entity<OrderItem>().HasIndex(i => i.CreatedAt);
        }

        private void SaveChangesMetadataUpdate()
        {
            // Update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // Check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // Do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}