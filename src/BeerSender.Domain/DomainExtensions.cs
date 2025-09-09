using BeerSender.Domain.Boxes;
using BeerSender.Domain.Boxes.Commands;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace BeerSender.Domain;

public static class DomainExtensions
{
    public static void RegisterDomain(this IServiceCollection services)
    {
        services.AddScoped<CommandRouter>();

        services.AddTransient<ICommandHandler<CreateBox>, CreateBoxHandler>();
        services.AddTransient<ICommandHandler<AddShippingLabel>, AddLabelHandler>();
        services.AddTransient<ICommandHandler<AddBottle>, AddBottleHandler>();
    }
    
    public static void ApplyDomainConfig(this StoreOptions options)
    {
        
    }
    
    public static void AddProjections(this StoreOptions options)
    {
        options.Projections.Add<BoxContentProjection>(ProjectionLifecycle.Async);
    }
}