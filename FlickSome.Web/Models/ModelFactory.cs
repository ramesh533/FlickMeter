using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FlickSome.Web.Models
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
                ReleaseDate = movie.ReleaseDate,
                Tags = movie.Tags
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
                    Reviewer = mr.Reviewer.Name,
                    Movie = mr.Movie == null ? string.Empty : mr.Movie.Title
                };
        }

        public ReviewerModel Create(Reviewer r)
        {
            return new ReviewerModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        SiteUrl = r.SiteUrl
                    };
        }

        public Movie Parse(Movie movie, MovieModel movieModel, IUnitOfWork _unitOfWork)
        {
            movie.Title = movieModel.Title;
            movie.Genre = movieModel.Genre;
            movie.ReleaseDate = movieModel.ReleaseDate;
            movie.ImagePath = movieModel.ImagePath;
            movie.Tags = movieModel.Tags;
            movie.Artists = ParseArtists(movie, movieModel, _unitOfWork);
            return movie;
        }

        public bool TryParse(MovieReview review, ReviewModel reviewModel, IUnitOfWork _unitOfWork, ModelStateDictionary modelState)
        {
            bool isValid = true;
            var movie = _unitOfWork.Repository<Movie>().Query()
                .Filter(m => m.Title.ToLower() == reviewModel.Movie.ToLower())
                .FirstOrDefault();
            if (movie == null)
            {
                modelState.AddModelError("Movie", "Not a valid Movie");
                isValid = false;
            }
            var reviewer = _unitOfWork.Repository<Reviewer>().Query()
                .Filter(r => r.Name.ToLower() == reviewModel.Reviewer.ToLower())
                .FirstOrDefault();
            if (reviewer == null)
            {
                modelState.AddModelError("Reviewer", "Not a valid Reviewer");
                isValid = false;
            }
            review.Movie = movie;
            review.Reviewer = reviewer;
            review.ReviewedDate = reviewModel.ReviewedDate;
            review.TagLine = reviewModel.TagLine;
            review.Rating = reviewModel.Rating;
            review.Review = reviewModel.Review;
            return isValid;
        }

        public Reviewer Parse(Reviewer reviewer, ReviewerModel reviewerModel)
        {
            reviewer.Name = reviewerModel.Name;
            reviewer.SiteUrl = reviewerModel.SiteUrl;
            return reviewer;
        }

        private ICollection<MovieArtist> ParseArtists(Movie movie, MovieModel movieModel, IUnitOfWork _unitOfWork)
        {
            MovieArtist movieArtist;
            ArtistRole[] roles = { ArtistRole.Hero, ArtistRole.Heroin, ArtistRole.Director, ArtistRole.Producer, ArtistRole.MusicDirector };
            string artistName;

            if (movie.Artists == null)
            {
                movie.Artists = new List<MovieArtist>();
            }

            foreach (var role in roles)
            {
                artistName = movieModel.GetNameByRole(role);

                if (!string.IsNullOrEmpty(artistName))
                {
                    var artist = _unitOfWork.Repository<Artist>().Query().Filter(a => string.Equals(artistName, a.Name)).FirstOrDefault();

                    if (artist == null)
                    {
                        artist = new Artist() { Name = artistName, PrimaryRole = role, State = ObjectState.Added };
                    }

                    if (string.IsNullOrEmpty(movie.GetArtistName(role)))
                    {
                        movieArtist = new MovieArtist() { Artist = artist, Movie = movie, Role = role, State = ObjectState.Added };
                        movie.Artists.Add(movieArtist);
                    }
                    else if (movie.GetArtistName(role) != artistName)
                    {
                        movieArtist = movie.Artists.Where(ma => ma.Role == role).FirstOrDefault();
                        movieArtist.Artist = artist;
                        movieArtist.State = ObjectState.Modified;
                    }
                }
                else if (!string.IsNullOrEmpty(movie.GetArtistName(role)))
                {
                    movieArtist = movie.Artists.Where(ma => ma.Role == role).FirstOrDefault();
                    movieArtist.State = ObjectState.Deleted;
                }
            }
            return movie.Artists;
        }
    
}
}