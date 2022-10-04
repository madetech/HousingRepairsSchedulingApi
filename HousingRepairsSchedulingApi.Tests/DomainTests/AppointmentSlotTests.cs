namespace HousingRepairsSchedulingApi.Tests.DomainTests;

using System;
using Domain;
using FluentAssertions;
using Xunit;

public class AppointmentSlotTests
{
    [Fact]
    public void WhenEndTimeIsBeforeStartTimeItThrowsArgumentOutOfRangeException()
    {
        var startTime = DateTime.Now;
        var endTime = startTime.Subtract(TimeSpan.FromHours(1));

        var act = () => new AppointmentSlot(startTime, endTime);

        act.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
}
