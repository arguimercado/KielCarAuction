using CarAuction.API.Entities.Enums;
using DomainBlocks.Domains;

namespace CarAuction.API.Entities;

public class Auction : AggregateRoot<Guid>
{

    public static Auction Create(int reservePrice, string seller,DateTime auctionEnd, Item item,AuctionStatusEnum status) 
        => new(Guid.NewGuid(), reservePrice, seller, auctionEnd, item,status);

    protected Auction(Guid id,int reservePrice, string seller, DateTime auctionEnd, Item item,AuctionStatusEnum status) : base(id)
    {
        ReservePrice = reservePrice;
        Seller = seller;
        Item = item;
        Status = Status;
        AuctionEnd = auctionEnd;
       

    }
    public Auction() : base(Guid.NewGuid())
    {
    }


    public int ReservePrice { get; private  set; }
    public string Seller { get; private set; } = default!;
    public string? Winner { get; private set; } = default!;
    public int? SoldAmount { get; private set; }
    public int? CurrentHighBidder { get; private set; }

    public DateTime AuctionEnd { get; private set; } = default!;

    public AuctionStatusEnum? Status { get; private set; }

    public Item Item { get; private set; } = default!;

    public void UpdateItem(string make, string model, int year, string color, int milleage)
    {
        Item.Update(make, model, year, color, milleage);
    }


}
