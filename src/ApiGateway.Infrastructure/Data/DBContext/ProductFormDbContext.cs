using Microsoft.EntityFrameworkCore;

namespace ApiGateway.Infrastructure.Data.DBContext;

public class ProductFormDbContext : DbContext
{
    public ProductFormDbContext(DbContextOptions<ProductFormDbContext> options)
        : base(options)
    {
    }
}
