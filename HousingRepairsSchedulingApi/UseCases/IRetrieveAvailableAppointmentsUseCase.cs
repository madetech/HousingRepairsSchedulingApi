namespace HousingRepairsSchedulingApi.UseCases;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain;
using HACT.Dtos;

public interface IRetrieveAvailableAppointmentsUseCase
{
    public Task<IEnumerable<Appointment>> Execute([NotNull] SorCode sorCode, [NotNull] AddressUprn addressUprn,
        DateTime? fromDate);
}
