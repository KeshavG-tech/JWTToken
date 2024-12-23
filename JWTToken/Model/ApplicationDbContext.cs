using Microsoft.EntityFrameworkCore;
using JWTToken.Model;

namespace JWTToken.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }    

        public DbSet<User> Users { get; set; }

        public DbSet<Login> logins { get; set; }
    }
}
