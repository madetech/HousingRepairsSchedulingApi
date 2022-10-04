namespace HousingRepairsSchedulingApi.UseCases;

using System.Threading.Tasks;
using Domain;
using JetBrains.Annotations;

public interface IBookAppointmentUseCase
{
    public Task<string> Execute([NotNull] string bookingReference, [NotNull] SorCode sorCode,
        [NotNull] AddressUprn addressUprn,
        [NotNull] AppointmentSlot appointmentSlot, [NotNull] Contact contact, string jobDescription);
}
