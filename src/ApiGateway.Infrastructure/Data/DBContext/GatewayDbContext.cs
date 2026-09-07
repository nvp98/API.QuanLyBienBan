using ApiGateway.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Data.DBContext;

public class GatewayDbContext : DbContext
{
    public GatewayDbContext(
        DbContextOptions<GatewayDbContext> options)
        : base(options)
    {
    }

    public DbSet<BienBanKPH> BienBanKPH => Set<BienBanKPH>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BienBanKPH>(entity =>
        {
            entity.HasKey(x => x.MaBB);
        });
    }
}