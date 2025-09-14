using CarAuction.API.Data;
using CarAuction.API.Entities;
using CarAuction.API.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Commons.Extensions;

public static class DatabaseExtension
{
    public static async Task UseMigrationDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            var context = services.GetRequiredService<AuctionDbContext>();
            await context.Database.MigrateAsync();
            logger.LogInformation("Database migration completed successfully.");
            await SeedData(context);
            logger.LogInformation("Seed data completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw; // Re-throw the exception to ensure the application does not start in an inconsistent state
        }
    }

    private static async Task SeedData(AuctionDbContext db)
    {
        if (await db.Auctions.AnyAsync())
            return;

        // Create a list of Auction objects instead of using IEnumerable directly
        IEnumerable<Auction> data = new List<Auction>
        {
            Auction.Create(
                reservePrice: 20000,
                seller: "bob",
                auctionEnd: DateTime.UtcNow.AddDays(10),
                item: Item.Create(
                    make: "Ford",
                    model: "GT",
                    color: "White",
                    milleage: 50000,
                    year: 2020,
                    imageUrl: "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 90000,
                seller: "alice",
                auctionEnd: DateTime.UtcNow.AddDays(60),
                item: Item.Create(
                    make: "Bugatti",
                    model: "Veyron",
                    color: "Black",
                    milleage: 15035,
                    year: 2018,
                    imageUrl: "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 0,
                seller: "bob",
                auctionEnd: DateTime.UtcNow.AddDays(4),
                item: Item.Create(
                    make: "Ford",
                    model: "Mustang",
                    color: "Black",
                    milleage: 65125,
                    year: 2023,
                    imageUrl: "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 50000,
                seller: "tom",
                auctionEnd: DateTime.UtcNow.AddDays(-10),
                item: Item.Create(
                    make: "Mercedes",
                    model: "SLK",
                    color: "Silver",
                    milleage: 15001,
                    year: 2020,
                    imageUrl: "https://cdn.pixabay.com/photo/2016/04/17/22/10/mercedes-benz-1335674_960_720.png"),
                status: AuctionStatusEnum.ReserveNotMet),

            Auction.Create(
                reservePrice: 20000,
                seller: "alice",
                auctionEnd: DateTime.UtcNow.AddDays(30),
                item: Item.Create(
                    make: "BMW",
                    model: "X1",
                    color: "White",
                    milleage: 90000,
                    year: 2017,
                    imageUrl: "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 20000,
                seller: "bob",
                auctionEnd: DateTime.UtcNow.AddDays(45),
                item: Item.Create(
                    make: "Ferrari",
                    model: "Spider",
                    color: "Red",
                    milleage: 50000,
                    year: 2015,
                    imageUrl: "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 150000,
                seller: "alice",
                auctionEnd: DateTime.UtcNow.AddDays(13),
                item: Item.Create(
                    make: "Ferrari",
                    model: "F-430",
                    color: "Red",
                    milleage: 5000,
                    year: 2022,
                    imageUrl: "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 0,
                seller: "bob",
                auctionEnd: DateTime.UtcNow.AddDays(19),
                item: Item.Create(
                    make: "Audi",
                    model: "R8",
                    color: "White",
                    milleage: 10050,
                    year: 2021,
                    imageUrl: "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 20000,
                seller: "tom",
                auctionEnd: DateTime.UtcNow.AddDays(20),
                item: Item.Create(
                    make: "Audi",
                    model: "TT",
                    color: "Black",
                    milleage: 25400,
                    year: 2020,
                    imageUrl: "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"),
                status: AuctionStatusEnum.Live),

            Auction.Create(
                reservePrice: 20000,
                seller: "bob",
                auctionEnd: DateTime.UtcNow.AddDays(48),
                item: Item.Create(
                    make: "Ford",
                    model: "Model T",
                    color: "Rust",
                    milleage: 150150,
                    year: 1938,
                    imageUrl: "https://cdn.pixabay.com/photo/2017/08/02/19/47/vintage-2573090_960_720.jpg"),
                status: AuctionStatusEnum.Live)
        };

        // Add the data to the database context
        await db.Auctions.AddRangeAsync(data);

        // Save changes to persist the data
        await db.SaveChangesAsync();
    }
}
