using HousingRepairsSchedulingApi.Dtos.Hro;

namespace HousingRepairsSchedulingApi.UseCases;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain;

public interface IRetrieveAvailableAppointmentsUseCase
{
    public Task<IEnumerable<AppointmentDto>> Execute([NotNull] SorCode sorCode, [NotNull] AddressUprn addressUprn,
        DateTime? fromDate);
}
