using System;

namespace HousingRepairsSchedulingApi.Dtos.Hro;

public record AppointmentDto
{
    public string Id { get; init; }
    public DateTime Date { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
}