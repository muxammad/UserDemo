using Microsoft.EntityFrameworkCore;
using UserDemo.Domain.Entities;

namespace UserDemo.DataAccess.Context
{
    public class UserDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = "Server=(localdb)\\MSSQLLocalDB; Database=UserDemo;Trusted_Connection = true;";
            optionsBuilder.UseSqlServer(path);
        }

        public DbSet<User> Users { get; set; }
    }
}
