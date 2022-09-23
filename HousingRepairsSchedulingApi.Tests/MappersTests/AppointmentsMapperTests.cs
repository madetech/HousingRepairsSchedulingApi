namespace HousingRepairsSchedulingApi.Tests.MappersTests;

using System.IO;
using System.IO.Enumeration;
using System.Linq;
using Dtos;
using FluentAssertions;
using Mappers;
using Newtonsoft.Json;
using Xunit;

public class AppointmentsMapperTests
{
    [Fact]
    public void ShouldFilterOutAppointmentsThatAreNotBookable()
    {
        var getSlotsResponse = GetResponseFixture();

        var appointments = AppointmentsMapper.MapGetSlotsResponse(getSlotsResponse);

        appointments.Should().HaveCount(7);
    }

    private static GetSlotsResponse GetResponseFixture()
    {
        const string responseJson = @"
{
    'StatusCode': '1',
    'StatusMessage': 'SUCCESS',
    'ContractReference': null,
    'SlotDays': [
        {
            'SlotDate': '2022-09-22T00:00:00',
            'ResourceCapacity': 0,
            'NonBookingDay': false,
            'Slots': []
        },
        {
            'SlotDate': '2022-09-23T00:00:00',
            'ResourceCapacity': 0,
            'NonBookingDay': false,
            'Slots': []
        },
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
                {
                    'Description': 'SR',
                    'StartTime': '10:00:00',
                    'EndTime': '14:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 11,
                    'MaximumSlotCapacity': 15
                },
                {
                    'Description': 'Sat AM',
                    'StartTime': '08:00:00',
                    'EndTime': '12:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 11,
                    'MaximumSlotCapacity': 15
                },
                {
                    'Description': '08:00-10:00',
                    'StartTime': '08:00:00',
                    'EndTime': '10:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 5,
                    'MaximumSlotCapacity': 7
                },
                {
                    'Description': '10:00-12:00',
                    'StartTime': '10:00:00',
                    'EndTime': '12:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 5,
                    'MaximumSlotCapacity': 7
                },
                {
                    'Description': '13:00-15:00',
                    'StartTime': '13:00:00',
                    'EndTime': '15:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 5,
                    'MaximumSlotCapacity': 7
                },
                {
                    'Description': '15:00-17:00',
                    'StartTime': '15:00:00',
                    'EndTime': '17:00:00',
                    'Bookable': true,
                    'AvailableSlotCapacity': 5,
                    'MaximumSlotCapacity': 7
                }
            ]
        }
    ],
    'GasComplianceDetails': null,
    'CalendarCentralisationDetails': {
        'CentralisedInJobPriorityDays': true,
        'CentralDate': '2022-09-24T14:22:10.512Z'
    },
    'TradeAreaDetails': {
        'Active': false,
        'AppointmentArea': null
    }
}
";

        return JsonConvert.DeserializeObject<GetSlotsResponse>(responseJson);
    }

}
