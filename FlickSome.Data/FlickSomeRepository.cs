using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data
{
    public static class FlickSomeRepository
    {
        public static IEnumerable<Movie> GetMovies(this IRepository<Movie> movieRepository, Language language, out int totalCount,
                                                   int page = 1, int pageSize = 10, bool includeArtists = false, bool includeReviews = false)
        {
            var movieRepoQuery = movieRepository.Query().Filter(m => m.Language == language)
                                                        .OrderBy(mq => mq.OrderByDescending(m => m.ReleaseDate));

            if (includeArtists)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Artists)
                                               .Include(m => m.Artists.Select(ma => ma.Artist));
            }

            if (includeReviews)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Reviews)
                                               .Include(m => m.Reviews.Select(mr => mr.Reviewer));
            }

            return movieRepoQuery.GetPage(page, pageSize, out totalCount);
        }

        public static Movie GetMovieById(this IRepository<Movie> movieRepository, int id, bool includeArtists = false, bool includeReviews = false)
        {
            var movieRepoQuery = movieRepository.Query().Filter(m => m.Id == id);

            if (includeArtists)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Artists)
                                               .Include(m => m.Artists.Select(ma => ma.Artist));
            }

            if (includeReviews)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Reviews)
                                               .Include(m => m.Reviews.Select(mr => mr.Reviewer));
            }

            return movieRepoQuery.Get().SingleOrDefault();
        }

        public static void SaveMovie(this IRepository<Movie> movieRepository, Movie movie)
        {
            if (movie.State == ObjectState.Added)
            {
                movieRepository.Insert(movie);
            }
            else if(movie.State == ObjectState.Modified)
            {
                movieRepository.Update(movie);
            }
        }
    }
}
