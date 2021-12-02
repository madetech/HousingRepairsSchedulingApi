using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;

namespace HousingRepairsSchedulingApi.Tests.ControllersTests
{
    using Controllers;
    using UseCases;

    public class AppointmentsControllerTests : ControllerTests
    {
        private AppointmentsController sytemUndertest;
        private Mock<IRetrieveAvailableAppointmentsUseCase> availableAppointmentsUseCaseMock;

        public AppointmentsControllerTests()
        {
            availableAppointmentsUseCaseMock = new Mock<IRetrieveAvailableAppointmentsUseCase>();
            sytemUndertest = new AppointmentsController(availableAppointmentsUseCaseMock.Object);
        }

        [Fact]
        public async Task TestEndpoint()
        {
            const string sorCode = "uprn";
            const string locationId = "locationId";

            var result = await sytemUndertest.AvailableAppointments(sorCode,locationId);
            GetStatusCode(result).Should().Be(200);
            availableAppointmentsUseCaseMock.Verify(x => x.Execute(sorCode,locationId), Times.Once);
        }
    }
}
