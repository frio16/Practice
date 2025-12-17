namespace SelfLearning.DTOs.Responses;

public class PnrApiResponse
{
    public string TrainNo { get; set; } = string.Empty;
    public string TrainName { get; set; } = string.Empty;
    public List<StationInfo> Stations { get; set; } = new();
    public List<RouteInfo> Routes { get; set; } = new();
}

public class StationInfo
{
    public string StationCode { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public int SequenceNumber { get; set; }
    public TimeOnly? ArrivalTime { get; set; }
    public TimeOnly? DepartureTime { get; set; }
}

public class RouteInfo
{
    public string SourceStationCode { get; set; } = string.Empty;
    public string DestinationStationCode { get; set; } = string.Empty;
}


