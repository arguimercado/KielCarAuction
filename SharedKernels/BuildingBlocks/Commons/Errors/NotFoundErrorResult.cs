using FluentResults;

namespace BuildingBlocks.Commons.Errors
{
    public class NotFoundErrorResult : Error
    {
        public NotFoundErrorResult(string message) : base(message)
        {
            Metadata.Add("ErrorType", "NotFound");
            Metadata.Add("StatusCode", 404);
        }
        public NotFoundErrorResult(string message, string key) : base(message)
        {
            Metadata.Add("ErrorType", "NotFound");
            Metadata.Add("StatusCode", 404);
            Metadata.Add("Key", key);
        }
    }
}
