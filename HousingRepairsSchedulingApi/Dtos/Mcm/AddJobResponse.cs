namespace HousingRepairsSchedulingApi.Dtos.Mcm;

public record AddJobResponse : McmResponse
{
    public int JobId { get; init; }
    public string StatusCode { get; init; }
    public string StatusMessage { get; init; }
}
