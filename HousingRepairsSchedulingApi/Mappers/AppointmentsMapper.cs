namespace HousingRepairsSchedulingApi.Mappers;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Domain;
using Dtos;

public class AppointmentsMapper
{
        // remove days that are not bookable or have no slots
        // remove slots that are not bookable
        // combine into single list
        public static IEnumerable<AppointmentSlot> MapGetSlotsResponse(GetSlotsResponse response) =>
            response.SlotDays.Where(day => day.NonBookingDay == false && day.ResourceCapacity > 0).SelectMany(day =>
            {
                var date = day.SlotDate;

                return day.Slots.Select(slot =>
                    new AppointmentSlot()
                    {
                        StartTime = date,
                        EndTime = date
                    }

                );
            });
}
