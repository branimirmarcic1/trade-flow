namespace BuildingBlocks.Exceptions;

public class ApiResponseException : InternalServerException
{
    public ApiResponseException(string message) : base(message)
    {
    }

    public ApiResponseException(string message, string details) : base(message, details)
    {
    }
}
