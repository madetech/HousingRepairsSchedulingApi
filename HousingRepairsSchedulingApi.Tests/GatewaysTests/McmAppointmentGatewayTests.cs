namespace HousingRepairsSchedulingApi.Tests.GatewaysTests;

using System;
using System.Threading.Tasks;
using Configuration;
using Domain;
using Dtos;
using FluentAssertions;
using Flurl.Http;
using Flurl.Http.Testing;
using Gateways;
using Gateways.Exceptions;
using Helpers;
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
            .Returns(new JobCodes(SorCode.Parse("sorcode"), "jobcode"));

        this.mcmAppointmentGateway =
            new McmAppointmentGateway(
                new McmConfiguration("http://foo.com", "username", "password"),
                jobCodesMapperMock.Object
            );
    }

    public void Dispose() => this.httpTest.Dispose();

    [Fact]
    public async Task ShouldThrowExceptionWhenMcmDoesNotReturn200()
    {
        var statusCode = 400;
        var badRequestMessage = "Bad request";
        this.httpTest.RespondWith(badRequestMessage, statusCode);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments(SorCode.Parse("sorCode"),
                AddressUprn.Parse("uprn"), DateTime.Now);

        await act.Should().ThrowExactlyAsync<FlurlHttpException>();
    }

    [Theory]
    [JsonFileData("fixtures/getAppointmentSlots.json", "error", typeof(GetSlotsResponse))]
    public async Task ShouldThrowExceptionWhenMcmReturnsAnErrorInTheResponseBody(GetSlotsResponse response)
    {
        this.httpTest.RespondWithJson(response);

        Func<Task> act = async () =>
            await this.mcmAppointmentGateway.GetAvailableAppointments(SorCode.Parse("sorCode"),
                AddressUprn.Parse("locationid"), DateTime.Now);

        await act.Should().ThrowExactlyAsync<McmRequestError>();
    }
}
