namespace NatureRemoMonitor.Exception;

public class NotSupportedException : NatureRemoMonitorException
{
    public NotSupportedException()
    {
    }

    public NotSupportedException(string? message) : base(message)
    {
    }

    public NotSupportedException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
