using DomainBlocks.Domains;

namespace CarAuction.API.Entities;

public class Item : Entity<Guid>
{

    public static Item Create(string make, string model, int year, string color, int milleage, string imageUrl)
        => new Item(make, model, year, color, milleage, imageUrl);

    public Item() : base(Guid.NewGuid())
    {
        
    }

    protected Item(string make, string model, int year, string color, int milleage, string imageUrl)
    {
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Milleage = milleage;
        ImageUrl = imageUrl;
    }

    public string Make { get; private set; } = default!;
    public string Model { get; private set; } = default!;

    public int Year { get; private set; } = 0;

    public string Color { get; private set; } = default!;

    public int Milleage { get; private set; } = default!;

    public string ImageUrl { get; private set; } = default!;

    public Auction Auction { get; private set; } = default!;

    public Guid AuctionId { get; private set; } = default!;

    public void Update(string make,string model,int year, string color, int milleage)
    {
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Milleage = milleage;
        
    }


}
