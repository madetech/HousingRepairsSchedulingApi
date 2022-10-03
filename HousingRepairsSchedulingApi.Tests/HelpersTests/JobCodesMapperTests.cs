using System.Text.Json;
using FluentAssertions;
using HousingRepairsSchedulingApi.Helpers;
using HousingRepairsSchedulingApi.Helpers.Exceptions;
using HousingRepairsSchedulingApi.Tests;
using Xunit;

public class JobCodesMapperTests
{
    [Theory]
    [FileData("fixtures/jobCodes.json")]
    public void ShouldMapFromSorCodeToJobCodes(string jobCodesRaw)
    {
        var jobCodesJson = JsonDocument.Parse(jobCodesRaw);
        var jobCodesMapper = new JobCodesMapper(jobCodesJson);

        var sorCode = "123456";
        var tradeCode = "EL";

        var jobCodes = jobCodesMapper.FromSorCode(sorCode);

        jobCodes.SorCode.Should().Be(sorCode);
        jobCodes.TradeCode.Should().Be(tradeCode);
    }

    [Theory]
    [FileData("fixtures/jobCodes.json")]
    public void ShouldThrowExceptionIfSorCodeNotFound(string jobCodesRaw)
    {
        var jobCodesJson = JsonDocument.Parse(jobCodesRaw);
        var jobCodesMapper = new JobCodesMapper(jobCodesJson);

        var unknownSorCode = "unknown";

        var act = () =>
            jobCodesMapper.FromSorCode(unknownSorCode);

        act.Should().ThrowExactly<SorNotFound>();
    }
}
