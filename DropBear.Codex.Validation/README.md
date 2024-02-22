# Validation Framework Project

This project provides a flexible and extensible validation framework designed to facilitate both synchronous and
asynchronous validation across various types within an application. It leverages a strategy pattern to allow for the
dynamic registration and application of validation logic tailored to specific data types or business rules.

## Features

- **Generic Validation Strategies**: Define custom validation logic for any data type through the implementation
  of `IValidationStrategy<T>` and `IValidationStrategyAsync<T>` interfaces.
- **Asynchronous Support**: Supports asynchronous validation scenarios, making it suitable for I/O-bound validation
  tasks such as database lookups or API calls.
- **Strategy Registration**: Allows for the runtime registration of validation strategies, enabling a highly flexible
  validation system that can be adapted to changing requirements.
- **Type-Safe Validation**: Ensures that validation strategies are applied in a type-safe manner, reducing runtime
  errors and improving reliability.

## Components

### Interfaces

- **`IValidationStrategy<T>`**: Defines a contract for validation strategies applicable to a specific type,
  encapsulating logic necessary for validating instances of `T`.
- **`IValidationStrategyAsync<T>`**: Similar to `IValidationStrategy<T>` but for asynchronous validation operations,
  returning `Task<bool>` to represent the validation outcome.
- **`IValidationManager`**: Orchestrates the application of registered validation strategies, providing methods to
  validate objects either synchronously or asynchronously.

### Implementations

- **`ValidationManager`**: Implements the `IValidationManager` interface, managing the registration and invocation of
  both synchronous and asynchronous validation strategies. It supports dynamic validation scenarios across different
  parts of an application.

## Usage

1. **Define Validation Strategies**: Implement the `IValidationStrategy<T>` or `IValidationStrategyAsync<T>` interface
   to define validation logic for your specific types.
2. **Register Strategies**: Use the `ValidationManager` to register your validation strategies at runtime, associating
   them with the types they are designed to validate.
3. **Validate Objects**: Invoke the `Validate` or `ValidateAsync` method on the `ValidationManager`, passing the object
   to be validated. The manager will apply the appropriate validation strategy based on the object's type.

## Example

```csharp
// Define a validation strategy for a custom type
public class MyTypeValidationStrategy : IValidationStrategy<MyType>
{
    public bool Validate(MyType context)
    {
        // Implement validation logic
        return context.SomeProperty != null;
    }
}

// Register the strategy with the validation manager
var validationManager = new ValidationManager();
validationManager.RegisterStrategy(new MyTypeValidationStrategy());

// Validate an instance of MyType
var myTypeInstance = new MyType();
bool isValid = validationManager.Validate(myTypeInstance);
```

## Extending the Framework

- **Custom Validation Logic**: Implement additional validation strategy interfaces for new data types as needed.
- **Asynchronous Validation**: Leverage `IValidationStrategyAsync<T>` for validations requiring asynchronous execution,
  such as network or database calls.

## Conclusion

This validation framework offers a robust foundation for implementing comprehensive validation logic throughout an
application, ensuring data integrity and adherence to business rules. Its flexible design allows it to be easily
extended and adapted to meet the evolving needs of complex software systems.