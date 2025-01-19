using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CMS.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Cafe> Cafes { get; }

        DbSet<Employee> Employees { get; }

        DatabaseFacade Database { get; }
        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
