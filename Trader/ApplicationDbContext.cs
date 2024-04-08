using Microsoft.EntityFrameworkCore;
using Trader.Models;

namespace Trader;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Traders> Traders { get; set; }
}
