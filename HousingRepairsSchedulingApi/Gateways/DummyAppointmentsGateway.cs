namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

public class DummyAppointmentsGateway : IAppointmentsGateway
{
    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(SorCode sorCode, AddressUprn addressUprn,
        DateTime? fromDate = null)
    {
        var dateTime = (fromDate ?? DateTime.Today).Date;
        var unorderedAppointments = new List<AppointmentSlot>
        {
            new(dateTime.AddDays(16).AddHours(8), dateTime.AddDays(16).AddHours(12)),
            new(
                dateTime.AddDays(20).AddDays(2).AddHours(12),
                dateTime.AddDays(20).AddDays(2).AddHours(16)
            ),
            new(
                dateTime.AddDays(7).AddDays(7).AddHours(8),
                dateTime.AddDays(7).AddDays(7).AddHours(12)
            ),
            new(
                dateTime.AddDays(1).AddDays(1).AddHours(8),
                dateTime.AddDays(1).AddDays(1).AddHours(12)
            ),
            new(
                dateTime.AddDays(5).AddDays(5).AddHours(12),
                dateTime.AddDays(5).AddDays(5).AddHours(16)
            )
        };
        var orderedAppointments = unorderedAppointments.OrderBy(x => x.StartTime);
        return orderedAppointments;
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
