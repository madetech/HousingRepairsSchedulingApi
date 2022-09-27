using System;
using System.Threading.Tasks;
using FluentAssertions;
using Flurl.Http;
using Xunit;
using Flurl.Http.Testing;
using HousingRepairsSchedulingApi.Factories;
using HousingRepairsSchedulingApi.Gateways;

namespace HousingRepairsSchedulingApi.Tests.GatewaysTests;

public class McmAppointmentGatewayTests : IDisposable
{
    private HttpTest httpTest;
    private McmAppointmentGateway mcmAppointmentGateway;

    public McmAppointmentGatewayTests()
    {
        httpTest = new HttpTest();
        mcmAppointmentGateway =
            new McmAppointmentGateway(
                baseUrl: "baseUrl",
                new AppointmentsFactory(),
                new JobCodesFactory(),
                "username",
                "mcmPassword");
    }

    public void Dispose()
    {
        httpTest.Dispose();
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenMcmDoesNotReturn200()
    {
        var statusCode = 400;
        var badRequestMessage = "Bad request";
        httpTest.RespondWith(badRequestMessage, statusCode);

        Func<Task> act = async () =>
            await mcmAppointmentGateway.GetAvailableAppointments("sorCode", "locationid", DateTime.Now);

        await act.Should().ThrowExactlyAsync<FlurlHttpException>();
    }
}