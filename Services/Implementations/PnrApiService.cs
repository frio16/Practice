using SelfLearning.DTOs.Responses;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Services.Implementations;

public class PnrApiService : IPnrApiService
{
    private readonly ILogger<PnrApiService> _logger;
    private readonly HttpClient _httpClient;

    public PnrApiService(ILogger<PnrApiService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("PnrApi");
    }

    public async Task<PnrApiResponse> GetTrainDetailsByPnrAsync(string pnr)
    {
        _logger.LogInformation("Calling dummy PNR API for PNR: {Pnr}", pnr);

        // Simulate API delay
        await Task.Delay(500);

        // Dummy response - in real scenario, this would call actual external API
        var dummyResponse = new PnrApiResponse
        {
            TrainNo = "12345",
            TrainName = "Dummy Express",
            Stations = new List<StationInfo>
            {
                new StationInfo
                {
                    StationCode = "NDLS",
                    StationName = "New Delhi",
                    SequenceNumber = 1,
                    ArrivalTime = null,
                    DepartureTime = new TimeOnly(8, 0)
                },
                new StationInfo
                {
                    StationCode = "CNB",
                    StationName = "Kanpur Central",
                    SequenceNumber = 2,
                    ArrivalTime = new TimeOnly(12, 30),
                    DepartureTime = new TimeOnly(12, 45)
                },
                new StationInfo
                {
                    StationCode = "ALD",
                    StationName = "Allahabad Junction",
                    SequenceNumber = 3,
                    ArrivalTime = new TimeOnly(15, 0),
                    DepartureTime = null
                }
            },
            Routes = new List<RouteInfo>
            {
                new RouteInfo
                {
                    SourceStationCode = "NDLS",
                    DestinationStationCode = "ALD"
                }
            }
        };

        _logger.LogInformation("Dummy PNR API response received for PNR: {Pnr}", pnr);
        return dummyResponse;
    }
}


