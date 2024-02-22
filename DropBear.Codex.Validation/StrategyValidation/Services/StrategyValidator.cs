using DropBear.Codex.Validation.StrategyValidation.Interfaces;

namespace DropBear.Codex.Validation.StrategyValidation.Services;

/// <summary>
///     Manages validation strategies for different types, allowing for both synchronous
///     and asynchronous validation. Strategies must be registered with the manager
///     before they can be applied.
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
        _syncValidationStrategies[typeof(T)] = strategy ?? throw new ArgumentNullException(nameof(strategy), "Validation strategy cannot be null.");
    }

    /// <summary>
    ///     Registers an asynchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the asynchronous validation strategy applies.</typeparam>
    /// <param name="strategy">The asynchronous validation strategy to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the strategy is null.</exception>
    public void RegisterAsyncStrategy<T>(IValidationStrategyAsync<T> strategy)
    {
        _asyncValidationStrategies[typeof(T)] = strategy ?? throw new ArgumentNullException(nameof(strategy), "Validation strategy cannot be null.");
    }

    /// <summary>
    ///     Validates an object of a specific type using the registered validation strategy.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>True if validation is successful; otherwise, false.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no validation strategy is registered for the type.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the context is null.</exception>
    public bool Validate<T>(T context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context), "Validation context cannot be null.");

        if (_syncValidationStrategies.TryGetValue(typeof(T), out var strategy) &&
            strategy is IValidationStrategy<T> typedStrategy)
            return typedStrategy.Validate(context);

        throw new InvalidOperationException($"No validation strategy registered for type {typeof(T).Name}.");
    }

    /// <summary>
    ///     Asynchronously validates an object of a specific type using the registered asynchronous validation strategy.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>
    ///     A task that represents the asynchronous validation operation. The task result contains true if validation is
    ///     successful; otherwise, false.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when no asynchronous validation strategy is registered for the type.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the context is null.</exception>
    public async Task<bool> ValidateAsync<T>(T context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context), "Validation context cannot be null.");

        if (_asyncValidationStrategies.TryGetValue(typeof(T), out var strategy) &&
            strategy is IValidationStrategyAsync<T> typedStrategy)
            return await typedStrategy.ValidateAsync(context);

        throw new InvalidOperationException(
            $"No asynchronous validation strategy registered for type {typeof(T).Name}.");
    }
}