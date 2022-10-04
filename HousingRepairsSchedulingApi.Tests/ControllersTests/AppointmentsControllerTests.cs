namespace HousingRepairsSchedulingApi.Tests.ControllersTests;

using System;
using System.Threading.Tasks;
using Controllers;
using Domain;
using Dtos.Hro;
using FluentAssertions;
using Moq;
using UseCases;
using Xunit;

public class AppointmentsControllerTests : ControllerTests
{
    private const string SampleLocationId = "locationId";
    private const string SampleSorCode = "SOR Code";

    private readonly Mock<IRetrieveAvailableAppointmentsUseCase> availableAppointmentsUseCaseMock;
    private readonly Mock<IBookAppointmentUseCase> bookAppointmentUseCaseMock;
    private readonly AppointmentsController systemUndertest;

    public AppointmentsControllerTests()
    {
        this.availableAppointmentsUseCaseMock = new Mock<IRetrieveAvailableAppointmentsUseCase>();
        this.bookAppointmentUseCaseMock = new Mock<IBookAppointmentUseCase>();
        this.systemUndertest = new AppointmentsController(this.availableAppointmentsUseCaseMock.Object,
            this.bookAppointmentUseCaseMock.Object);
    }

    // Should replace with Faker
    private static BookAppointmentRequest sampleBookAppointmentRequest() => new()
    {
        Reference = "reference",
        LocationId = SampleLocationId,
        SorCode = SampleSorCode,
        Appointment = new Appointment { StartTime = DateTime.Now, EndTime = DateTime.Now.AddHours(1) },
        ContactDetails = new ContactDetails
        {
            Email = "test@test.com", PhoneNumber = "01237890765", Name = "Test Testsson"
        },
        JobDescription = "There's a snake in my boot"
    };

    [Fact]
    public async Task TestAvailableAppointmentsEndpoint()
    {
        var result = await this.systemUndertest.AvailableAppointments(SampleSorCode, SampleLocationId);
        GetStatusCode(result).Should().Be(200);
        this.availableAppointmentsUseCaseMock.Verify(
            x => x.Execute(SorCode.Parse(SampleSorCode), AddressUprn.Parse(SampleLocationId), null),
            Times.Once);
    }


    [Fact]
    public async Task ReturnsErrorWhenFailsToGetAvailableAppointments()
    {
        const string errorMessage = "An error message";
        this.availableAppointmentsUseCaseMock.Setup(x => x.Execute(It.IsAny<SorCode>(), It.IsAny<AddressUprn>(), null))
            .Throws(new Exception(errorMessage));

        var result = await this.systemUndertest.AvailableAppointments(SampleSorCode, SampleLocationId);

        GetStatusCode(result).Should().Be(500);
        GetResultData<string>(result).Should().Be(errorMessage);
    }

    [Fact]
    public async Task TestBookAppointmentEndpoint()
    {
        var result =
            await this.systemUndertest.BookAppointment(sampleBookAppointmentRequest());
        GetStatusCode(result).Should().Be(200);
    }

    [Fact]
#pragma warning disable CA1707
    public async Task GivenAFromDate_WhenRequestingAvailableAppointment_ThenResultsAreReturned()
#pragma warning restore CA1707
    {
        // Arrange
        const string sorCode = "sorCode";
        const string locationId = "locationId";
        var fromDate = new DateTime(2021, 12, 15);

        // Act
        var result = await this.systemUndertest.AvailableAppointments(sorCode, locationId, fromDate);

        // Assert
        GetStatusCode(result).Should().Be(200);
        this.availableAppointmentsUseCaseMock.Verify(
            x => x.Execute(SorCode.Parse(sorCode), AddressUprn.Parse(locationId), fromDate), Times.Once);
    }

    [Fact]
    public async Task ReturnsErrorWhenFailsToBookAppointments()
    {
        const string errorMessage = "An error message";
        this.bookAppointmentUseCaseMock
            .Setup(x => x.Execute(It.IsAny<string>(), It.IsAny<SorCode>(), It.IsAny<AddressUprn>(),
                It.IsAny<AppointmentSlot>(),
                It.IsAny<Contact>(), It.IsAny<string>())).Throws(new Exception(errorMessage));

        var result =
            await this.systemUndertest.BookAppointment(sampleBookAppointmentRequest());

        GetStatusCode(result).Should().Be(500);
        GetResultData<string>(result).Should().Be(errorMessage);
    }
}
