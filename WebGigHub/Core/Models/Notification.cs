using System;
using System.ComponentModel.DataAnnotations;

namespace WebGigHub.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public Gig Gig { get; private set; }
        
        protected Notification()
        {
            
        }

        private Notification(NotificationType notificationType, Gig gig)
        {
            Type = notificationType;
            Gig = gig ?? throw new ArgumentNullException("gig");
            DateTime = DateTime.Now;
        }

        public static Notification GidCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }
        
        public static Notification GidUpdated(Gig newGig, DateTime originalDateTime, string origialVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, newGig)
            {
                OriginalDateTime = originalDateTime, OriginalVenue = origialVenue
            };
            return notification;
        }

        public static Notification GigCancel(Gig gigCancel)
        {
            return new Notification(NotificationType.GigCanceled, gigCancel);
        }
    }
}