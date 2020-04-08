using System.Data.Entity.ModelConfiguration;
using WebGigHub.Core.Models;

namespace WebGigHub.Persistence.EntityConfigurations
{
    public class NotificationConfiguration: EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(notification => notification.Gig);
        }
    }
}