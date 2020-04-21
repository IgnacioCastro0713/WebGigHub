using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebGigHub.Core;

namespace WebGigHub.Controllers
{
    public class FolloweesController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var artist = await _unitOfWork.FolloweeRepository.GetFolloweesByFollowerId(userId);
            return View(artist);
        }
    }
}