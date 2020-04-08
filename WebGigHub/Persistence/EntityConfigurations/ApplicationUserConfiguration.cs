using System.Data.Entity.ModelConfiguration;
using WebGigHub.Core.Models;

namespace WebGigHub.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration: EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            HasMany(user => user.Followers)
                .WithRequired(following => following.Followee)
                .WillCascadeOnDelete(false);
            
            HasMany(user => user.Followees)
                .WithRequired(following => following.Follower)
                .WillCascadeOnDelete(false);

        }
    }
}