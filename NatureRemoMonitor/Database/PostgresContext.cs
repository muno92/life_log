using Microsoft.EntityFrameworkCore;

namespace NatureRemoMonitor.Database;

public class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Device> Devices { get; set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public virtual DbSet<SensorValue> SensorValues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONFIGURATION");
        if (connectionString == null)
        {
            throw new InvalidOperationException("DATABASE_CONFIGURATION is not set");
        }

        optionsBuilder.UseNpgsql(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("devices_pkey");

            entity.ToTable("devices");

            entity.Property(e => e.Id)
                .ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<SensorValue>(entity =>
        {
            entity.HasKey(d => d.Id).HasName("sensor_values_pkey");

            entity.ToTable("sensor_values");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()");

            entity.HasOne(d => d.Device).WithMany()
                .HasForeignKey(d => d.DeviceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sensor_values_device_id_fkey");
        });
    }
}
