namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration;
using Domain;
using Dtos;
using Exceptions;
using Factories;
using Flurl;
using Flurl.Http;

public class McmAppointmentGateway : IAppointmentsGateway
{
    private static readonly int numDaysLimit = 5;
    private readonly Url appointmentManagementUrl;
    private readonly AppointmentsFactory appointmentsFactory;
    private readonly JobCodesFactory jobCodesFactory;
    private readonly McmConfiguration mcmConfiguration;

    public McmAppointmentGateway(McmConfiguration mcmConfiguration, AppointmentsFactory appointmentsFactory,
        JobCodesFactory jobCodesFactory)
    {
        this.appointmentsFactory = appointmentsFactory;
        this.appointmentManagementUrl = mcmConfiguration.BaseUrl.AppendPathSegment("/api/AppointmentManagement");
        this.jobCodesFactory = jobCodesFactory;
        this.mcmConfiguration = mcmConfiguration;
    }

    // TODO: Make sure that we are only extracting 5 days worth of appointment slots
    // Assumptions:
    // * We can just hand in 15 `DaysAround` and that'll be fine. (Might require some tweaking)
    // * PriorityCode, ExpenditureCode, 2 hour time slots etc. _can_ just be hardcoded.
    // * We should be able to modify the `AppointmentsFactory` to take in a limit and a from date and get 5 days
    // worth of time slots relatively easily.
    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        var earliestDate = fromDate ?? DateTime.Today.AddDays(1).Date;
        var jobCodes = this.jobCodesFactory.FromSorCode(sorCode);
        var getSlotsRequest = new GetSlotsRequest(jobCodes, earliestDate, locationId);

        var response = await this.appointmentManagementUrl.AppendPathSegment("GetAvailableSlots")
            .WithBasicAuth(this.mcmConfiguration.Username, this.mcmConfiguration.Password)
            .PostJsonAsync(getSlotsRequest)
            .ReceiveJson<GetSlotsResponse>();

        if (response.StatusCode != "1")
        {
            throw new McmRequestError(response.StatusCode, response.StatusMessage);
        }

        return this.appointmentsFactory.FromGetSlotsResponse(response, numDaysLimit, earliestDate);
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
