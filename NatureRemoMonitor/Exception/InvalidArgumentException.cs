using System.Runtime.Serialization;

namespace NatureRemoMonitor.Exception;

public class InvalidArgumentException : NatureRemoMonitorException
{
    public InvalidArgumentException()
    {
    }

    protected InvalidArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidArgumentException(string? message) : base(message)
    {
    }

    public InvalidArgumentException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
