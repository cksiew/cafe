
using CMS.Application.Configurations;
using Microsoft.Extensions.Configuration;

namespace CMS.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider, ApplicationConfiguration appConfig)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context, appConfig);
        }

        private static async Task SeedAsync(ApplicationDbContext context, ApplicationConfiguration appConfig)
        {
            CopySeedDataFiles(appConfig);
            await SeedCafeAsync(context, appConfig);
        }

        private static void CopySeedDataFiles(ApplicationConfiguration appConfig)
        {
            // Copy images
            var targetDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var imagesDirectory = Path.Combine(targetDirectory, appConfig.UploadedImageDirectory);
            if (!Directory.Exists(imagesDirectory))
            {
                Directory.CreateDirectory(imagesDirectory);
            }
            var sourceImagesDirectory = Path.Combine(targetDirectory, "Data", "Sample", "Images");
            foreach( var file in Directory.GetFiles(sourceImagesDirectory))
            {
                var destFilePath = Path.Combine(imagesDirectory, Path.GetFileName(file));
                File.Copy(file, destFilePath,true);
            }
            

        }

        private static async Task SeedCafeAsync(ApplicationDbContext context, ApplicationConfiguration appConfig)
        {
            if (!await context.Cafes.AnyAsync())
            {
                InitialData.InitializeSeedData(appConfig);
                await context.Cafes.AddRangeAsync(InitialData.Cafes);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedEmployeeAsync(ApplicationDbContext context)
        {
            if (!await context.Employees.AnyAsync())
            {
                await context.Employees.AddRangeAsync(InitialData.Employees);
                await context.SaveChangesAsync();
            }
        }

        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
            }
}
