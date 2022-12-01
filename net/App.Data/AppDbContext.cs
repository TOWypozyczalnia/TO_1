﻿using App.Data.Entities;
using App.Data.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#pragma warning restore format

namespace App.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options) : base(options)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public DbSet<Actor> Actor { get; set; }
    public DbSet<Movie> Movie { get; set; }
    public DbSet<Director> Director { get; set; }
    public DbSet<LoggedUser> LoggedUser { get; set; }
    public DbSet<Review> Review { get; set; }

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

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    DbSet<TEntity> IAppDbContext.Set<TEntity, TKey>()
    {
        return Set<TEntity>();
    }
}