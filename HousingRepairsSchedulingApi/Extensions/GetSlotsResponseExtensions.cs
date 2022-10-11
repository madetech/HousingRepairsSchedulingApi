namespace HousingRepairsSchedulingApi.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Dtos.Mcm;

public static class GetSlotsResponseExtensions
{
    private static readonly string[] SlotDescriptions = { "ALL", "SR", "Sat AM" };

    // remove days that don't match the criteria in `ShouldKeepDay`
    // take numDaysLimit (5) days
    //
    // remove slots that are not bookable
    // remove slots with no availability
    // remove slots with description in list `SlotDescriptions`
    //
    // Map slots into `AppointmentSlots`
    //
    // Assumptions:
    // * All time slots are going to be 2 hours long in duration. (we shouldn't have any of the Sat AM or ALL descriptions)
    // * The timezone stuff doesn't actually matter
    // * if the above is true, we can just get the description back from the start time of the appointment.
    public static IEnumerable<AppointmentSlot> ToAppointmentSlots(this GetSlotsResponse response, int numDaysLimit,
        DateTime fromDate) =>
        response.SlotDays
            .Where(day => day.ShouldKeepDay(fromDate))
            .Take(numDaysLimit)
            .SelectMany(day =>
                {
                    var date = day.SlotDate;

                    return day.Slots
                        .Where(slot =>
                            slot.AvailableSlotCapacity > 0 && slot.Bookable &&
                            !SlotDescriptions.Contains(slot.Description)).Select(slot =>
                            new AppointmentSlot(date.Add(slot.StartTime), date.Add(slot.EndTime)));
                }
            );

    private static bool HasTwoHourTimeSlot(this SlotDay day) =>
        day.Slots.Any(slot => !SlotDescriptions.Contains(slot.Description));

    private static bool ShouldKeepDay(this SlotDay day, DateTime fromDate)
    {
        if (day.NonBookingDay || day.ResourceCapacity == 0 || day.SlotDate < fromDate.Date)
        {
            return false;
        }

        // Should keep day if it has at least one slot that matches all of the following criteria:
        // * Slot is bookable
        // * Slot has an available capacity > 0
        // * Slot must have a description such as 10:00-12:00 that is not in the list {"ALL", "Sat AM", "SR"}
        return day.Slots.Any(slot =>
            slot.Bookable && slot.AvailableSlotCapacity > 0 && !SlotDescriptions.Contains(slot.Description));
    }
}
