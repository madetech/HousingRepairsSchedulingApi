namespace HousingRepairsSchedulingApi.UseCases;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Domain;

public interface IBookAppointmentUseCase
{
    public Task<string> Execute([NotNull] string bookingReference, [NotNull] SorCode sorCode,
        [NotNull] AddressUprn addressUprn,
        [NotNull] AppointmentSlot appointmentSlot, [NotNull] Contact contact, string jobDescription);
}
