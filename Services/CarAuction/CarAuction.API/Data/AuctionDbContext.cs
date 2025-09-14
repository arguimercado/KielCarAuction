using CarAuction.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Data;

public class AuctionDbContext : DbContext
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionDbContext).Assembly);
    }
  
}
