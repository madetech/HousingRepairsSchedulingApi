namespace HousingRepairsSchedulingApi.UseCases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Gateways;
using HACT.Dtos;

public class RetrieveAvailableAppointmentsUseCase : IRetrieveAvailableAppointmentsUseCase
{
    private readonly IAppointmentsGateway appointmentsGateway;

    public RetrieveAvailableAppointmentsUseCase(IAppointmentsGateway appointmentsGateway) =>
        this.appointmentsGateway = appointmentsGateway;

    public async Task<IEnumerable<Appointment>> Execute(SorCode sorCode, AddressUprn addressUprn,
        DateTime? fromDate = null)
    {
        var availableAppointments =
            await this.appointmentsGateway.GetAvailableAppointments(sorCode, addressUprn, fromDate);

        var result = availableAppointments.Select(x => x.ToHactAppointment());

        return result;
    }
}
