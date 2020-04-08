using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGigHub.Controllers;
using WebGigHub.Core.Models;
using WebGigHub.Core.ViewModels;
using WebGigHub.IntegrationTest.Extensions;
using WebGigHub.Persistence;

namespace WebGigHub.IntegrationTest.Controllers
{
    [TestFixture]
    public class GigsControllerTest
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public async Task Mine_WheCalled_ShouldReturnedUpcomingGigs()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.First();

            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = await _controller.Mine();
            result.As<ViewResult>().Model.As<List<Gig>>().Should().HaveCount(1);
        }


        [Test, Isolated]
        public async Task Mine_WheCalled_ShouldUpdateTheGivenGig()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            var result = await _controller.Update(new GigFormViewModel
            {
                Id = gig.Id,
                Date =  DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time = "20:00",
                Venue = "Venue",
                Genre = 2
            });
            
            // Assert
            _context.Entry(gig).Reload();

            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Venue");
            gig.GenreId.Should().Be(2);
        }
    }
}