namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Dtos;
using Factories;
using Flurl;
using Flurl.Http;

public class McmAppointmentGateway : IAppointmentsGateway
{
    private readonly string appointmentManagementUrl;
    private readonly AppointmentsFactory appointmentsFactory;
    private readonly JobIdentifierFactory jobIdentifierFactory;
    private readonly string mcmPassword;
    private readonly string mcmUsername;

    // TODO: Bundle up baseUrl, username, and password into configuration object
    public McmAppointmentGateway(string baseUrl, AppointmentsFactory appointmentsFactory,
        JobIdentifierFactory jobIdentifierFactory, string mcmUsername, string mcmPassword)
    {
        this.appointmentsFactory = appointmentsFactory;
        this.appointmentManagementUrl = baseUrl.AppendPathSegment("/api/AppointmentManagement");
        this.jobIdentifierFactory = jobIdentifierFactory;
        this.mcmUsername = mcmUsername;
        this.mcmPassword = mcmPassword;
    }

    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(string sorCode, string locationId,
        DateTime? fromDate = null)
    {
        // TODO: Error Handling
        var jobIdentifier = this.jobIdentifierFactory.FromSorCode(sorCode);
        var getSlotsRequest = new GetSlotsRequest(jobIdentifier, fromDate ?? DateTime.Today.AddDays(1), locationId);

        var response = await this.appointmentManagementUrl.AppendPathSegment("GetAvailableSlots")
            .WithBasicAuth(this.mcmUsername, this.mcmPassword).PostJsonAsync(getSlotsRequest)
            .ReceiveJson<GetSlotsResponse>();


        return this.appointmentsFactory.FromGetSlotsResponse(response);
    }

    public Task<string> BookAppointment(string bookingReference, string sorCode, string locationId,
        DateTime startDateTime,
        DateTime endDateTime) =>
        throw new NotImplementedException();
}
