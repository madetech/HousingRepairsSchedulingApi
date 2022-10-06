namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Configuration;
using Domain;
using Dtos.Mcm;
using Exceptions;
using Extensions;
using Flurl;
using Flurl.Http;
using Helpers;

public class McmAppointmentGateway : IAppointmentsGateway
{
    private static readonly int NumDaysLimit = 5;
    private readonly string appointmentManagementUrl;
    private readonly IJobCodesMapper jobCodesMapper;
    private readonly string jobManagementUrl;
    private readonly McmConfiguration mcmConfiguration;
    private readonly McmRequestFactory mcmRequestFactory;

    public McmAppointmentGateway(
        McmConfiguration mcmConfiguration,
        IJobCodesMapper jobCodesMapper,
        McmRequestFactory mcmRequestFactory
    )
    {
        this.appointmentManagementUrl =
            mcmConfiguration.BaseUrl.AppendPathSegment("/api/AppointmentManagement").ToString();
        this.jobManagementUrl = mcmConfiguration.BaseUrl.AppendPathSegment("/api/JobManagement").ToString();
        this.mcmConfiguration = mcmConfiguration;
        this.jobCodesMapper = jobCodesMapper;
        this.mcmRequestFactory = mcmRequestFactory;
    }

    // Assumptions:
    // * We can just hand in 15 `DaysAround` and that'll be fine. (Might require some tweaking)
    // * PriorityCode, ExpenditureCode, 2 hour time slots etc. _can_ just be hardcoded.
    public async Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments(SorCode sorCode, AddressUprn addressUprn,
        DateTime? fromDate = null)
    {
        var earliestDate = fromDate ?? DateTime.Today.AddDays(1).Date;
        var jobCodes = this.jobCodesMapper.FromSorCode(sorCode);
        var getSlotsRequest = new GetSlotsRequest(jobCodes, earliestDate, addressUprn);

        var response = await this.appointmentManagementUrl.AppendPathSegment("GetAvailableSlots")
            .WithBasicAuth(this.mcmConfiguration.Username, this.mcmConfiguration.Password)
            .PostJsonAsync(getSlotsRequest)
            .ReceiveJson<GetSlotsResponse>();

        this.CheckMcmResponse(response);

        return response.ToAppointmentSlots(NumDaysLimit, earliestDate);
    }

    public async Task<string> BookAppointment(string bookingReference, SorCode sorCode, AddressUprn addressUprn,
        AppointmentSlot appointmentSlot, Contact contact, string jobDescription)
    {
        var jobCodes = this.jobCodesMapper.FromSorCode(sorCode);

        var jobId = await this.AddJob(bookingReference, jobCodes, addressUprn, contact, jobDescription);

        return bookingReference;
    }


    private async Task<int> AddJob(
        string bookingReference,
        JobCodes jobCodes,
        AddressUprn addressUprn,
        Contact contact,
        string jobDescription
    )
    {
        var addJobRequest =
            this.mcmRequestFactory.AddJobRequest(bookingReference, jobCodes, addressUprn, contact, jobDescription);

        var response = await this.jobManagementUrl.AppendPathSegment("AddJob")
            .WithBasicAuth(this.mcmConfiguration.Username, this.mcmConfiguration.Password)
            .PostJsonAsync(addJobRequest)
            .ReceiveJson<AddJobResponse>();

        this.CheckMcmResponse(response);

        return response.JobId;
    }

    private void CheckMcmResponse(McmResponse response)
    {
        if (response.StatusCode != "1")
        {
            throw new McmRequestError(response.StatusCode, response.StatusMessage);
        }
    }
}
