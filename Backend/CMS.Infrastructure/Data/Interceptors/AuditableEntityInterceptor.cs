using CMS.Infrastructure.Data.Extensions;

namespace CMS.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public static void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {

                if (entry.State == EntityState.Added)
                {
                    //TODO: CreatedBy shall change to the login user when authentication feature is implemented.
                    entry.Entity.CreatedBy = "admin";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified
                    || entry.HasChangedOwnedEntities())
                {
                    //TODO: CreatedBy shall change to the login user when authentication feature is implemented.
                    entry.Entity.ModifiedBy = "admin";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }
}
