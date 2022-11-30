using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task7.Application.Interfaces;
using Task7.Persistence.Contexts;

namespace Task7.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                               == "Production" 
            ? configuration["ConnectionStrings:TicTacToeDbConnectionStringProduction"]
            : configuration["ConnectionStrings:TicTacToeDbConnectionStringDevelop"];

        services.AddDbContext<TicTacToeDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<ITicTacToeDbContext, TicTacToeDbContext>();

        return services;
    }
}