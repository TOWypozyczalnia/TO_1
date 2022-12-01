using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using App.Data.Interfaces;
using App.Data.Repositories;

namespace App.Data;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IActorRepository, ActorRepository>()
            .AddScoped<IMovieRepository, MovieRepository>()
            .AddScoped<IDirectorRepository, DirectorRepository>()
			.AddScoped<IReviewRepository, ReviewRepository>()
			.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(configuration["Data:AppConnection:ConnectionString"]))
            .AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());
    }
}