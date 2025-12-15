using SelfLearning.DTOs.Responses;

namespace SelfLearning.Services.Interfaces;

public interface IPnrApiService
{
    Task<PnrApiResponse> GetTrainDetailsByPnrAsync(string pnr);
}

