using System.ComponentModel.DataAnnotations;

namespace SelfLearning.DTOs.Requests;

public class CreatePassengerTravelShareRequest
{
    [Required]
    [StringLength(10)]
    public string TrainNo { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Pnr { get; set; } = string.Empty;

    [Required]
    public DateOnly TravelDate { get; set; }

    [StringLength(500)]
    public string? Message { get; set; }
}

