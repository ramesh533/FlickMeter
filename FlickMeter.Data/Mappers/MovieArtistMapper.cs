using FlickMeter.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data.Mappers
{
    public class MovieArtistMapper : EntityTypeConfiguration<MovieArtist>
    {
        public MovieArtistMapper()
        {
            this.ToTable("MovieArtists");

            this.HasKey(ma => ma.Id);
            this.Property(ma => ma.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(ma => ma.Id).IsRequired();
            this.Property(ma => ma.Role).IsRequired();

            this.HasRequired(ma => ma.Movie);
            this.HasRequired(ma => ma.Artist);

            this.Ignore(ma => ma.State);
        }
    }
}
