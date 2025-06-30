using System.Data.Entity;
using eUseControl.Domain.Entities;

namespace eUseControl.Domain
{
     public class eUseControlContext : DbContext
     {
          public eUseControlContext() : base("name=eUseControlDB")
          {
               Database.SetInitializer(new DropCreateDatabaseIfModelChanges<eUseControlContext>());
          }

          public DbSet<User> Users { get; set; }
          public DbSet<Product> Products { get; set; }
          public DbSet<Order> Orders { get; set; } 
          public DbSet<OrderItem> OrderItems { get; set; } 

          protected override void OnModelCreating(DbModelBuilder modelBuilder)
          {
               modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
               base.OnModelCreating(modelBuilder);
          }
     }
}