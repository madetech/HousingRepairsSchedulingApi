namespace HousingRepairsSchedulingApi.UseCases;

using System.Threading.Tasks;
using Domain;
using Gateways;

public class BookAppointmentUseCase : IBookAppointmentUseCase
{
    private readonly IAppointmentsGateway appointmentsGateway;

    public BookAppointmentUseCase(IAppointmentsGateway appointmentsGateway) =>
        this.appointmentsGateway = appointmentsGateway;

    public Task<string> Execute(string bookingReference, SorCode sorCode, AddressUprn addressUprn,
        AppointmentSlot appointmentSlot, Contact contact, string jobDescription)
    {
        var result = this.appointmentsGateway.BookAppointment(bookingReference, sorCode, addressUprn,
            appointmentSlot, contact, jobDescription);

        return result;
    }
}
