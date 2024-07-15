using HomeWebApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace HomeWebApp.Persistence.Data
{
    public class HomeWebApiDbContext:DbContext
    {
        public HomeWebApiDbContext(DbContextOptions<HomeWebApiDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Employee> Employees {  get; set; } 

        public DbSet<AppFiles> AppFiles { get; set; }

        public DbSet<Department> Departments { get; set; }

    }
}
