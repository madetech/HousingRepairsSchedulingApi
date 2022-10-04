namespace HousingRepairsSchedulingApi.Tests.HelpersTests;

using System.Text.Json;
using Domain;
using FluentAssertions;
using Helpers;
using Helpers.Exceptions;
using Xunit;

public class JobCodesMapperTests
{
    [Theory]
    [FileData("fixtures/jobCodes.json")]
    public void ShouldMapFromSorCodeToJobCodes(string jobCodesRaw)
    {
        var jobCodesJson = JsonDocument.Parse(jobCodesRaw);
        var jobCodesMapper = new JobCodesMapper(jobCodesJson);

        var sorCode = SorCode.Parse("123456");
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

        var unknownSorCode = SorCode.Parse("unknown");

        var act = () =>
            jobCodesMapper.FromSorCode(unknownSorCode);

        act.Should().ThrowExactly<SorNotFound>();
    }
}
