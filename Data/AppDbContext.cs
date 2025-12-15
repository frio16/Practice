using Microsoft.EntityFrameworkCore;
using SelfLearning.Domain.Entities;

namespace SelfLearning.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Train> Trains => Set<Train>();
    public DbSet<Station> Stations => Set<Station>();
    public DbSet<RouteStop> RouteStops => Set<RouteStop>();
    public DbSet<PassengerTravelShare> PassengerTravelShares => Set<PassengerTravelShare>();
    public DbSet<PassengerProfile> PassengerProfiles => Set<PassengerProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Train>(entity =>
        {
            entity.HasKey(e => e.TrainId);

            entity.HasIndex(e => e.TrainNo)
                  .IsUnique();

            entity.Property(e => e.LastUpdated)
                  .HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId);

            entity.HasIndex(e => e.StationCode)
                  .IsUnique()
                  .HasFilter("\"station_code\" IS NOT NULL");
        });

        modelBuilder.Entity<RouteStop>(entity =>
        {
            entity.HasKey(e => e.RouteStopId);

            entity.HasOne(e => e.Train)
                  .WithMany()
                  .HasForeignKey(e => e.TrainId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Station)
                  .WithMany()
                  .HasForeignKey(e => e.StationId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(e => new { e.TrainId, e.SeqNo })
                  .IsUnique();
        });

        modelBuilder.Entity<PassengerTravelShare>(entity =>
        {
            entity.HasKey(e => e.ShareId);

            entity.HasOne(e => e.Train)
                  .WithMany()
                  .HasForeignKey(e => e.TrainId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<PassengerProfile>(entity =>
        {
            entity.HasKey(e => e.ProfileId);

            entity.HasOne(e => e.Share)
                  .WithMany()
                  .HasForeignKey(e => e.ShareId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("now()");
        });
    }
}

