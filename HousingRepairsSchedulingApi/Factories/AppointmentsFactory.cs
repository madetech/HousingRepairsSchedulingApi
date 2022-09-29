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
    //
    // Assumptions:
    // * All time slots are going to be 2 hours long in duration. (we shouldn't have any of the Sat AM or ALL descriptions)
    // * The timezone stuff doesn't actually matter
    // * if the above is true, we can just get the description back from the start time of the appointment.
    public IEnumerable<AppointmentSlot> FromGetSlotsResponse(GetSlotsResponse response) =>
        response.SlotDays.Where(day => day.NonBookingDay == false && day.ResourceCapacity > 0).SelectMany(day =>
        {
            var date = day.SlotDate;

            return day.Slots.Where(slot => slot.AvailableSlotCapacity > 0 && slot.Bookable).Select(slot =>
                new AppointmentSlot { StartTime = date.Add(slot.StartTime), EndTime = date.Add(slot.EndTime) }
            );
        });
}
