namespace HousingRepairsSchedulingApi.Tests.MappersTests;

using System;
using System.Linq;
using Dtos;
using FluentAssertions;
using Factories;
using Newtonsoft.Json;
using Xunit;

public class AppointmentsMapperTests
{
    [Theory]
    [JsonFileData("getAppointmentSlots.json", "twoDaysAround", typeof(GetSlotsResponse))]
    public void ShouldFilterOutAppointmentsThatAreNotBookable(GetSlotsResponse getSlotsResponse)
    {
        var appointments = AppointmentsFactory.FromGetSlotsResponse(getSlotsResponse);

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

        var appointment = AppointmentsFactory.FromGetSlotsResponse(getSlotsResponse).First();

        appointment.StartTime.Should().Be(DateTime.Parse("2022-09-26T08:00:00"));
        appointment.EndTime.Should().Be(DateTime.Parse("2022-09-26T17:00:00"));
    }
}
