namespace HousingRepairsSchedulingApi.Tests.GatewaysTests;

using System;
using System.Threading.Tasks;
using Dtos;
using Factories;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Gateways;
using Gateways.Exceptions;
using Xunit;

public class McmAppointmentGatewayTests : IDisposable
{
    private readonly HttpTest httpTest;
    private readonly McmAppointmentGateway mcmAppointmentGateway;

    public McmAppointmentGatewayTests()
    {
        this.httpTest = new HttpTest();
        this.mcmAppointmentGateway =
            new McmAppointmentGateway(
                "http://foo.com",
                new AppointmentsFactory(),
                new JobCodesFactory(),
                "username",
                "mcmPassword");
    }

    public void Dispose() => this.httpTest.Dispose();

    [Fact]
    public async Task ShouldThrowExceptionWhenMcmDoesNotReturn200()
    {
        var statusCode = 400;
        var badRequestMessage = "Bad request";
        this.httpTest.RespondWith(badRequestMessage, statusCode);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments("sorCode", "locationid", DateTime.Now);

        await act.Should().ThrowExactlyAsync<FlurlHttpException>();
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "error", typeof(GetSlotsResponse))]
    public async Task ShouldThrowExceptionWhenMcmReturnsAnErrorInTheResponseBody(GetSlotsResponse response)
    {
        this.httpTest.RespondWithJson(response);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments("sorCode", "locationid", DateTime.Now);

        await act.Should().ThrowExactlyAsync<McmRequestError>();
    }
}
