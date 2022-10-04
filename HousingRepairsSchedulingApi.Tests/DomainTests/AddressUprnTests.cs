namespace HousingRepairsSchedulingApi.Tests.DomainTests;

using System;
using System.Collections.Generic;
using Domain;
using FluentAssertions;
using Xunit;

public class AddressUprnTests
{
    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
    public void ShouldThrowExceptionWhenParsingInvalidData(string invalidAddressUprn)
    {
        var act = () => AddressUprn.Parse(invalidAddressUprn);

        act.Should().Throw<Exception>();
    }

    public static IEnumerable<object[]> InvalidArgumentTestData()
    {
        yield return new object[] { null };
        yield return new object[] { "" };
        yield return new object[] { " " };
    }
}
