namespace NatureRemoMonitor.API.Resource;

public record Device(
    string Id,
    string Name,
    int TemperatureOffset,
    int HumidityOffset,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string FirmwareVersion,
    string MacAddress,
    string BtMacAddress,
    string SerialNumber,
    IEnumerable<User> Users,
    NewestEvents NewestEvents
);
