using DropBear.Codex.Validation.ReturnTypes;

namespace DropBear.Codex.Validation.CheckValidation.Interfaces;

/// <summary>
///     Defines the contract for services that perform validations and checks on data,
///     ensuring data integrity and conformity to specified rules or patterns.
/// </summary>
public interface ICheckValidator
{
    /// <summary>
    ///     Ensures the specified value is not null.
    /// </summary>
    /// <typeparam name="T">The type of the value being checked.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="parameterName">The name of the parameter being checked, for error messaging.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult NotNull<T>(T value, string parameterName);

    /// <summary>
    ///     Ensures the specified string is within a given range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <param name="min">The minimum allowable value.</param>
    /// <param name="max">The maximum allowable value.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsInRange(int value, string parameterName, int min, int max);

    /// <summary>
    ///     Ensures the specified string is a valid email format.
    /// </summary>
    /// <param name="email">The email string to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsEmail(string email, string parameterName);

    /// <summary>
    ///     Ensures the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsNotNullOrEmpty<T>(IEnumerable<T>? collection, string parameterName);

    /// <summary>
    ///     Ensures the specified string is not null, empty, or whitespace.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsNotNullOrWhitespace(string value, string parameterName);

    /// <summary>
    ///     Ensures the specified string is a valid URL.
    /// </summary>
    /// <param name="url">The URL string to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsUrl(string url, string parameterName);

    /// <summary>
    ///     Ensures the specified value is a valid enumeration value.
    /// </summary>
    /// <typeparam name="TEnum">The type of the enumeration.</typeparam>
    /// <param name="value">The enumeration value to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsValidEnumValue<TEnum>(TEnum value, string parameterName) where TEnum : struct, Enum;

    /// <summary>
    ///     Ensures the specified integer value is greater than a specified minimum value.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <param name="minValue">The minimum value that the integer should exceed.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsGreaterThan(int value, string parameterName, int minValue);

    /// <summary>
    ///     Ensures the specified integer value is less than a specified maximum value.
    /// </summary>
    /// <param name="value">The integer value to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <param name="maxValue">The maximum value that the integer should not exceed.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsLessThan(int value, string parameterName, int maxValue);

    /// <summary>
    ///     Ensures the specified string value consists only of alphanumeric characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsAlphaNumeric(string value, string parameterName);

    /// <summary>
    ///     Ensures the specified type is assignable to a specified base type.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <param name="parameterName">The name of the parameter being checked.</param>
    /// <param name="baseType">The base type to which the specified type should be assignable.</param>
    /// <returns>A <see cref="ValidationResult" /> indicating if the check passes or fails.</returns>
    ValidationResult IsAssignableTo(Type? type, string parameterName, Type? baseType);
}