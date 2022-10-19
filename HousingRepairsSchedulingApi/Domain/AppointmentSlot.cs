namespace HousingRepairsSchedulingApi.Domain;

using System;
using Ardalis.GuardClauses;

public class AppointmentSlot
{
    public AppointmentSlot(string id, DateTime startTime, DateTime endTime)
    {
        Guard.Against.OutOfRange(endTime, nameof(endTime), startTime, DateTime.MaxValue);
        this.Id = id;
        this.StartTime = startTime;
        this.EndTime = endTime;
    }

    public string Id { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
}
