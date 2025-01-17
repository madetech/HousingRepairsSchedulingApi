namespace HousingRepairsSchedulingApi.Tests.ExtensionsTests;

using System;
using System.Linq;
using Dtos.Mcm;
using Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

public class GetSlotsResponseExtensionsTests
{
    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "twoDaysAround", typeof(GetSlotsResponse))]
    public void ShouldFilterOutAppointmentsThatAreNotBookable(GetSlotsResponse getSlotsResponse)
    {
        var appointments = getSlotsResponse.ToAppointmentSlots(5, DateTime.MinValue);

        appointments.Should().HaveCount(4);
    }

    [Fact]
    public void ShouldMapSlotDateAndTimeToStartTimeAndEndTime()
    {
        var responseJson = @"{
    'StatusCode': '1',
    'StatusMessage': 'SUCCESS',
    'ContractReference': null,
    'SlotDays': [
        {
            'SlotDate': '2022-09-26T00:00:00',
            'ResourceCapacity': 5,
            'NonBookingDay': false,
            'Slots': [
                {
                    'Description': '08:00-10:00',
                    'StartTime': '08:00:00',
                    'EndTime': '10:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 24,
                    'MaximumSlotCapacity': 33
                },
            ]
        }
    ]}";
        var getSlotsResponse = JsonConvert.DeserializeObject<GetSlotsResponse>(responseJson);

        var appointment = getSlotsResponse.ToAppointmentSlots(5, DateTime.MinValue).First();

        appointment.StartTime.Should().Be(DateTime.Parse("2022-09-26T08:00:00"));
        appointment.EndTime.Should().Be(DateTime.Parse("2022-09-26T10:00:00"));
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "slotWithNoAvailability", typeof(GetSlotsResponse))]
    public void ShouldFilterOutSlotsWithNoAvailability(GetSlotsResponse getSlotsResponse)
    {
        var appointments = getSlotsResponse.ToAppointmentSlots(5, DateTime.MinValue);

        appointments.Should().HaveCount(1);
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "moreThanFiveDaysOfAppointments", typeof(GetSlotsResponse))]
    public void ShouldLimitNumberOfAppointmentDays(GetSlotsResponse getSlotsResponse)
    {
        var desiredNumberOfDays = 5;

        var appointments =
            getSlotsResponse.ToAppointmentSlots(desiredNumberOfDays, DateTime.MinValue);

        appointments.Select(appointment => appointment.StartTime.Day).Distinct().Should()
            .HaveCountLessOrEqualTo(desiredNumberOfDays);
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "moreThanFiveDaysOfAppointments", typeof(GetSlotsResponse))]
    public void ShouldFilterOutDaysBeforeFromDate(GetSlotsResponse getSlotsResponse)
    {
        var fromDate = DateTime.Parse("2022-09-30T00:00:00");

        var appointments = getSlotsResponse.ToAppointmentSlots(5, fromDate);

        appointments.All(day => day.StartTime >= fromDate).Should().BeTrue();
    }
}
