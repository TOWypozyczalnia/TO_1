using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning restore format

namespace App.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(IConfiguration configuration, DbContextOptions<DataDbContext> options) : base(options)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration["Data:AppConnection:ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}