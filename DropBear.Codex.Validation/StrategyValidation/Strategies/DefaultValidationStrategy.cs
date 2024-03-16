using System.ComponentModel.DataAnnotations;
using DropBear.Codex.Validation.StrategyValidation.Interfaces;
using ValidationResult = DropBear.Codex.Validation.ReturnTypes.ValidationResult;

namespace DropBear.Codex.Validation.StrategyValidation.Strategies;

public class DefaultValidationStrategy<T> : IValidationStrategy<T>
{
    /// <summary>
    ///     Validates an instance of T using reflection to find and evaluate data annotations.
    /// </summary>
    /// <param name="context">The instance of T to validate.</param>
    /// <returns>A ValidationResult indicating the outcome of the validation.</returns>
    public ValidationResult Validate(T context)
    {
        var validationResult = ValidationResult.Success();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes(typeof(ValidationAttribute), inherit: true);
            foreach (ValidationAttribute attribute in attributes)
            {
                var isValid = attribute.IsValid(property.GetValue(context));
                if (isValid) continue;
                var errorMessage = attribute.FormatErrorMessage(property.Name);
                validationResult.AddError(property.Name, errorMessage);
            }
        }

        return validationResult;
    }
}
