using CarAuction.API.Commons.Dtos;
using CarAuction.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Features.Queries;

public record AuctionByIdResponse(AuctionDto Data);

public record GetAuctionByIdQuery(Guid Id) : IQuery<AuctionByIdResponse>;

internal class GetAuctionByIdQueryHandler(AuctionDbContext db) : IQueryHandler<GetAuctionByIdQuery, AuctionByIdResponse>
{
    public async Task<Result<AuctionByIdResponse>> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
    {
        var auction = await db.Auctions
                            .Include(a => a.Item)
                            .Where(a => a.Id == request.Id)
                            .Select(m => AuctionMapper.EntityToDto(m))
                            .FirstOrDefaultAsync();

        if (auction == null)
            return Result.Fail(new Error($"Auction with id {request.Id} not found."));


        return Result.Ok(new AuctionByIdResponse(auction));
    }
}
