namespace HousingRepairsSchedulingApi.Dtos;

using System.Collections.Generic;

public class GetSlotsResponse
{
    public List<SlotDay> Days { get; set; }
    public string StatusCode { get; set; }
    public string StatusMessage { get; set; }

}

public class SlotDay
{
   public string SlotDate { get; set; }
   public int ResourceCapacity { get; set; }
   public bool NonBookingDay { get; set; }
   public List<AppointmentSlot> AppointmentSlots { get; set; }
}

public class AppointmentSlot
{
   public string Description { get; set; }
   public string StartTime { get; set; }
   public string EndTime { get; set; }
   public bool Bookable { get; set; }
   public int AvailableSlotCapacity { get; set; }
   public int MaximumSlotCapacity { get; set; }
}
