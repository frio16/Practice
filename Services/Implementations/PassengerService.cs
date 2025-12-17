using Microsoft.EntityFrameworkCore;
using SelfLearning.Data;
using SelfLearning.Domain.Entities;
using SelfLearning.DTOs.Requests;
using SelfLearning.DTOs.Responses;
using SelfLearning.Services.Interfaces;

namespace SelfLearning.Services.Implementations;

public class PassengerService : IPassengerService
{
    private readonly AppDbContext _context;
    private readonly IPnrApiService _pnrApiService;
    private readonly ILogger<PassengerService> _logger;

    public PassengerService(
        AppDbContext context,
        IPnrApiService pnrApiService,
        ILogger<PassengerService> logger)
    {
        _context = context;
        _pnrApiService = pnrApiService;
        _logger = logger;
    }

    public async Task<PassengerTravelShareResponse> CreatePassengerTravelShareAsync(
        CreatePassengerTravelShareRequest request)
    {
        _logger.LogInformation("Processing passenger travel share for TrainNo: {TrainNo}, PNR: {Pnr}", request.TrainNo, request.Pnr);

        // Check if train exists in TrainMaster
        var existingTrain = await _context.Trains
            .FirstOrDefaultAsync(t => t.TrainNo == request.TrainNo);

        bool trainWasCreated = false;

        if (existingTrain == null)
        {
            _logger.LogInformation("Train {TrainNo} not found. Calling PNR API...", request.TrainNo);

            // Call PNR API
            var pnrResponse = await _pnrApiService.GetTrainDetailsByPnrAsync(request.Pnr);

            // Check if train from PNR API response already exists
            existingTrain = await _context.Trains
                .FirstOrDefaultAsync(t => t.TrainNo == pnrResponse.TrainNo);

            if (existingTrain == null)
            {
                // Insert TrainMaster
                var newTrain = new Train
                {
                    TrainNo = pnrResponse.TrainNo,
                    TrainName = pnrResponse.TrainName,
                    LastUpdated = DateTime.UtcNow
                };

                _context.Trains.Add(newTrain);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Created train: {TrainNo} - {TrainName}", newTrain.TrainNo, newTrain.TrainName);
                existingTrain = newTrain;
                trainWasCreated = true;
            }
            else
            {
                _logger.LogInformation("Train {TrainNo} from PNR API already exists", pnrResponse.TrainNo);
            }

            // Insert StationMaster (if stations don't exist)
            var stationCodeToId = new Dictionary<string, int>();

            foreach (var stationInfo in pnrResponse.Stations)
            {
                var existingStation = await _context.Stations
                    .FirstOrDefaultAsync(s => s.StationCode == stationInfo.StationCode);

                if (existingStation == null)
                {
                    var newStation = new Station
                    {
                        StationCode = stationInfo.StationCode,
                        StationName = stationInfo.StationName
                    };

                    _context.Stations.Add(newStation);
                    await _context.SaveChangesAsync();

                    stationCodeToId[stationInfo.StationCode] = newStation.StationId;
                    _logger.LogInformation("Created station: {StationCode} - {StationName}",
                        newStation.StationCode, newStation.StationName);
                }
                else
                {
                    stationCodeToId[stationInfo.StationCode] = existingStation.StationId;
                }
            }

            // Insert RouteStop (only if train was just created and route stops don't exist)
            if (trainWasCreated)
            {
                var existingRouteStops = await _context.RouteStops
                    .AnyAsync(rs => rs.TrainId == existingTrain.TrainId);

                if (!existingRouteStops)
                {
                    foreach (var stationInfo in pnrResponse.Stations.OrderBy(s => s.SequenceNumber))
                    {
                        var routeStop = new RouteStop
                        {
                            TrainId = existingTrain.TrainId,
                            StationId = stationCodeToId[stationInfo.StationCode],
                            SeqNo = stationInfo.SequenceNumber,
                            ArrivalTime = stationInfo.ArrivalTime,
                            DepartureTime = stationInfo.DepartureTime
                        };

                        _context.RouteStops.Add(routeStop);
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Created route stops for train: {TrainNo}", existingTrain.TrainNo);
                }
            }
        }

        // Save Passenger Travel Share
        var passengerTravelShare = new PassengerTravelShare
        {
            Pnr = request.Pnr,
            TrainId = existingTrain.TrainId,
            TravelDate = request.TravelDate,
            Message = request.Message,
            CreatedAt = DateTime.UtcNow
        };

        _context.PassengerTravelShares.Add(passengerTravelShare);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created passenger travel share with ShareId: {ShareId}", passengerTravelShare.ShareId);

        return new PassengerTravelShareResponse
        {
            ShareId = passengerTravelShare.ShareId,
            TrainNo = existingTrain.TrainNo,
            TrainName = existingTrain.TrainName ?? string.Empty,
            Pnr = passengerTravelShare.Pnr,
            TravelDate = passengerTravelShare.TravelDate,
            Message = passengerTravelShare.Message,
            CreatedAt = passengerTravelShare.CreatedAt,
            TrainWasCreated = trainWasCreated
        };
    }
}

