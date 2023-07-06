using Microsoft.EntityFrameworkCore;

namespace Shop.Infrastructure.EntityFramework;

public sealed class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
}
