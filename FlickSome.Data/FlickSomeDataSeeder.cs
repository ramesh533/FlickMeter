using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;

namespace FlickSome.Data
{
    public class FlickSomeDataSeeder
    {
        FlickSomeContext _context;

        public FlickSomeDataSeeder(FlickSomeContext context)
        {
            _context = context;
            _context.IgnoreObjectState = true;
        }

        public void Seed()
        {
            SeedUsers();

            if (_context.Reviewers.Count() == 0)
            {
                _context.Reviewers.Add(new Reviewer() { Name = "Reviewer1", SiteUrl = "http://www.google.com", State = ObjectState.Added });
                _context.Reviewers.Add(new Reviewer() { Name = "Reviewer2", SiteUrl = "http://www.google.com", State = ObjectState.Added });
                _context.SaveChanges();
            }

            if (_context.Movies.Count() == 0)
            {
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
                        Language = Language.Telugu,
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

        private void SeedUsers()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("flickSomeConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }

            if (!Roles.RoleExists("Administrator"))
                Roles.CreateRole("Administrator");

            if (!WebSecurity.UserExists("ramesh"))
                WebSecurity.CreateUserAndAccount("ramesh", "123456Aa", new { Mobile = "+19725000000" });

            if (!Roles.GetRolesForUser("ramesh").Contains("Administrator"))
                Roles.AddUsersToRoles(new[] { "ramesh" }, new[] { "Administrator" });
        }
    }
}
