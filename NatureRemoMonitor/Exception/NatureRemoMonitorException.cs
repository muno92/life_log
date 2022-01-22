using System.Runtime.Serialization;

namespace NatureRemoMonitor.Exception;

public class NatureRemoMonitorException : System.Exception
{
    public NatureRemoMonitorException()
    {
    }

    protected NatureRemoMonitorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NatureRemoMonitorException(string? message) : base(message)
    {
    }

    public NatureRemoMonitorException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
