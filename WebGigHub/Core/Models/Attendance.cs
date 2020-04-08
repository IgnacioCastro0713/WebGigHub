using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebGigHub.Core.Models;

namespace WebGigHub.Core.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }
        public int GigId { get; set; }
        public string AttendeeId { get; set; }
        
    }
    
}