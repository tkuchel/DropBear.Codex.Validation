namespace DropBear.Codex.Validation.ReturnTypes;

/// <summary>
///     Represents the outcome of a validation operation, encapsulating any errors encountered.
/// </summary>
public class ValidationResult
{
    private readonly List<(string Parameter, string ErrorMessage)> _errors = [];

    // Constructor made private to force the use of factory methods for instantiation.
    private ValidationResult() { }

    /// <summary>
    ///     Gets a value indicating whether the validation operation was successful.
    /// </summary>
    public bool IsValid => _errors.Count == 0;

    /// <summary>
    ///     Gets an enumeration of all errors encountered during the validation operation,
    ///     where each error consists of a parameter name and an associated error message.
    /// </summary>
    public IEnumerable<(string Parameter, string ErrorMessage)> Errors => _errors.AsReadOnly();

    /// <summary>
    /// Creates a new instance of ValidationResult. This method is internal and intended for use within the validation framework.
    /// </summary>
    /// <returns>A new instance of the ValidationResult class.</returns>
    internal static ValidationResult New() => new();
    
    /// <summary>
    /// Creates a failure validation result with the specified errors.
    /// </summary>
    /// <param name="errors">A dictionary containing parameter names and their associated error messages.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating the failure, with detailed errors.</returns>
    /// <remarks>
    /// This method allows for aggregating multiple validation errors, where each key in the dictionary represents a parameter name,
    /// and the value is the corresponding error message.
    /// </remarks>
    public static ValidationResult Fail(Dictionary<string, string> errors)
    {
        var result = new ValidationResult();
        foreach (var error in errors)
        {
            result.AddError(error.Key, error.Value);
        }
        return result;
    }

    /// <summary>
    ///     Adds an error to the validation result.
    /// </summary>
    /// <param name="parameter">The name of the parameter related to the error.</param>
    /// <param name="errorMessage">The error message associated with the parameter.</param>
    /// <returns>The same <see cref="ValidationResult" /> instance, allowing for method chaining.</returns>
    public ValidationResult AddError(string parameter, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException("Parameter name cannot be null or whitespace.", nameof(parameter));

        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("Error message cannot be null or whitespace.", nameof(errorMessage));

        _errors.Add((parameter, errorMessage));
        return this;
    }

    /// <summary>
    ///     Creates a successful validation result, indicating no errors were encountered.
    /// </summary>
    /// <returns>A <see cref="ValidationResult" /> indicating a successful validation.</returns>
    public static ValidationResult Success() => new();

    /// <summary>
    ///     Determines whether an error associated with a specific parameter exists.
    /// </summary>
    /// <param name="parameter">The name of the parameter to check for errors.</param>
    /// <returns>True if an error for the specified parameter exists; otherwise, false.</returns>
    public bool HasErrorFor(string parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
            throw new ArgumentException("Parameter name cannot be null or whitespace.", nameof(parameter));

        return _errors.Any(e => e.Parameter == parameter);
    }
}
