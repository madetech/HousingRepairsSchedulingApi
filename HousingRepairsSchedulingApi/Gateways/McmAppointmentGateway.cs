namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration;
using Domain;
using Dtos;
using Exceptions;
using Extensions;
using Flurl;
using Flurl.Http;
using Helpers;

public class McmAppointmentGateway : IAppointmentsGateway
{
    private static readonly int NumDaysLimit = 5;
    private readonly Url appointmentManagementUrl;
    private readonly IJobCodesMapper jobCodesMapper;
    private readonly McmConfiguration mcmConfiguration;

    public McmAppointmentGateway(McmConfiguration mcmConfiguration, IJobCodesMapper jobCodesMapper)
    {
        this.appointmentManagementUrl = mcmConfiguration.BaseUrl.AppendPathSegment("/api/AppointmentManagement");
        this.mcmConfiguration = mcmConfiguration;
        this.jobCodesMapper = jobCodesMapper;
    }

    // Assumptions:
    // * We can just hand in 15 `DaysAround` and that'll be fine. (Might require some tweaking)
    // * PriorityCode, ExpenditureCode, 2 hour time slots etc. _can_ just be hardcoded.
    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        var earliestDate = fromDate ?? DateTime.Today.AddDays(1).Date;
        var jobCodes = this.jobCodesMapper.FromSorCode(sorCode);
        var getSlotsRequest = new GetSlotsRequest(jobCodes, earliestDate, locationId);

        var response = await this.appointmentManagementUrl.AppendPathSegment("GetAvailableSlots")
            .WithBasicAuth(this.mcmConfiguration.Username, this.mcmConfiguration.Password)
            .PostJsonAsync(getSlotsRequest)
            .ReceiveJson<GetSlotsResponse>();

        if (response.StatusCode != "1")
        {
            throw new McmRequestError(response.StatusCode, response.StatusMessage);
        }

        return response.ToAppointmentSlots(NumDaysLimit, earliestDate);
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
