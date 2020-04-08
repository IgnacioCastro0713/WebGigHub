using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Web.Http.Results;
using WebGigHub.Controllers.Api;
using WebGigHub.Core;
using WebGigHub.Core.Models;
using WebGigHub.Core.Repositories;
using WebGigHub.Test.Extensions;

namespace WebGigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private GigsController _gigsController;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();
            
            var mockUoK = new Mock<IUnitOfWork>();
            mockUoK.SetupGet(work => work.GigRepository).Returns(_mockRepository.Object);
            
            _gigsController = new GigsController(mockUoK.Object);
            _userId = "1";
            _gigsController.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public async Task Cancel_NoGigWithGivenIdExist_ShouldReturnNotFound()
        {
            var result = await _gigsController.Cancel(8);
            
            result.Should().BeOfType<NotFoundResult>();
        }


        [TestMethod]
        public async Task Cancel_GigIsCancel_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(repository => repository.GetGigWithAttendances(2))
                .Returns(Task.FromResult(gig));
            
            var result = await _gigsController.Cancel(2);
            
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task Cancel_UserCancelingAnotherUsersGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig
            {
                ArtistId = $"{_userId}-"
            };
            
            _mockRepository.Setup(repository => repository.GetGigWithAttendances(2))
                .Returns(Task.FromResult(gig));
            
            var result = await _gigsController.Cancel(2);
            
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public async Task Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig
            {
                ArtistId = _userId
            };
            
            _mockRepository.Setup(repository => repository.GetGigWithAttendances(2))
                .Returns(Task.FromResult(gig));
            
            var result = await _gigsController.Cancel(2);
            
            result.Should().BeOfType<OkResult>();
        }
    }
}
