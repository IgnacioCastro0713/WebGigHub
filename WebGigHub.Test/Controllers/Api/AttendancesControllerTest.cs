using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebGigHub.Controllers.Api;
using WebGigHub.Core;
using WebGigHub.Core.Dtos;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;
using WebGigHub.Test.Extensions;

namespace WebGigHub.Test.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTest
    {
        private AttendancesController _attendancesController;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.AttendanceRepository).Returns(_mockRepository.Object);

            _attendancesController = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _attendancesController.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public async Task Attend_UserAttendingAGigForWhichHeHasAnAttendance_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(Task.FromResult(attendance));

            var result = await _attendancesController.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public async Task Attend_ValidRequest_ShouldReturnOk()
        {
            var result = await _attendancesController.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public async Task DeleteAttendance_NoAttendanceWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = await _attendancesController.DeleteAttendance(2);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1))
                .Returns(Task.FromResult(attendance));

            var result = await _attendancesController.DeleteAttendance(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public async Task DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(Task.FromResult(attendance));

            var result = (OkNegotiatedContentResult<int>) await _attendancesController.DeleteAttendance(1);

            result.Content.Should().Be(1);
        }
    }
}
