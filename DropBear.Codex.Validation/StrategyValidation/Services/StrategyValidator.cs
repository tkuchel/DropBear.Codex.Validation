using DropBear.Codex.Validation.StrategyValidation.Interfaces;
using DropBear.Codex.Validation.StrategyValidation.Strategies;
using ValidationResult = DropBear.Codex.Validation.ReturnTypes.ValidationResult;

namespace DropBear.Codex.Validation.StrategyValidation.Services;

/// <summary>
///     Manages validation strategies for different types, allowing for both synchronous and asynchronous validation.
///     Strategies must be registered with the manager before they can be applied. If no strategy is registered for a type,
///     a default reflection-based validation strategy is used.
/// </summary>
public class StrategyValidator : IStrategyValidator
{
    private readonly Dictionary<Type, object> _asyncValidationStrategies = new();
    private readonly Dictionary<Type, object> _syncValidationStrategies = new();

    /// <summary>
    ///     Registers a synchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the validation strategy applies.</typeparam>
    /// <param name="strategy">The validation strategy to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the strategy is null.</exception>
    public void RegisterStrategy<T>(IValidationStrategy<T> strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        _syncValidationStrategies[typeof(T)] = strategy;
    }

    /// <summary>
    ///     Registers an asynchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the asynchronous validation strategy applies.</typeparam>
    /// <param name="strategy">The asynchronous validation strategy to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the strategy is null.</exception>
    public void RegisterAsyncStrategy<T>(IValidationStrategyAsync<T> strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        _asyncValidationStrategies[typeof(T)] = strategy;
    }

    /// <summary>
    ///     Validates an object of a specific type using both registered custom validation strategies and the default
    ///     validation strategy.
    ///     Custom strategies are applied first, followed by the default strategy to ensure comprehensive validation coverage.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>
    ///     A ValidationResult indicating the outcome of the validation, aggregating results from both custom and default
    ///     strategies.
    /// </returns>
    public ValidationResult Validate<T>(T context)
    {
        if (context is null) throw new ArgumentNullException(nameof(context), "Validation context cannot be null.");

        var validationResult = ValidationResult.New();
        var defaultStrategy = new DefaultValidationStrategy<T>();
        var defaultValidationResult = defaultStrategy.Validate(context);

        // Aggregate errors from the default validation result
        foreach (var error in defaultValidationResult.Errors)
            validationResult.AddError(error.Parameter, error.ErrorMessage);

        // If a custom strategy is registered, apply it and aggregate its errors as well
        if (!_syncValidationStrategies.TryGetValue(typeof(T), out var strategy) ||
            strategy is not IValidationStrategy<T> typedStrategy) return validationResult;
        {
            var customValidationResult = typedStrategy.Validate(context);
            foreach (var error in customValidationResult.Errors)
                validationResult.AddError(error.Parameter, error.ErrorMessage);
        }

        return validationResult;
    }

    /// <summary>
    ///     Asynchronously validates an object of a specific type using both registered custom asynchronous validation
    ///     strategies and the default validation strategy.
    ///     Custom strategies are applied first, followed by the default strategy to ensure comprehensive validation coverage.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate. Must not be null.</param>
    /// <returns>
    ///     A task that represents the asynchronous validation operation, yielding a ValidationResult that aggregates
    ///     errors from both custom and default strategies.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the validation context is null.</exception>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if no asynchronous validation strategy is registered for the type
    ///     and default asynchronous validation is not supported.
    /// </exception>
    public async Task<ValidationResult> ValidateAsync<T>(T context)
    {
        if (context is null) throw new ArgumentNullException(nameof(context), "Validation context cannot be null.");

        var validationResult = ValidationResult.New();
        var hasCustomStrategy = _asyncValidationStrategies.TryGetValue(typeof(T), out var strategy);
        var defaultStrategy =
            new DefaultValidationStrategy<T>(); // Assumes DefaultValidationStrategy supports async; adjust as necessary.

        // Apply custom strategy if available
        if (hasCustomStrategy && strategy is IValidationStrategyAsync<T> typedStrategy)
        {
            var customValidationResult = await typedStrategy.ValidateAsync(context).ConfigureAwait(false);
            foreach (var error in customValidationResult.Errors)
                validationResult.AddError(error.Parameter, error.ErrorMessage);
        }

        // Apply default strategy
        var defaultValidationResult =
            await Task.Run(() =>
                defaultStrategy
                    .Validate(context)).ConfigureAwait(false);
        foreach (var error in defaultValidationResult.Errors)
            validationResult.AddError(error.Parameter, error.ErrorMessage);

        return validationResult;
    }
}
