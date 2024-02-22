using DropBear.Codex.Validation.CheckValidation.Interfaces;
using DropBear.Codex.Validation.CheckValidation.Services;
using DropBear.Codex.Validation.StrategyValidation.Interfaces;
using DropBear.Codex.Validation.StrategyValidation.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DropBear.Codex.Validation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        services.AddSingleton<ICheckValidator, CheckValidator>();
        services.AddSingleton<IStrategyValidator, StrategyValidator>();
        return services;
    }
    
}