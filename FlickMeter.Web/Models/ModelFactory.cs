using FlickMeter.Data.Entities;
using FlickMeter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FlickMeter.Web.Models
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
                movieModel.Hero = movie.GetArtistName(ArtistRole.Hero);
                movieModel.Heroin = movie.GetArtistName(ArtistRole.Heroin);
                movieModel.CharacterArtists = movie.GetArtistName(ArtistRole.CharacterArtist);

                movieModel.Director = movie.GetArtistName(ArtistRole.Director);
                movieModel.MusicDirector = movie.GetArtistName(ArtistRole.MusicDirector);
                movieModel.Producer = movie.GetArtistName(ArtistRole.Producer);
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