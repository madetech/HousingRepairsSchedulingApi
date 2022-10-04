namespace HousingRepairsSchedulingApi.Tests.DomainTests;

using System;
using System.Collections.Generic;
using Domain;
using FluentAssertions;
using Xunit;

public class SorCodeTests
{
    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
    public void ShouldThrowExceptionWhenParsingInvalidData(string invalidSorCode)
    {
        var act = () => SorCode.Parse(invalidSorCode);

        act.Should().Throw<Exception>();
    }

    public static IEnumerable<object[]> InvalidArgumentTestData()
    {
        yield return new object[] { null };
        yield return new object[] { "" };
        yield return new object[] { " " };
    }
}
