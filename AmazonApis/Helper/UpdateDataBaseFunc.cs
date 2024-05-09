using AmazonCore.Entities.Identity;
using AmazonRepository.Data;
using AmazonRepository.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AmazonApis.Helper
{
    public static class UpdateDataBaseFunc
    {
        public static async Task UpdateDataBase(WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var dbContext = service.GetRequiredService<DataContext>();
                var IdentitydbContext = service.GetRequiredService<IdentityDataContext>();

                var UserManager = service.GetRequiredService<UserManager<AppUser>>();

                await dbContext.Database.MigrateAsync();
                await IdentitydbContext.Database.MigrateAsync();

                await DataContextSeed.ApplySeeding(dbContext);
                await IdentityDataContextSeed.IdentityApplySeeding(UserManager);

            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Appling Migration");
            }
        }
    }
}
