using HousingRepairsSchedulingApi.Dtos.Hro;

namespace HousingRepairsSchedulingApi.Domain;

public static class DomainExtensions
{
    public static AppointmentDto ToAppointmentDto(this AppointmentSlot appointmentSlot) =>
        new()
        {
            Id = appointmentSlot.Id,
            Date = appointmentSlot.StartTime.Date,
            StartTime = appointmentSlot.StartTime,
            EndTime = appointmentSlot.EndTime
        };
}