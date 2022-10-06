namespace HousingRepairsSchedulingApi.Dtos.Mcm;

using System;

public record BookAppointmentResponse : McmResponse
{
    public int AppointmentId { get; init; }
    public string AppointmentStatus { get; init; }
    public int Duration { get; init; }
    public DateTime ScheduledDateTime { get; init; }
    public string StatusCode { get; init; }
    public string StatusMessage { get; init; }
}
