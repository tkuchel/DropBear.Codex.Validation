using DropBear.Codex.Validation.ReturnTypes;

namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
/// Defines a contract for implementing asynchronous validation strategies for specific types.
/// </summary>
/// <typeparam name="T">The type of the object to validate.</typeparam>
public interface IValidationStrategyAsync<in T>
{
    /// <summary>
    /// Asynchronously validates an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <param name="context">The instance of <typeparamref name="T"/> to be validated.</param>
    /// <returns>A task that represents the asynchronous validation operation, yielding a <see cref="ValidationResult"/>.</returns>
    Task<ValidationResult> ValidateAsync(T context);
}