using System.Runtime.Serialization;

namespace NatureRemoMonitor.Exception;

public class NotSupportedException : NatureRemoMonitorException
{
    public NotSupportedException()
    {
    }

    protected NotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotSupportedException(string? message) : base(message)
    {
    }

    public NotSupportedException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
