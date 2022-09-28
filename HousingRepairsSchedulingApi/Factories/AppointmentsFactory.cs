namespace HousingRepairsSchedulingApi.Factories;

using System.Collections.Generic;
using System.Linq;
using Domain;
using Dtos;

public class AppointmentsFactory
{
    // remove days that are not bookable or have no slots
    //
    // remove slots that are not bookable
    // (this means looking at the bookable flag and the available slot capacity).
    //
    // combine into single list
    public IEnumerable<AppointmentSlot> FromGetSlotsResponse(GetSlotsResponse response) =>
        response.SlotDays.Where(day => day.NonBookingDay == false && day.ResourceCapacity > 0).SelectMany(day =>
        {
            var date = day.SlotDate;

            return day.Slots.Where(slot => slot.AvailableSlotCapacity > 0 && slot.Bookable).Select(slot =>
                new AppointmentSlot { StartTime = date.Add(slot.StartTime), EndTime = date.Add(slot.EndTime) }
            );
        });
}
