namespace HousingRepairsSchedulingApi.Tests.GatewaysTests;

using System;
using System.Threading.Tasks;
using Configuration;
using Domain;
using Dtos.Mcm;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Gateways;
using Gateways.Exceptions;
using Helpers;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

public class McmAppointmentGatewayTests : IDisposable
{
    private readonly HttpTest httpTest;
    private readonly McmAppointmentGateway mcmAppointmentGateway;

    public McmAppointmentGatewayTests()
    {
        this.httpTest = new HttpTest();
        var jobCodesMapperMock = new Mock<IJobCodesMapper>();

        jobCodesMapperMock.Setup(expression => expression.FromSorCode(It.IsAny<SorCode>()))
            .Returns(new JobCodes(SorCode(), "EL"));

        this.mcmAppointmentGateway =
            new McmAppointmentGateway(
                new NullLogger<McmAppointmentGateway>(),
                new McmConfiguration("http://foo.com", "username", "password"),
                jobCodesMapperMock.Object,
                new McmRequestFactory()
            );
    }

    public void Dispose() => this.httpTest.Dispose();

    [Fact]
    public async Task WhenGettingAppointmentsShouldThrowExceptionWhenMcmDoesNotReturn200()
    {
        var statusCode = 400;
        var badRequestMessage = "Bad request";
        this.httpTest.RespondWith(badRequestMessage, statusCode);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments(Domain.SorCode.Parse("sorCode"),
                Domain.AddressUprn.Parse("uprn"), DateTime.Now);

        await act.Should().ThrowExactlyAsync<FlurlHttpException>();
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "error", typeof(GetSlotsResponse))]
    public async Task WhenGettingAppointmentShouldThrowExceptionWhenMcmReturnsAnErrorInTheResponseBody(
        GetSlotsResponse response)
    {
        this.httpTest.RespondWithJson(response);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments(SorCode(),
                AddressUprn(), DateTime.Now);

        await act.Should().ThrowExactlyAsync<McmRequestError>();
    }

    [Fact]
    public async Task WhenAddingJobShouldThrowExceptionWhenMcmDoesNotReturn200()
    {
        var statusCode = 500;
        var internalServerErrorMessage = "Internal Server Error";

        this.httpTest.RespondWith(internalServerErrorMessage, statusCode);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.BookAppointment(Reference(), SorCode(), AddressUprn(),
                AppointmentSlot(), Contact(), Reference());

        await act.Should().ThrowExactlyAsync<FlurlHttpException>();
    }

    private static SorCode SorCode() => Domain.SorCode.Parse("sorcode");

    private static AddressUprn AddressUprn() => Domain.AddressUprn.Parse("addressuprn");

    private static AppointmentSlot AppointmentSlot() => new(DateTime.Now, DateTime.Now.AddHours(1));

    private static string Reference() => "a reference";

    // Should use a faker library
    private static Contact Contact() => new("01237 990881", "07888 888888", "test@test.com");
}
