namespace HousingRepairsSchedulingApi.Dtos.Mcm;

using System;

public record BookAppointmentRequest
{
    public string ClientSystemUser { get; init; }
    public int JobId { get; init; }
    public string Trade { get; init; }
    public DateTime AppointmentDateTime { get; init; }
    public string SlotTimeDescription { get; init; }
    public string AppointmentNotes { get; init; }
}
