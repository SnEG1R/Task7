using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Task7.Application.Common.UserIdProviders;

namespace Task7.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddSingleton<IUserIdProvider, NameUserIdProvider>();

        return services;
    }
}