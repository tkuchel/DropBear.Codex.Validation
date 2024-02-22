namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
/// Defines a contract for implementing asynchronous validation strategies for specific types.
/// </summary>
/// <typeparam name="T">The type of the object to validate.</typeparam>
public interface IValidationStrategyAsync<T>
{
    /// <summary>
    /// Asynchronously validates an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <param name="context">The instance of <typeparamref name="T"/> to be validated.</param>
    /// <returns>A task that represents the asynchronous validation operation. The task result is true if
    /// the instance passes the validation logic defined in the implementation; otherwise, false.</returns>
    /// <remarks>
    /// This method is suitable for validation scenarios requiring IO-bound operations, such as network requests
    /// or database calls, enabling asynchronous execution to improve application responsiveness.
    /// </remarks>
    Task<bool> ValidateAsync(T context);
}