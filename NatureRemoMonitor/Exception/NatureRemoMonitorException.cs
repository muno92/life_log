namespace NatureRemoMonitor.Exception;

public class NatureRemoMonitorException : System.Exception
{
    public NatureRemoMonitorException()
    {
    }

    public NatureRemoMonitorException(string? message) : base(message)
    {
    }

    public NatureRemoMonitorException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
