using CMS.Application.Data;

namespace CMS.Infrastructure.Data
{
    internal class ApplicationDbContext(DbContextOptions options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Cafe> Cafes => Set<Cafe>();

        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
