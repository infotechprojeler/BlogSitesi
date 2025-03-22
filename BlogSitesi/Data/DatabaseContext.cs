using BlogSitesi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogSitesi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=BlogSitesi; Trusted_Connection=True; TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                CreateDate = DateTime.Now,
                Id = 1,
                Email = "admin@blogsitesi.io",
                IsActive = true,
                IsAdmin = true,
                Name = "admin",
                Surname = "user",
                Password = "123456",
                Username = "adminuser"
            }); // veritabanı oluştuktan sonra varsayılan kayıt oluşturmak için
            base.OnModelCreating(modelBuilder);
        }
    }
}
