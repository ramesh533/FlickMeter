﻿using FlickMeter.Data.Entities;
using FlickMeter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data
{
    public static class FlickMeterRepository
    {
        public static IEnumerable<Movie> GetMovies(this IRepository<Movie> movieRepository, bool includeArtists = false, bool includeReviews = false)
        {
            var movieRepoQuery = movieRepository.Query().OrderBy(mq => mq.OrderByDescending(m => m.ReleaseDate));

            if (includeArtists)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Artists).Include(m => m.Artists.Select(ma => ma.Artist));
            }

            if (includeReviews)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Reviews).Include(m => m.Reviews.Select(mr => mr.Reviewer));
            }

            return movieRepoQuery.Get();
        }

        public static Movie GetMovieById(this IRepository<Movie> movieRepository, int id, bool includeArtists = false, bool includeReviews = false)
        {
            var movieRepoQuery = movieRepository.Query().Filter(m => m.Id == id);
            
            if (includeArtists)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Artists).Include(m => m.Artists.Select(ma => ma.Artist));
            }

            if (includeReviews)
            {
                movieRepoQuery = movieRepoQuery.Include(m => m.Reviews).Include(m => m.Reviews.Select(mr => mr.Reviewer));
            }

            return movieRepoQuery.Get().SingleOrDefault();
        }
    }
}