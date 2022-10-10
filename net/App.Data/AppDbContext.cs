using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using App.Data.Interfaces;

#pragma warning restore format

namespace App.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options) : base(options)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration["Data:AppConnection:ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    DbSet<TEntity> IAppDbContext.Set<TEntity, TKey>()
    {
        return Set<TEntity>();
    }
}