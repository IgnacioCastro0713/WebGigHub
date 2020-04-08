using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using WebGigHub.Core.Dtos;
using WebGigHub.Core.Models;
using WebGigHub.Persistence;

namespace WebGigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController() => _context = new ApplicationDbContext();

        public async Task<IEnumerable<NotificationDto>> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            
            var notification = await _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToListAsync();

            return notification.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public async Task<IHttpActionResult> MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = await _context.UserNotifications
                .Where(notification => notification.UserId == userId && !notification.IsRead)
                .ToListAsync();
            
            notifications.ForEach(notification => notification.Read());

            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}