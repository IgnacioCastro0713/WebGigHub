using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebGigHub.Core;
using WebGigHub.Core.Models;
using WebGigHub.Core.ViewModels;

namespace WebGigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [Authorize]
        public async Task<ActionResult> Mine()
        {
            var gigs = await _unitOfWork.GigRepository
                .GetUpcomingGigs(User.Identity.GetUserId());
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = _unitOfWork.GigRepository.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Attending Gigs",
                Attendances = _unitOfWork.AttendanceRepository.GetFutureAttendances(userId)
                    .ToLookup(attendance => attendance.GigId)
            };

            return View("Gigs", viewModel);
        }


        [Authorize]
        public async Task<ActionResult> Create()
        {
            ViewBag.Title = "Add";

            var viewModel = new GigFormViewModel
            {
                Genres = await _unitOfWork.GenreRepository.GetGenres(),
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Add";
                viewModel.Genres = await _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };


            _unitOfWork.GigRepository.Add(gig);
            await _unitOfWork.Complete();
            return RedirectToAction("Mine");
        }

        [Authorize]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Title = "Edit";
            var gig = await _unitOfWork.GigRepository.GetGig(id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            if (gig.ArtistId != User.Identity.GetUserId())
            {
                return new HttpUnauthorizedResult();
            }

            var viewModel = new GigFormViewModel
            {
                Genres = await _unitOfWork.GenreRepository.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy", CultureInfo.InvariantCulture),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Id = gig.Id
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Edit";
                viewModel.Genres = await _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = await _unitOfWork.GigRepository.GetGigWithAttendances(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel);

            await _unitOfWork.Complete();

            return RedirectToAction("Mine");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        public async Task<ActionResult> Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = await _unitOfWork.GigRepository.GetGigDetail(id);

            if (gig == null)
            {
                return HttpNotFound();
            }

            var viewModel = new GigDetailsViewModel
            {
                Gig = gig
            };

            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsAttending = await _unitOfWork.AttendanceRepository.IsAttending(gig.Id, userId);
                viewModel.IsFollowing = await _unitOfWork.FollowingRepository.IsFollowing(gig.ArtistId, userId);
            }

            return View(viewModel);
        }
    }
}