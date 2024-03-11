using DropBear.Codex.Validation.ReturnTypes;

namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
/// Defines a contract for a validation strategy applicable to a specific type.
/// </summary>
/// <typeparam name="T">The type of object this strategy is designed to validate.</typeparam>
public interface IValidationStrategy<in T>
{
    /// <summary>
    /// Validates an instance of <typeparamref name="T"/> according to the rules defined in the implementing class.
    /// </summary>
    /// <param name="context">The instance of <typeparamref name="T"/> to validate.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the outcome of the validation.</returns>
    ValidationResult Validate(T context);
}