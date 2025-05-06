using System.ComponentModel.DataAnnotations.Schema;

namespace NatureRemoMonitor.Database;

public partial class SensorValue
{
    [Column("id")] public Int64 Id { get; set; }

    [Column("device_id")] public Guid DeviceId { get; set; }

    [Column("temperature")] public double Temperature { get; set; }

    [Column("humidity")] public double Humidity { get; set; }

    [Column("illumination")] public double Illumination { get; set; }

    [Column("created_at")] public DateTime CreatedAt { get; set; }

    public virtual Device Device { get; set; } = null!;
}
