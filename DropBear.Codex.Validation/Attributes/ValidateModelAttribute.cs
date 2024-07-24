#region

using DropBear.Codex.Validation.ReturnTypes;
using DropBear.Codex.Validation.StrategyValidation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DropBear.Codex.Validation.Attributes;

#pragma warning disable CA1019
public sealed class ValidateModelAttribute(IStrategyValidator validator) : ActionFilterAttribute
#pragma warning restore CA1019
{
    /// <summary>
    ///     Executes before the action method runs and validates the model using registered strategies.
    ///     Adds all validation errors to the ModelState.
    /// </summary>
    /// <param name="context">Filter context for the executing action.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments.Values)
        {
            var argumentType = argument?.GetType();
            if (argumentType == null)
            {
                continue;
            }

            var validateMethod = validator.GetType().GetMethod(nameof(IStrategyValidator.Validate))
                ?.MakeGenericMethod(argumentType);

            // ValidationResult.Validate<T> expects a T, so we pass 'argument'
            if (validateMethod == null)
            {
                continue;
            }

            var validationResult = (ValidationResult)validateMethod.Invoke(validator, new[] { argument })!;

            if (validationResult is not { IsValid: false })
            {
                continue;
            }

            foreach (var error in validationResult.Errors)
                // Assuming error.Parameter is the property name and error.ErrorMessage is the associated message
            {
                context.ModelState.AddModelError(error.Parameter, error.ErrorMessage);
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
