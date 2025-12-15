using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfLearning.Domain.Entities;

[Table("route_stop")]
public class RouteStop
{
    [Key]
    [Column("route_stop_id")]
    public int RouteStopId { get; set; }

    [Column("train_id")]
    public int TrainId { get; set; }

    [Column("station_id")]
    public int StationId { get; set; }

    [Column("seq_no")]
    public int SeqNo { get; set; }

    [Column("arrival_time")]
    public TimeOnly? ArrivalTime { get; set; }

    [Column("departure_time")]
    public TimeOnly? DepartureTime { get; set; }

    // Navigation properties
    public Train Train { get; set; } = null!;
    public Station Station { get; set; } = null!;
}
