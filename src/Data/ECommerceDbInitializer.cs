using Kaleido.Samples.ECommerce.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace Kaleido.Samples.ECommerce.Data;

/// <summary>
/// Creates the catalog database and seeds it (deterministically, from <c>Assets/*.json</c>) when it
/// does not exist yet; <paramref name="reseed"/> deletes it first, to start over.
/// </summary>
public static class ECommerceDbInitializer
{
    public static async Task InitializeAsync(
        IServiceProvider serviceProvider,
        bool reseed)
    {
        await using var scope =
            serviceProvider.CreateAsyncScope();

        var dbContext =
            scope.ServiceProvider
                .GetRequiredService<ECommerceDbContext>();

        if (reseed)
        {
            await dbContext.Database.EnsureDeletedAsync();
        }

        if (await dbContext.Database.EnsureCreatedAsync())
        {
            new ECommerceSeeder(dbContext).Seed();
        }
    }
}
