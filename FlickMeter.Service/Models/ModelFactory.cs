using FlickMeter.Data.Entities;
using FlickMeter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FlickMeter.Service.Models
{
    public class ModelFactory
    {
        public MovieModel Create(Movie movie)
        {
            StringBuilder sbArtists = new StringBuilder();

            var movieModel = new MovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                ImagePath = movie.ImagePath,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate
            };

            if (movie.Artists != null)
            {
                if (movie.Artists.Any(ma => ma.Role == ArtistRole.Hero))
                {
                    sbArtists.AppendFormat("{0},", movie.Artists.Where(ma => ma.Role == ArtistRole.Hero).FirstOrDefault().Artist.Name);
                }

                if (movie.Artists.Any(ma => ma.Role == ArtistRole.Heroin))
                {
                    sbArtists.AppendFormat("{0},", movie.Artists.Where(ma => ma.Role == ArtistRole.Heroin).FirstOrDefault().Artist.Name);
                }

                if (movie.Artists.Any(ma => ma.Role != ArtistRole.Hero && ma.Role != ArtistRole.Heroin))
                {
                    movie.Artists.Where(ma => ma.Role != ArtistRole.Hero && ma.Role != ArtistRole.Heroin).ToList().ForEach(ma =>
                    {
                        sbArtists.AppendFormat("{0},", ma.Artist.Name);
                    });
                }

                movieModel.Artists = sbArtists.ToString().TrimEnd(',');
            }

            if (movie.Reviews != null)
            {
                movieModel.Reviews = new List<ReviewModel>();
                movie.Reviews.ToList().ForEach(mr =>
                {
                    movieModel.Reviews.Add(Create(mr));
                });
            }
            return movieModel;
        }

        public ReviewModel Create(MovieReview mr)
        {
            return new ReviewModel()
                {
                    Id = mr.Id,
                    ReviewedDate = mr.ReviewedDate,
                    Rating = mr.Rating,
                    TagLine = mr.TagLine,
                    Review = mr.Review,
                    Reviewer = new ReviewerModel()
                    {
                        Id = mr.Reviewer.Id,
                        Name = mr.Reviewer.Name,
                        SiteUrl = mr.Reviewer.SiteUrl
                    }
                };
        }
    }
}