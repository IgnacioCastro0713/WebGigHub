using System.Data.Entity.ModelConfiguration;
using WebGigHub.Core.Models;

namespace WebGigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : EntityTypeConfiguration<Gig>
    {
        public GigConfiguration()
        {
            Property(gig => gig.ArtistId)
                .IsRequired();

            Property(gig => gig.GenreId)
                .IsRequired();

            Property(gig => gig.Venue)
                .IsRequired()
                .HasMaxLength(255);

            HasMany(gig => gig.Attendances)
                .WithRequired(attendance =>  attendance.Gig)
                .WillCascadeOnDelete(false);
        }
    }
}