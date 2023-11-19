namespace NatureRemoMonitor.Exception;

public class InvalidArgumentException : NatureRemoMonitorException
{
    public InvalidArgumentException()
    {
    }

    public InvalidArgumentException(string? message) : base(message)
    {
    }

    public InvalidArgumentException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
