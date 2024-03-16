using System.Globalization;
using System.Text.RegularExpressions;
using DropBear.Codex.Validation.CheckValidation.Interfaces;
using DropBear.Codex.Validation.ReturnTypes;
using static System.Text.RegularExpressions.RegexOptions;

namespace DropBear.Codex.Validation.CheckValidation.Services;

/// <summary>
///     Provides validation methods to enforce constraints on values. Returns validation results instead of throwing
///     exceptions.
/// </summary>
public partial class CheckValidator : ICheckValidator
{
    private const string EmailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
    private const string AlphanumericPattern = @"^[a-zA-Z0-9]+$";
    private const string PasswordPattern = @"^(?=.*\d)(?=.*\W).+$";

    /// <summary>
    ///     Ensures the specified value is not null.
    /// </summary>
    public ValidationResult NotNull<T>(T value, string parameterName) => value == null
        ? ValidationResult.Fail(
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { parameterName, $"{parameterName} cannot be null." }
            })
        : ValidationResult.Success();

    /// <summary>
    ///     Ensures the specified value is within a given range.
    /// </summary>
    public ValidationResult IsInRange(int value, string parameterName, int min, int max)
    {
        if (value < min || value > max)
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }


    /// <summary>
    ///     Ensures the specified string is a valid email format.
    /// </summary>
    public ValidationResult IsEmail(string email, string parameterName) => !EmailRegex().IsMatch(email)
        ? ValidationResult.Fail(
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { parameterName, $"{parameterName} cannot be null." }
            })
        : ValidationResult.Success();

    // Example for complex validation
    /// <summary>
    ///     Ensures the specified string is not null, empty, or whitespace.
    /// </summary>
    public ValidationResult IsNotNullOrWhitespace(string value, string parameterName) =>
        string.IsNullOrWhiteSpace(value)
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    public ValidationResult IsNotNullOrEmpty<T>(IEnumerable<T>? collection, string parameterName)
    {
        if (collection is null || !collection.Any())
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }

    // Adapting IsUrl to return ValidationResult
    public ValidationResult IsUrl(Uri url, string parameterName) =>
        !Uri.TryCreate(url.ToString(), UriKind.Absolute, out _)
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    // Adapting IsValidEnumValue<TEnum> to return ValidationResult
    public ValidationResult IsValidEnumValue<TEnum>(TEnum value, string parameterName) where TEnum : struct, Enum =>
        !Enum.IsDefined(typeof(TEnum), value)
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    // Adapting IsGreaterThan to return ValidationResult
    public ValidationResult IsGreaterThan(int value, string parameterName, int minValue) =>
        value <= minValue
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    // Adapting IsLessThan to return ValidationResult
    public ValidationResult IsLessThan(int value, string parameterName, int maxValue) =>
        value >= maxValue
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    // Adapting IsAlphaNumeric to return ValidationResult
    public ValidationResult IsAlphaNumeric(string value, string parameterName) =>
        !AlphaNumericRegex().IsMatch(value)
            ? ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                })
            : ValidationResult.Success();

    // Adapting IsAssignableTo to return ValidationResult
    public ValidationResult IsAssignableTo(Type? type, string parameterName, Type? baseType)
    {
        if (type == null || baseType == null || !baseType.IsAssignableFrom(type))
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }

    /// <summary>
    ///     Ensures the specified object is not null.
    /// </summary>
    public static ValidationResult NotNull(object? value, string parameterName) => value == null
        ? ValidationResult.Fail(
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { parameterName, $"{parameterName} cannot be null." }
            })
        : ValidationResult.Success();

    public static ValidationResult IsPasswordSecure(string password, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(password) || !PasswordPatternRegex().IsMatch(password))
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }

    /// <summary>
    ///     Checks if the specified files exist.
    /// </summary>
    public static ValidationResult DoFilesExist(string[]? filePaths, string parameterName)
    {
        if (filePaths is null || filePaths.Length is 0)
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

        foreach (var filePath in filePaths)
            if (!File.Exists(filePath))
                return ValidationResult.Fail(
                    new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        { parameterName, $"{parameterName} cannot be null." }
                    });

        return ValidationResult.Success();
    }

    /// <summary>
    ///     Checks if any fields in the provided object are null.
    /// </summary>
    public static ValidationResult AreAnyFieldsNull<T>(T? obj, string parameterName) where T : class
    {
        if (obj is null)
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
            if (property.GetValue(obj) == null)
                return ValidationResult.Fail(
                    new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        { parameterName, $"{parameterName} cannot be null." }
                    });

        return ValidationResult.Success();
    }

    /// <summary>
    ///     Checks if all fields in the provided object are null.
    /// </summary>
    public static ValidationResult AreAllFieldsNull<T>(T? obj, string parameterName) where T : class
    {
        if (obj is null)
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

        var properties = typeof(T).GetProperties();
        return properties.All(property => property.GetValue(obj) == null)
            ? ValidationResult.Success()
            : ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
    }

    /// <summary>
    ///     Checks if the provided string is a valid GUID.
    /// </summary>
    public static ValidationResult IsGuid(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value) || !Guid.TryParse(value, out _))
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }

    /// <summary>
    ///     Checks if the provided string represents a valid date.
    /// </summary>
    public static ValidationResult IsDate(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            !DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });
        return ValidationResult.Success();
    }

    /// <summary>
    ///     Ensures the specified integer value is positive.
    /// </summary>
    public static ValidationResult IsPositive(int value, string parameterName) => value > 0
        ? ValidationResult.Success()
        : ValidationResult.Fail(
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { parameterName, $"{parameterName} cannot be null." }
            });

    /// <summary>
    ///     Ensures the specified integer value is negative.
    /// </summary>
    public static ValidationResult IsNegative(int value, string parameterName) => value < 0
        ? ValidationResult.Success()
        : ValidationResult.Fail(
            new Dictionary<string, string>(StringComparer.Ordinal)
            {
                { parameterName, $"{parameterName} cannot be null." }
            });

    /// <summary>
    ///     Ensures the specified boolean value is true.
    /// </summary>
    public static ValidationResult IsTrue(bool value, string parameterName) =>
        value
            ? ValidationResult.Success()
            : ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

    /// <summary>
    ///     Ensures the specified boolean value is false.
    /// </summary>
    public static ValidationResult IsFalse(bool value, string parameterName) =>
        !value
            ? ValidationResult.Success()
            : ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

    /// <summary>
    ///     Ensures all fields in the provided object are either null or have their default value.
    /// </summary>
    public static ValidationResult AreAllFieldsNullOrDefault<T>(T? obj, string parameterName) where T : class
    {
        if (obj is null)
            return ValidationResult.Fail(
                new Dictionary<string, string>(StringComparer.Ordinal)
                {
                    { parameterName, $"{parameterName} cannot be null." }
                });

        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            if (value is not null && !IsDefaultValue(property.PropertyType, value))
                return ValidationResult.Fail(
                    new Dictionary<string, string>(StringComparer.Ordinal)
                    {
                        { parameterName, $"{parameterName} cannot be null." }
                    });
        }

        return ValidationResult.Success();
    }

    /// <summary>
    ///     Checks if a value is the default for its type.
    /// </summary>
    private static bool IsDefaultValue(Type type, object? value) =>
        (type.IsValueType && Activator.CreateInstance(type)!.Equals(value)) || value is null;

    [GeneratedRegex(EmailPattern, ExplicitCapture, 1000)]
    private static partial Regex EmailRegex();

    [GeneratedRegex(AlphanumericPattern, None, 1000)]
    private static partial Regex AlphaNumericRegex();

    [GeneratedRegex(PasswordPattern, None, 1000)]
    private static partial Regex PasswordPatternRegex();
}
