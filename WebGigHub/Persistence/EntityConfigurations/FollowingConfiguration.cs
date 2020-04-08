using System.Data.Entity.ModelConfiguration;
using WebGigHub.Core.Models;

namespace WebGigHub.Persistence.EntityConfigurations
{
    public class FollowingConfiguration: EntityTypeConfiguration<Following>
    {
        public FollowingConfiguration()
        {
            HasKey(following => new {following.FollowerId, following.FolloweeId});
        }
    }
}