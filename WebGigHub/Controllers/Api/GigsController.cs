using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web.Http;
using WebGigHub.Core;

namespace WebGigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpDelete]
        public async Task<IHttpActionResult> Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = await _unitOfWork.GigRepository.GetGigWithAttendances(id);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();

            await _unitOfWork.Complete();

            return Ok();
        }
    }
}