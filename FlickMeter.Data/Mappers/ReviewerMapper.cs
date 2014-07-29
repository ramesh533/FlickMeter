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
    public class ReviewerMapper : EntityTypeConfiguration<Reviewer>
    {
        public ReviewerMapper()
        {
            this.ToTable("Reviewers");

            this.HasKey(r => r.Id);
            this.Property(r => r.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(r => r.Id).IsRequired();

            this.Property(r => r.Name).IsRequired();
            this.Property(r => r.Name).HasMaxLength(100);

            this.Ignore(r => r.State);
        }
    }
}
