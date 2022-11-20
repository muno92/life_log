namespace SpeedTest.Resource;

public record ServerList(string Type, DateTime Timestamp, IEnumerable<Server> Servers);
