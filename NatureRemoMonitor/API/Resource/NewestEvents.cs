using System.Text.Json.Serialization;

namespace NatureRemoMonitor.API.Resource;

public record NewestEvents(
    [property: JsonPropertyName("hu")] SensorValue Humidity,
    [property: JsonPropertyName("il")] SensorValue Illumination,
    [property: JsonPropertyName("mo")] SensorValue Movement,
    [property: JsonPropertyName("te")] SensorValue Temperature
);
