using Microsoft.AspNetCore.Mvc;
using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerService _passengerService;
    private readonly ILogger<PassengerController> _logger;

    public PassengerController(
        IPassengerService passengerService,
        ILogger<PassengerController> logger)
    {
        _passengerService = passengerService;
        _logger = logger;
    }

    [HttpPost("travel-share")]
    [ProducesResponseType(typeof(PassengerTravelShareResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PassengerTravelShareResponse>> CreateTravelShare(
        [FromBody] CreatePassengerTravelShareRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _passengerService.CreatePassengerTravelShareAsync(request);
            return CreatedAtAction(nameof(CreateTravelShare), new { id = response.ShareId }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating passenger travel share");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }
}

