using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfLearning.Domain.Entities;

[Table("passenger_travel_share")]
public class PassengerTravelShare
{
    [Key]
    [Column("share_id")]
    public int ShareId { get; set; }

    [Column("passenger_id")]
    public int? PassengerId { get; set; }

    [Required]
    [MaxLength(20)]
    [Column("pnr")]
    public string Pnr { get; set; } = null!;

    [Column("train_id")]
    public int TrainId { get; set; }

    [Column("travel_date")]
    public DateOnly TravelDate { get; set; }

    [MaxLength(500)]
    [Column("message")]
    public string? Message { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public Train Train { get; set; } = null!;
}
