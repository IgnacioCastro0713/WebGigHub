using System.Data.Entity.Core;
using System.Data.Entity.ModelConfiguration;
using System.Reflection;
using WebGigHub.Core.Models;

namespace WebGigHub.Persistence.EntityConfigurations
{
    public class GenreConfiguration: EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            Property(genre => genre.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}