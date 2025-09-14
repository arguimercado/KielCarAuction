using FluentResults;

namespace BuildingBlocks.Commons.Errors;

public class ValidationErrorResult : Error
{
    public ValidationErrorResult(string message, List<string> propertyNames) : base(message)
    {
        PropertyNames = propertyNames;
        Metadata.Add("PropertyNames", propertyNames);
    }
    public List<string> PropertyNames { get; } = new List<string>();
    public ValidationErrorResult(string message) : base(message)
    {
        Metadata.Add("StatusCode", 400);
    }
}
