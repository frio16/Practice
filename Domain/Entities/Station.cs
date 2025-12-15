using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfLearning.Domain.Entities;

[Table("station_master")]
public class Station
{
    [Key]
    [Column("station_id")]
    public int StationId { get; set; }

    [MaxLength(20)]
    [Column("station_code")]
    public string? StationCode { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("station_name")]
    public string StationName { get; set; } = null!;
}
