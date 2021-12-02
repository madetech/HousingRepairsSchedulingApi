namespace HousingRepairsSchedulingApi.UseCases
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using Gateways;
    using HACT.Dtos;

    public class RetrieveAvailableAppointmentsUseCase : IRetrieveAvailableAppointmentsUseCase
    {
        private readonly IAppointmentsGateway appointmentsGateway;

        public RetrieveAvailableAppointmentsUseCase(IAppointmentsGateway appointmentsGateway)
        {
            this.appointmentsGateway = appointmentsGateway;
        }

        public async Task<IEnumerable<Appointment>> Execute(string sorCode, string locationId)
        {
            Guard.Against.NullOrWhiteSpace(sorCode, nameof(sorCode));
            Guard.Against.NullOrWhiteSpace(locationId, nameof(locationId));
            var result = await appointmentsGateway.GetAvailableAppointments(sorCode, locationId);
            return result;
        }
    }
}
