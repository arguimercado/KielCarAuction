using CarAuction.API.Features.Commands;
using CarAuction.API.Features.Queries;
using Carter;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Endpoints;

public class AuctionModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auctions")
                       .WithTags("Auctions");

        group.MapPost("/", async ([FromBody] CreateAuctionRequest request,[FromServices]ISender sender,CancellationToken cancelationToken = default) =>
        {
            var command = new CreateAuctionCommand(request);
            var response = await sender.Send(command, cancelationToken);

            if(response.IsFailed)
                return Results.Problem(response.Errors.First().Message);

            return Results.Ok(response.Value);
        });

        group.MapPut("/{id:guid}", async ([FromRoute]Guid id, [FromBody]UpdateAuctionRequest request, [FromServices] ISender sender, CancellationToken cancelationToken = default) =>
        {
            var command = new UpdateAuctionCommand(id, request);
            var response = await sender.Send(command, cancelationToken);

            if (response.IsFailed)
                return Results.Problem(response.Errors.First().Message);

            return Results.Ok(response.Value);  
        });

        group.MapGet("/", async ([FromServices] ISender sender, CancellationToken cancelationToken = default) =>
        {
            var query = new GetAuctionsQuery();
            var response = await sender.Send(query, cancelationToken);
            
            if (response.IsFailed)
                return Results.Problem(response.Errors.First().Message);

            return Results.Ok(response.Value);
        });

        group.MapGet("/{id:guid}", async ([FromRoute]Guid id, [FromServices]ISender sender,CancellationToken cancelationToken = default) =>
        {
            var query = new GetAuctionByIdQuery(id);
            var response = await sender.Send(query, cancelationToken);

            if (response.IsFailed)
                return Results.Problem(response.Errors.First().Message);

            return Results.Ok(response.Value);
        });
    }
}
