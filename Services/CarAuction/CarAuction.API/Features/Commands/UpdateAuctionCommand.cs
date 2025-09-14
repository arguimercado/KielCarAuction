
using CarAuction.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Features.Commands;

public record UpdateAuctionRequest(
    string Make,
    string Model,
    int Year,
    string Color,
    int Milleage);

public record UpdateAuctionCommand(Guid Id, UpdateAuctionRequest Request) : ICommand;

public class UpdateAuctionValidator : AbstractValidator<UpdateAuctionCommand>
{
    public UpdateAuctionValidator()
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

      
    }
}


internal class UpdateAuctionCommandHandler(AuctionDbContext db) : ICommandHandler<UpdateAuctionCommand>
{
    public async Task<Result<Unit>> Handle(UpdateAuctionCommand request, CancellationToken cancellationToken)
    {
        var auctionItem = await db.Auctions
                                    .AsTracking()
                                    .Include(item => item)
                                    .FirstOrDefaultAsync(a => a.Id == request.Id);
        if(auctionItem == null)
        {
            return Result.Fail(new Error("Auction Item not found"));
        }

        auctionItem.UpdateItem(request.Request.Make, request.Request.Model, request.Request.Year, request.Request.Color, request.Request.Milleage);
        await db.SaveChangesAsync(cancellationToken);

        return Result.Ok(Unit.Value);
    }
}
