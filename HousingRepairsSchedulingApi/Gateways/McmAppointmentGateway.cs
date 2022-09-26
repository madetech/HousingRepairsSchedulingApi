namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

public class McmAppointmentGateway : IAppointmentsGateway
{
    public Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        //Make JobIdentiers from sorCode
        //Make GetSlotsRequest from JobIdentifiers, locationId and fromDate
        //Send request to MCM
        //Return AppointmentSlots from GetSlotsResponse
        throw new NotImplementedException();
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
