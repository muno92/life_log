namespace SpeedTest.Resource;

public record NetworkInterface(string InternalIp, string Name, string MacAddr, bool IsVpn, string ExternalIp);
