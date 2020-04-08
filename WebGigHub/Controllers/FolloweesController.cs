using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebGigHub.Core.Models;
using WebGigHub.Persistence;

namespace WebGigHub.Controllers
{
    public class FolloweesController : Controller
    {
        
        private readonly ApplicationDbContext _context;

        public FolloweesController() => _context = new ApplicationDbContext();
        
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var artist = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
            
            return View(artist);
        }
    }
}