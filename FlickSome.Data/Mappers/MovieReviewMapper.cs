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
    public class MovieReviewMapper : EntityTypeConfiguration<MovieReview>
    {
        public MovieReviewMapper()
        {
            this.ToTable("MovieReviews");

            this.HasKey(mr => mr.Id);
            this.Property(mr => mr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(mr => mr.Id).IsRequired();

            this.HasRequired(mr => mr.Movie);
            this.HasRequired(mr => mr.Reviewer);

            this.Property(mr => mr.ReviewedDate).IsRequired();
            this.Property(mr => mr.ReviewedDate).HasColumnType("smalldatetime");

            this.Property(mr => mr.Rating).IsRequired();
            this.Property(mr => mr.TagLine).IsRequired();
            this.Property(mr => mr.Review).IsRequired();

            this.Ignore(mr => mr.State);
        }
    }
}
