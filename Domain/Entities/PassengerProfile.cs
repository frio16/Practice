using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfLearning.Domain.Entities;

[Table("passenger_profile")]
public class PassengerProfile
{
    [Key]
    [Column("profile_id")]
    public int ProfileId { get; set; }

    [Column("share_id")]
    public int ShareId { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [MaxLength(20)]
    [Column("gender")]
    public string? Gender { get; set; }

    [Column("interests")]
    public string? Interests { get; set; }

    [MaxLength(100)]
    [Column("travel_purpose")]
    public string? TravelPurpose { get; set; }

    [MaxLength(100)]
    [Column("preference")]
    public string? Preference { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public PassengerTravelShare Share { get; set; } = null!;
}
