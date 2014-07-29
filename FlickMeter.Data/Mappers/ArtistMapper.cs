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
    public class ArtistMapper : EntityTypeConfiguration<Artist>
    {
        public ArtistMapper()
        {
            this.ToTable("Artists");

            this.HasKey(a => a.Id);
            this.Property(a => a.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(a => a.Id).IsRequired();

            this.Property(a => a.Name).IsRequired();
            this.Property(a => a.Name).HasMaxLength(255);

            this.Ignore(a => a.State);
        }
    }
}
