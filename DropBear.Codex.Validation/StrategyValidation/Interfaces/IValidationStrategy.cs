namespace DropBear.Codex.Validation.StrategyValidation.Interfaces;

/// <summary>
/// Defines a contract for a validation strategy applicable to a specific type, enabling the encapsulation
/// of validation logic specific to that type.
/// </summary>
/// <typeparam name="T">The type of object this strategy is designed to validate.</typeparam>
public interface IValidationStrategy<T>
{
    /// <summary>
    /// Validates an instance of <typeparamref name="T"/> according to the rules defined in the implementing class.
    /// </summary>
    /// <param name="context">The instance of <typeparamref name="T"/> to validate.</param>
    /// <returns>True if the instance is considered valid according to the validation rules; otherwise, false.</returns>
    /// <remarks>
    /// Implementations should encapsulate all logic necessary for validating instances of <typeparamref name="T"/>,
    /// including property validation, cross-property validation, and enforcement of business rules.
    /// </remarks>
    bool Validate(T context);
}