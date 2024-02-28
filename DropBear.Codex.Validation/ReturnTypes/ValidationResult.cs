namespace DropBear.Codex.Validation.ReturnTypes;

/// <summary>
///     Encapsulates the outcome of a validation operation.
/// </summary>
public class ValidationResult
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ValidationResult" /> class.
    /// </summary>
    /// <param name="isValid">If the validation is valid.</param>
    /// <param name="errorMessage">The error message for validation failure.</param>
    private ValidationResult(bool isValid, string errorMessage = "")
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Gets a value indicating whether the validation was successful.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool IsValid { get; }

    /// <summary>
    ///     Gets the error message associated with a validation failure.
    /// </summary>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string ErrorMessage { get; }

    /// <summary>
    ///     Represents a successful validation result.
    /// </summary>
    public static ValidationResult Success => new(isValid: true);

    /// <summary>
    ///     Creates a failure validation result with the specified error message.
    /// </summary>
    /// <param name="errorMessage">The error message for validation failure.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating the failure.</returns>
    public static ValidationResult Fail(string errorMessage)
    {
        return new ValidationResult(isValid: false, errorMessage);
    }
}