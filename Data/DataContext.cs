using crud.Models;

using Microsoft.EntityFrameworkCore;

namespace crud.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
            // Checks if db exist and create if it is not.
            // It uses connection string from appsettings.json to locate physical path for db file.
            this.Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite();
    }
}
