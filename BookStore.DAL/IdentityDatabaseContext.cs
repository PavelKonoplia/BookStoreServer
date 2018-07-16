using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using BookStore.Entity.Models;

namespace BookStore.DAL
{
    public class IdentityDatabaseContext : IdentityDbContext<User, CustomRole, long,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public IdentityDatabaseContext() : base("DbConnection")
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CustomerToSellerRequest> Requests { get; set; }

        public static IdentityDatabaseContext Create()
        {
            return new IdentityDatabaseContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            //modelBuilder.Entity<Book>().Map(m =>
            //     {
            //         m.MapInheritedProperties();
            //         m.ToTable("Books");
            //     });

            //modelBuilder.Entity<Tag>().Map(m =>
            //    {
            //         m.MapInheritedProperties();
            //         m.ToTable("Tags");
            //    });
        }
    }
}
