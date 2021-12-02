namespace HousingRepairsSchedulingApi.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using UseCases;

    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase;

        public AppointmentsController(IRetrieveAvailableAppointmentsUseCase retrieveAvailableAppointmentsUseCase)
        {
            this.retrieveAvailableAppointmentsUseCase = retrieveAvailableAppointmentsUseCase;
        }
        [HttpGet]
        public async Task<IActionResult> AvailableAppointments([FromQuery] string sorCode, [FromQuery] string locationId)
        {
            var result = await this.retrieveAvailableAppointmentsUseCase.Execute(sorCode, locationId);
            return this.Ok(result);
        }
    }
}
