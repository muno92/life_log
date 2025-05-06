using System.ComponentModel.DataAnnotations.Schema;

namespace NatureRemoMonitor.Database;

public class Device
{
    [Column("id")] public Guid Id { get; set; }

    [Column("name")] public string Name { get; set; } = null!;

    [Column("created_at")] public DateTime CreatedAt { get; set; }
}
