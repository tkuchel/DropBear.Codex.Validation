#region

using DropBear.Codex.Validation.ReturnTypes;

#endregion

namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
///     Defines the contract for a Strategy Validator that manages synchronous and asynchronous validation strategies for
///     different types.
/// </summary>
public interface IStrategyValidator
{
    /// <summary>
    ///     Registers a synchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the validation strategy applies.</typeparam>
    /// <param name="strategy">The validation strategy to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the strategy is null.</exception>
    void RegisterStrategy<T>(IValidationStrategy<T> strategy);

    /// <summary>
    ///     Registers an asynchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the asynchronous validation strategy applies.</typeparam>
    /// <param name="strategy">The asynchronous validation strategy to register.</param>
    /// <exception cref="ArgumentNullException">Thrown if the strategy is null.</exception>
    void RegisterAsyncStrategy<T>(IValidationStrategyAsync<T> strategy);

    /// <summary>
    ///     Validates an object of a specific type using both registered custom validation strategies and a default validation
    ///     strategy.
    ///     Applies custom strategies first, followed by the default strategy, to ensure comprehensive validation coverage.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>
    ///     A ValidationResult indicating the outcome of the validation, aggregating results from both custom and default
    ///     strategies.
    /// </returns>
    ValidationResult Validate<T>(T context);

    /// <summary>
    ///     Asynchronously validates an object of a specific type using both registered custom asynchronous validation
    ///     strategies and a default validation strategy.
    ///     Applies custom strategies first, followed by the default strategy, to ensure comprehensive validation coverage.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate. Must not be null.</param>
    /// <returns>
    ///     A task representing the asynchronous validation operation, yielding a ValidationResult that aggregates errors
    ///     from both custom and default strategies.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the validation context is null.</exception>
    Task<ValidationResult> ValidateAsync<T>(T context);
}
