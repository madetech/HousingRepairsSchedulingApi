namespace HousingRepairsSchedulingApi.Extensions;

using System;
using Microsoft.Extensions.Logging;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, string, Exception> AddJobMessageDefinition =
        LoggerMessage
            .Define<string>(
                LogLevel.Information,
                new EventId(1),
                "Creating job in MCM with booking reference {Ref}");

    public static void AddJob(this ILogger logger, string bookingReference) =>
        AddJobMessageDefinition(logger, bookingReference, null);
}
