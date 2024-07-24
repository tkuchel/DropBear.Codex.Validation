#region

using DropBear.Codex.Validation.ReturnTypes;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace DropBear.Codex.Validation.Extensions;

public static class ModelStateExtensions
{
    /// <summary>
    ///     Adds errors from a ValidationResult to a ModelStateDictionary.
    /// </summary>
    /// <param name="modelState">The ModelStateDictionary to add errors to.</param>
    /// <param name="validationResult">The ValidationResult containing errors to add.</param>
    /// <param name="keyPrefix">An optional prefix to prepend to the key of each error.</param>
    public static void AddValidationResult(this ModelStateDictionary modelState, ValidationResult validationResult,
        string keyPrefix = "")
    {
        if (validationResult.IsValid)
        {
            return;
        }

        foreach (var error in validationResult.Errors)
        {
            // Assuming error.Parameter is the field name and error.ErrorMessage is the associated message
            // Adjust 'keyPrefix + error.Parameter' if your error collection structure is different
            modelState.AddModelError(keyPrefix + error.Parameter, error.ErrorMessage);
        }
    }
}
