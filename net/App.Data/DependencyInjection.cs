using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using App.Data.Interfaces;

namespace App.Data;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>())
            .AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(configuration["Data:AppConnection:ConnectionString"]));
    }
}