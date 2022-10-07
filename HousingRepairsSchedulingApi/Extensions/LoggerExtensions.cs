namespace HousingRepairsSchedulingApi.Extensions;

using System;
using Microsoft.Extensions.Logging;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, string, Exception> BeforeAddJobMessageDefinition =
        LoggerMessage
            .Define<string>(
                LogLevel.Information,
                new EventId(1),
                "Creating job in MCM with booking reference {Ref}");

    private static readonly Action<ILogger, int, string, Exception> AfterAddJobMessageDefinition =
        LoggerMessage
            .Define<int, string>(
                LogLevel.Information,
                new EventId(2),
                "Created job in MCM with id {Id} for booking reference {Ref}");

    public static void BeforeAddJob(this ILogger logger, string bookingReference) =>
        BeforeAddJobMessageDefinition(logger, bookingReference, null);

    public static void AfterAddJob(this ILogger logger, int jobId, string bookingReference) =>
        AfterAddJobMessageDefinition(logger, jobId, bookingReference, null);

    private static readonly Action<ILogger, int, Exception> BeforeBookAppointmentMessageDefinition =
        LoggerMessage
            .Define<int>(
                LogLevel.Information,
                new EventId(3),
                "Booking appointment in MCM for job with id {Id}");
    public static void BeforeBookAppointment(this ILogger logger, int jobId) =>
        BeforeBookAppointmentMessageDefinition(logger, jobId, null);

    private static readonly Action<ILogger, int, Exception> AfterBookAppointmentMessageDefinition =
        LoggerMessage
            .Define<int>(
                LogLevel.Information,
                new EventId(4),
                "Successfully booked appointment in MCM for job with id {Id}");
    public static void AfterBookAppointment(this ILogger logger, int jobId) =>
        AfterBookAppointmentMessageDefinition(logger, jobId, null);
}
