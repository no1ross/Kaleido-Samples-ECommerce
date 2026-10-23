using Kaleido.Samples.ECommerce.Data.Entities;

namespace Kaleido.Samples.ECommerce.Data.Seed.Seeders;

internal sealed class InventorySeeder
{
    private static readonly Random Random = new(SeedSettings.RandomSeed);

    public static void Seed(
        ECommerceDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(
            dbContext);

        var inventories =
            dbContext.Products
                .AsEnumerable()
                .Select(
                    product => new Inventory
                    {
                        InventoryId =
                            Guid.NewGuid(),

                        ProductId =
                            product.ProductId,

                        AvailableQuantity =
                            GenerateQuantity(),

                        ReorderThreshold =
                            GenerateReorderThreshold(),

                        UpdatedUtc =
                            DateTime.UtcNow
                    })
                .ToList();

        dbContext.Inventories.AddRange(
            inventories);

        dbContext.SaveChanges();
    }

    private static int GenerateQuantity()
    {
        var bucket =
            Random.Next(
                1,
                101);

        return bucket switch
        {
            <= 10 => 0,

            <= 25 => Random.Next(1, 10),

            <= 75 => Random.Next(10, 100),

            _ => Random.Next(100, 500)
        };
    }

    private static int GenerateReorderThreshold()
    {
        return Random.Next(
            10,
            100);
    }
}
