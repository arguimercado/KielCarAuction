
using CarAuction.API.Data;


namespace CarAuction.API.Features.Commands;

public record CreateAuctionRequest(
    string Make,
    string Model,
    int Year,
    string Color,
    int Milleage,
    string? ImageUrl,
    int ReservePrice,
    DateTime AuctionEnd);

public record CreateAuctionResponse(Guid Id);

public record CreateAuctionCommand(CreateAuctionRequest Request) : ICommand<CreateAuctionResponse>;
public class CreateAuctionValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionValidator()
    {
        RuleFor(a => a.Request.Make)
            .NotEmpty().WithMessage("Make is required.")
            .MaximumLength(50).WithMessage("Make cannot exceed 50 characters.");
        RuleFor(a => a.Request.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(50).WithMessage("Model cannot exceed 50 characters.");

        RuleFor(a => a.Request.Year)
            .InclusiveBetween(1886, DateTime.UtcNow.Year + 1)
            .WithMessage($"Year must be between 1886 and {DateTime.UtcNow.Year + 1}.");

        RuleFor(a => a.Request.Color)
            .NotEmpty().WithMessage("Color is required.")
            .MaximumLength(30).WithMessage("Color cannot exceed 30 characters.");

        RuleFor(a => a.Request.Milleage)
            .GreaterThanOrEqualTo(0).WithMessage("Milleage must be a non-negative value.");

        RuleFor(a => a.Request.ReservePrice)
            .GreaterThanOrEqualTo(0).WithMessage("Reserve Price must be a non-negative value.");

        RuleFor(a => a.Request.AuctionEnd)
            .GreaterThan(DateTime.UtcNow).WithMessage("Auction end date must be in the future.");
    }
}

internal class CreateAuctionCommandHandler(AuctionDbContext db) : ICommandHandler<CreateAuctionCommand,CreateAuctionResponse>
{
    public async Task<Result<CreateAuctionResponse>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = Entities.Auction.Create(
            reservePrice: request.Request.ReservePrice,
            seller: "bob", // In a real application, this would come from the authenticated user context
            auctionEnd: request.Request.AuctionEnd,
            item: Entities.Item.Create(
                make: request.Request.Make,
                model: request.Request.Model,
                color: request.Request.Color,
                milleage: request.Request.Milleage,
                year: request.Request.Year,
                imageUrl: request.Request.ImageUrl ?? ""),
            status: Entities.Enums.AuctionStatusEnum.Live);

        db.Auctions.Add(auction);

        await db.SaveChangesAsync(cancellationToken);

        return Result.Ok(new CreateAuctionResponse(auction.Id));
    }
}
