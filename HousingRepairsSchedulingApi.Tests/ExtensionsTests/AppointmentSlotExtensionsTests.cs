namespace HousingRepairsSchedulingApi.Tests.ExtensionsTests;

using System;
using System.Collections.Generic;
using Domain;
using Extensions;
using FluentAssertions;
using Xunit;

public class AppointmentSlotExtensionsTests
{
    [Theory]
    [MemberData(nameof(AppointmentSlots))]
    public void ShouldConvertAppointmentSlotsIntoDescriptionsForMcm(AppointmentSlot appointmentSlot,
        string expectedDescription)
    {
        var slotDescription = appointmentSlot.McmSlotDescription();

        slotDescription.Should().Be(expectedDescription);
    }


    public static IEnumerable<object[]> AppointmentSlots()
    {
        yield return new object[]
        {
            new AppointmentSlot(DateTime.Parse("2022-10-06T08:00:00"), DateTime.Parse("2022-10-06T10:00:00")),
            "08:00-10:00"
        };
        yield return new object[]
        {
            new AppointmentSlot(DateTime.Parse("2022-10-06T10:05:00"), DateTime.Parse("2022-10-06T12:30:00")),
            "10:05-12:30"
        };
    }
}
