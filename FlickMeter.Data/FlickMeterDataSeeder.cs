using FlickMeter.Data.Entities;
using FlickMeter.Data.Enums;
using FlickMeter.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data
{
    public class FlickMeterDataSeeder
    {
        FlickMeterContext _context;

        public FlickMeterDataSeeder(FlickMeterContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Movies.Count() > 0)
            {
                return;
            }

            _context.Reviewers.Add(new Reviewer() { Name = "Reviewer1", SiteUrl = "http://www.google.com", State = ObjectState.Added });
            _context.Reviewers.Add(new Reviewer() { Name = "Reviewer2", SiteUrl = "http://www.google.com", State = ObjectState.Added });
            _context.SaveChanges();

            _context.SaveChanges();

            for (int i = 1; i <= 100; i++)
            {
                Random random = new Random();
                var genreArray = Enum.GetValues(typeof(Genre));
                var movieArtists = new List<MovieArtist>();
                var movieReviews = new List<MovieReview>();

                Movie movie = new Movie()
                {
                    Title = "Movie" + i,
                    Genre = (Genre)genreArray.GetValue(random.Next(1, genreArray.Length)),
                    ImagePath = "default.jpg",
                    ReleaseDate = DateTime.Now.AddMonths(i * -1),
                    State = ObjectState.Added
                };

                foreach (ArtistRole role in Enum.GetValues(typeof(ArtistRole)))
                {
                    movieArtists.Add(new MovieArtist()
                    {
                        Movie = movie,
                        Artist = new Artist()
                        {
                            Name = role.ToString() + i,
                            PrimaryRole = role,
                            State = ObjectState.Added
                        },
                        Role = role,
                        State = ObjectState.Added
                    });
                }

                foreach (var item in _context.Reviewers)
                {
                    movieReviews.Add(new MovieReview()
                    {
                        Movie = movie,
                        TagLine = "tag line goes here",
                        Rating = random.Next(1, 5),
                        Reviewer = item,
                        Review = "review goes here",
                        ReviewedDate = DateTime.Now.AddMonths(i * -1),
                        State = ObjectState.Added
                    });
                }

                _context.Movies.Add(movie);
                movieArtists.ForEach(ma => _context.MovieArtists.Add(ma));
                movieReviews.ForEach(mr => _context.MovieReviews.Add(mr));
            }

            _context.SaveChanges();
        }
    }
}
