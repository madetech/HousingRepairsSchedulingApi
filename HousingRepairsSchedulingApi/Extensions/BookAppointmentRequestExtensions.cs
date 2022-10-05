namespace HousingRepairsSchedulingApi.Extensions;

using Domain;
using Dtos.Hro;

public static class BookAppointmentRequestExtensions
{
    public static Contact GetContact(this BookAppointmentRequest request) => new(
        request.ContactDetails.PhoneNumber,
        request.ContactDetails.MobileNumber, request.ContactDetails.Email);

    public static AppointmentSlot GetAppointmentSlot(this BookAppointmentRequest request) =>
        new(request.Appointment.StartTime, request.Appointment.EndTime);

    public static SorCode GetSorCode(this BookAppointmentRequest request) => SorCode.Parse(request.SorCode);

    public static AddressUprn GetAddressUprn(this BookAppointmentRequest request) =>
        AddressUprn.Parse(request.LocationId);
}
