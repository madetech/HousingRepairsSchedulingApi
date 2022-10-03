namespace HousingRepairsSchedulingApi.Helpers;

using System;
using System.IO;
using System.Text.Json;
using Ardalis.GuardClauses;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddJobCodesMapper(this IServiceCollection services, string jobCodesPath)
    {
        Guard.Against.NullOrWhiteSpace(jobCodesPath, nameof(jobCodesPath));

        var path = Path.IsPathRooted(jobCodesPath)
            ? jobCodesPath
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), jobCodesPath);

        if (!File.Exists(path))
        {
            throw new ArgumentException($"Could not find job codes configuration at path: {path}");
        }

        var fileData = File.ReadAllText(jobCodesPath);
        var jobCodes = JsonDocument.Parse(fileData);

        services.AddTransient<IJobCodesMapper, JobCodesMapper>(_ => new JobCodesMapper(jobCodes));
    }
}
