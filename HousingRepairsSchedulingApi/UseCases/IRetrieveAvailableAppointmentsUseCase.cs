namespace HousingRepairsSchedulingApi.UseCases;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using HACT.Dtos;
using JetBrains.Annotations;

public interface IRetrieveAvailableAppointmentsUseCase
{
    public Task<IEnumerable<Appointment>> Execute([NotNull] SorCode sorCode, [NotNull] AddressUprn addressUprn,
        DateTime? fromDate);
}
