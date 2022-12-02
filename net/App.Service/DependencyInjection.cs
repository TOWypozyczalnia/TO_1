using App.Data.Interfaces;
using App.Data.Repositories;
using APP.Service;
using App.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddScoped<IRecommendationSerivce, RecommendationService>();
    }
}