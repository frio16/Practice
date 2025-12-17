using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;

namespace SelfLearning.Services.Interfaces;

public interface IPassengerService
{
    Task<PassengerTravelShareResponse> CreatePassengerTravelShareAsync(CreatePassengerTravelShareRequest request);
}


