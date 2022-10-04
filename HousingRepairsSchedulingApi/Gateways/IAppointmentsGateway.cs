namespace HousingRepairsSchedulingApi.Gateways;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using JetBrains.Annotations;

public interface IAppointmentsGateway
{
    Task<IEnumerable<AppointmentSlot>> GetAvailableAppointments([NotNull] SorCode sorCode,
        [NotNull] AddressUprn addressUprn,
        DateTime? fromDate = null);

    Task<string> BookAppointment([NotNull] string bookingReference, [NotNull] SorCode sorCode,
        [NotNull] AddressUprn addressUprn,
        [NotNull] AppointmentSlot appointmentSlot, [NotNull] Contact contact, string jobDescription);
}
