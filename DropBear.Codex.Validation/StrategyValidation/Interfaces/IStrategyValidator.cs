namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
///     Defines the contract for a Validation Manager that orchestrates the application of validation strategies
///     for different data types, supporting both synchronous and asynchronous validation approaches.
/// </summary>
public interface IStrategyValidator
{
    /// <summary>
    ///     Registers a synchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the validation strategy applies.</typeparam>
    /// <param name="strategy">The validation strategy to register.</param>
    void RegisterStrategy<T>(IValidationStrategy<T> strategy);

    /// <summary>
    ///     Registers an asynchronous validation strategy for a specific type.
    /// </summary>
    /// <typeparam name="T">The type to which the asynchronous validation strategy applies.</typeparam>
    /// <param name="strategy">The asynchronous validation strategy to register.</param>
    void RegisterAsyncStrategy<T>(IValidationStrategyAsync<T> strategy);

    /// <summary>
    ///     Validates an object of a specific type using the registered validation strategy.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>True if the object passes the validation; otherwise, false.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no validation strategy is registered for the specified type.</exception>
    bool Validate<T>(T context);

    /// <summary>
    ///     Asynchronously validates an object of a specific type using the registered asynchronous validation strategy.
    /// </summary>
    /// <typeparam name="T">The type of the object to be validated.</typeparam>
    /// <param name="context">The object to validate.</param>
    /// <returns>
    ///     A task representing the asynchronous validation operation. The task result is true if the object passes the
    ///     validation; otherwise, false.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when no asynchronous validation strategy is registered for the
    ///     specified type.
    /// </exception>
    Task<bool> ValidateAsync<T>(T context);
}