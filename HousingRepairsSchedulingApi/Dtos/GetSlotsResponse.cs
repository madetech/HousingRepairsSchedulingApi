namespace HousingRepairsSchedulingApi.Dtos;

using System;
using System.Collections.Generic;

public class GetSlotsResponse
{
    public List<SlotDay> SlotDays { get; set; }
    public string StatusCode { get; set; }
    public string StatusMessage { get; set; }
}

public class SlotDay
{
    public DateTime SlotDate { get; set; }
    public int ResourceCapacity { get; set; }
    public bool NonBookingDay { get; set; }

    public List<Slot> Slots { get; set; }
}

public class Slot
{
    public string Description { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool Bookable { get; set; }
    public int AvailableSlotCapacity { get; set; }
    public int MaximumSlotCapacity { get; set; }
}
