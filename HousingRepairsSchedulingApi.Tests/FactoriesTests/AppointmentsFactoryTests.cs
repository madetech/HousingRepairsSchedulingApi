namespace HousingRepairsSchedulingApi.Tests.FactoriesTests;

using System;
using System.Linq;
using Dtos;
using Factories;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

public class AppointmentsFactoryTests
{
    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "twoDaysAround", typeof(GetSlotsResponse))]
    public void ShouldFilterOutAppointmentsThatAreNotBookable(GetSlotsResponse getSlotsResponse)
    {
        var appointmentsFactory = new AppointmentsFactory();
        var appointments = appointmentsFactory.FromGetSlotsResponse(getSlotsResponse, 5);

        appointments.Should().HaveCount(7);
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
                    'Description': 'ALL',
                    'StartTime': '08:00:00',
                    'EndTime': '17:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 24,
                    'MaximumSlotCapacity': 33
                },
            ]
        }
    ]}";
        var getSlotsResponse = JsonConvert.DeserializeObject<GetSlotsResponse>(responseJson);

        var appointmentsFactory = new AppointmentsFactory();
        var appointment = appointmentsFactory.FromGetSlotsResponse(getSlotsResponse, 5).First();

        appointment.StartTime.Should().Be(DateTime.Parse("2022-09-26T08:00:00"));
        appointment.EndTime.Should().Be(DateTime.Parse("2022-09-26T17:00:00"));
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "slotWithNoAvailability", typeof(GetSlotsResponse))]
    public void ShouldFilterOutSlotsWithNoAvailability(GetSlotsResponse getSlotsResponse)
    {
        var appointmentsFactory = new AppointmentsFactory();
        var appointments = appointmentsFactory.FromGetSlotsResponse(getSlotsResponse, 5);

        appointments.Should().HaveCount(1);
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "moreThanFiveDaysOfAppointments", typeof(GetSlotsResponse))]
    public void ShouldLimitNumberOfAppointmentDays(GetSlotsResponse getSlotsResponse)
    {
        var desiredNumberOfDays = 5;

        var appointmentsFactory = new AppointmentsFactory();
        var appointments = appointmentsFactory.FromGetSlotsResponse(getSlotsResponse, desiredNumberOfDays);

        appointments.Select(appointment => appointment.StartTime.Day).Distinct().Should().HaveCountLessOrEqualTo(5);
    }
}
