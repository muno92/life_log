namespace SpeedTest.Resource;

public record Output(
    string Type,
    DateTime Timestamp,
    Ping Ping,
    SpeedDetail Download,
    SpeedDetail Upload,
    double PacketLoss,
    string Isp,
    NetworkInterface Interface,
    Server Server,
    Result Result
);
