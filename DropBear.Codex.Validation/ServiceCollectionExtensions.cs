using DropBear.Codex.Validation.CheckValidation.Interfaces;
using DropBear.Codex.Validation.CheckValidation.Services;
using DropBear.Codex.Validation.StrategyValidation.Interfaces;
using DropBear.Codex.Validation.StrategyValidation.Services;
using DropBear.Codex.Validation.StrategyValidation.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace DropBear.Codex.Validation;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidationServices(this IServiceCollection services)
    {
        // register the check validator
        services.AddSingleton<ICheckValidator, CheckValidator>();
        
        // register the open generic type to be resolved for any T at runtime
        services.AddSingleton(typeof(IValidationStrategy<>), typeof(DefaultValidationStrategy<>));
        
        // register the strategy validator
        services.AddSingleton<IStrategyValidator, StrategyValidator>();
        return services;
    }
    
}