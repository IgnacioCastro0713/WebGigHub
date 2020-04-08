using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebGigHub.Core.Dtos;
using WebGigHub.Core.Models;
using WebGigHub.Persistence;

namespace WebGigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController() => _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (await _context.Followings.AnyAsync(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
            {
                return BadRequest("Following already exist.");
            }

            _context.Followings.Add(new Following
            {
                FollowerId = userId,
                FolloweeId= dto.FolloweeId
            });

            await _context.SaveChangesAsync();
            
            return Ok();
        }


        [HttpDelete]
        public async Task<IHttpActionResult> DeleteFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var follow = await _context.Followings
                .SingleAsync(following => following.FollowerId == userId && following.FolloweeId == id);

            if (follow == null)
            {
                return BadRequest();
            }

            _context.Followings.Remove(follow);
            await _context.SaveChangesAsync();
            
            return Ok(id);
        }
        
    }
}
