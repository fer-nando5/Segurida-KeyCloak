using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace System.Eclaim.Infraestructure.Configurations.Context
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var devConnectionString = "Host=localhost; Port=1600; Database=security_db; Username=admin; Password=Password2025";
            var connectionString = Environment.GetEnvironmentVariable("SecurityDb") ?? devConnectionString;

            var optionBuilder = new DbContextOptionsBuilder<IdentityDbContext>();

            optionBuilder.UseNpgsql(connectionString,
                opt => opt.MigrationsHistoryTable("__EFMigrationHistory"));
            return new IdentityDbContext(optionBuilder.Options);
        }
    }
}
