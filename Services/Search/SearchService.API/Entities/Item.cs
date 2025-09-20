using MongoDB.Entities;

namespace SearchService.API.Entities;

public class Item : Entity
{  
    public int ReservePrice { get; set; }

    public string Seller { get; set; } = default!;

    public string? Winner { get; set; }

    public int? SoldAmount { get; set; }

    public int? CurrentHigherBidder { get; set; }

    public DateTime AuctionDate { get; set; }

    public string Make { get; set; } = default!;

    public string Model { get; set; } = default!;


    public int Year { get; set; }

    public string Color { get;  set; } = default!;

    public int? Milleage { get; set; } = default!;

    public string? ImageUrl { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime ModifiedOn { get; set; }

}
