using Microsoft.EntityFrameworkCore;

namespace Models.Db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ClientDb> Clients { get; set; }
        public DbSet<EmployeeDb> Employees { get; set; }
        public DbSet<AccountDb> Accounts { get; set; }
        public DbSet<CurrencyDb> Currencies { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;" +
                "Port=5432;" +
                "Database=bank;" +
                "Username=postgres;" +
                "Password=1357911"
                );
        }
    }
}