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

    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        var jobCodes = this.jobCodesFactory.FromSorCode(sorCode);
        var getSlotsRequest = new GetSlotsRequest(jobCodes, fromDate ?? DateTime.Today.AddDays(1), locationId);

        var response = await this.appointmentManagementUrl.AppendPathSegment("GetAvailableSlots")
            .WithBasicAuth(this.mcmConfiguration.Username, this.mcmConfiguration.Password)
            .PostJsonAsync(getSlotsRequest)
            .ReceiveJson<GetSlotsResponse>();

        if (response.StatusCode != "1")
        {
            throw new McmRequestError(response.StatusCode, response.StatusMessage);
        }

        return this.appointmentsFactory.FromGetSlotsResponse(response);
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
