namespace HousingRepairsSchedulingApi.Extensions;

using System.Globalization;
using Domain;

public static class AppointmentSlotExtensions
{
    /// <summary>
    ///     This only takes into account slots with descriptions such as "10:00-12:00" other descriptions such as
    ///     "Sat AM", "All" or "SR" won't be handled and will likely result in MCM throwing an error. The assumption
    ///     is that this won't matter as Redbridge should only have 2 hour appointments with the former description
    ///     available.
    /// </summary>
    public static string McmSlotDescription(this AppointmentSlot slot) =>
        string.Format(
            new NumberFormatInfo(),
            "{0:D2}:{1:D2}-{2:D2}:{3:D2}",
            slot.StartTime.Hour,
            slot.StartTime.Minute,
            slot.EndTime.Hour,
            slot.EndTime.Minute
        );
}
