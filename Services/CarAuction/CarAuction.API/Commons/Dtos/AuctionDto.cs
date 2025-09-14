using CarAuction.API.Entities;

namespace CarAuction.API.Commons.Dtos;


public static class AuctionMapper
{
    public static AuctionDto EntityToDto(Auction action) => new AuctionDto(
        Id: action.Id.ToString(), 
        ReservePrice: action.ReservePrice,
        Seller: action.Seller,
        Winner: action.Winner,
        SoldAmount: action.SoldAmount,
        CurrentHighBidder: action.CurrentHighBidder,
        AuctionEnd: action.AuctionEnd,
        Make: action.Item.Make,
        Model: action.Item.Model,
        Year: action.Item.Year,
        Color: action.Item.Color,
        Milleage: action.Item.Milleage,
        ImageUrl: action.Item.ImageUrl,
        CreateOn: action.CreatedOn,
        ModifiedOn: action.LastModifiedOn,
        Status: action.Status.ToString() ?? "");
}
public record AuctionDto(
    string Id,
    int ReservePrice, 
    string Seller, 
    string? Winner, 
    int? SoldAmount, 
    int? CurrentHighBidder, 
    DateTime AuctionEnd, 
    string Make,
    string Model,
    int Year,
    string Color,
    int Milleage,
    string? ImageUrl,
    DateTime? CreateOn,
    DateTime? ModifiedOn,
    string Status);

