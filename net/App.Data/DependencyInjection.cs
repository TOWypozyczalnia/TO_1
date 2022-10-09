using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace App.Data;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(configuration["Data:AppConnection:ConnectionString"]));

        services.AddScoped(provider => provider.GetService<AppDbContext>());

        Console.WriteLine("Connected to db...");

        return services;
    }
}