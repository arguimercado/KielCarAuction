using CarAuction.API.Commons.Dtos;
using CarAuction.API.Data;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Features.Queries;


public record GetAuctionResponse(IEnumerable<AuctionDto> Data);

public record GetAuctionsQuery() : IQuery<GetAuctionResponse>;


internal class GetAuctionsQueryHandler(AuctionDbContext db) : IQueryHandler<GetAuctionsQuery, GetAuctionResponse>
{
    
    public async Task<Result<GetAuctionResponse>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
    {
        
        var auctions = await db.Auctions
                                .AsNoTracking()
                                .Include(item => item.Item)
                                .Select(a => AuctionMapper.EntityToDto(a))
                                .ToListAsync(cancellationToken);

        return Result.Ok(new GetAuctionResponse(auctions));
    }
}
