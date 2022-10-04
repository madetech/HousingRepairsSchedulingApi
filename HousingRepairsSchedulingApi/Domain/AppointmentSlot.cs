namespace HousingRepairsSchedulingApi.Domain;

using System;
using Ardalis.GuardClauses;

public class AppointmentSlot
{
    public AppointmentSlot(DateTime startTime, DateTime endTime)
    {
        Guard.Against.OutOfRange(endTime, nameof(endTime), startTime, DateTime.MaxValue);
        this.StartTime = startTime;
        this.EndTime = endTime;
    }

    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
}
