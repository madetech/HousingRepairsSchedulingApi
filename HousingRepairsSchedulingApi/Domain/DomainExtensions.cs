namespace HousingRepairsSchedulingApi.Domain;

using HACT.Dtos;

public static class DomainExtensions
{
    public static Appointment ToHactAppointment(this AppointmentSlot appointmentSlot) =>
        new()
        {
            Reference = new Reference { ID = appointmentSlot.Id },
            Date = appointmentSlot.StartTime.Date,
            TimeOfDay = new TimeOfDay
            {
                EarliestArrivalTime = appointmentSlot.StartTime, LatestArrivalTime = appointmentSlot.EndTime
            }
        };
}
