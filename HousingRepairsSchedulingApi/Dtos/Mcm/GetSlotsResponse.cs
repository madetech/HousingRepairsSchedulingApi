namespace HousingRepairsSchedulingApi.Dtos.Mcm;

using System;
using System.Collections.Generic;

public record GetSlotsResponse : McmResponse
{
    public List<SlotDay> SlotDays { get; init; }
    public string StatusCode { get; init; }
    public string StatusMessage { get; init; }
}

public record SlotDay
{
    public DateTime SlotDate { get; init; }
    public int ResourceCapacity { get; init; }
    public bool NonBookingDay { get; init; }

    public List<Slot> Slots { get; init; }
}

public record Slot
{
    public string Description { get; init; }
    public TimeSpan StartTime { get; init; }
    public TimeSpan EndTime { get; init; }
    public bool Bookable { get; init; }
    public int AvailableSlotCapacity { get; init; }
    public int MaximumSlotCapacity { get; init; }
}
