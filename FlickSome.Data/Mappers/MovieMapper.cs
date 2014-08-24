using FlickSome.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data.Mappers
{
    public class MovieMapper : EntityTypeConfiguration<Movie>
    {
        public MovieMapper()
        {
            this.ToTable("Movies");

            this.HasKey(m => m.Id);
            this.Property(m => m.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Id).IsRequired();

            this.Property(m => m.Title).IsRequired();
            this.Property(m => m.Title).HasMaxLength(250);

            this.Property(m => m.ImagePath).IsRequired();
            this.Property(m => m.ImagePath).HasMaxLength(1000);

            this.Property(m => m.Genre).IsRequired();

            this.Property(m => m.ReleaseDate).IsRequired();
            this.Property(m => m.ReleaseDate).HasColumnType("smalldatetime");
            
            this.Property(m => m.Language).IsRequired();

            this.Property(m => m.Tags);

            this.Ignore(m => m.State);
        }
    }
}
