namespace HousingRepairsSchedulingApi.Controllers;

using System;
using System.Threading.Tasks;
using Domain;
using Dtos.Hro;
using Extensions;
using Microsoft.AspNetCore.Mvc;
using Sentry;
using UseCases;

[ApiController]
[Route("[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IBookAppointmentUseCase bookAppointmentUseCase;
    private readonly IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase;

    public AppointmentsController(IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase,
        IBookAppointmentUseCase bookAppointmentUseCase)
    {
        this.retrieveAvailableAppointmentsUseCase = retrieveAvailableAppointmentsUseCase;
        this.bookAppointmentUseCase = bookAppointmentUseCase;
    }

    [HttpGet]
    [Route("AvailableAppointments")]
    public async Task<IActionResult> AvailableAppointments([FromQuery] string sorCode, [FromQuery] string locationId,
        [FromQuery] DateTime? fromDate = null)
    {
        try
        {
            var result = await this.retrieveAvailableAppointmentsUseCase.Execute(SorCode.Parse(sorCode),
                AddressUprn.Parse(locationId), fromDate);
            return this.Ok(result);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
            return this.StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("BookAppointment")]
    public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest bookAppointmentRequest)
    {
        try
        {
            var result = await this.bookAppointmentUseCase.Execute(bookAppointmentRequest.Reference,
                bookAppointmentRequest.GetSorCode(), bookAppointmentRequest.GetAddressUprn(),
                bookAppointmentRequest.GetAppointmentSlot(),
                bookAppointmentRequest.GetContact(), bookAppointmentRequest.JobDescription);

            return this.Ok(result);
        }
        catch (Exception ex)
        {
            SentrySdk.CaptureException(ex);
            return this.StatusCode(500, ex.Message);
        }
    }
}
