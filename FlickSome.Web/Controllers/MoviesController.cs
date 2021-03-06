﻿using FlickSome.Data;
using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlickSome.Web.Controllers
{
    public class MoviesController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IEnumerable<MovieModel> Get()
        //{
        //    int totalCount = 0;
        //    var result = _unitOfWork.Repository<Movie>().GetMovies(out totalCount, includeArtists: true).Select(m => ModelFactoryInstance.Create(m));
        //    return result;
        //}

        public object GetMovies(int? page = null, int? pageSize = null)
        {
            int totalCount = 0;
            var movies = _unitOfWork.Repository<Movie>().GetMovies(Language.Telugu, out totalCount, page: page ?? 1, pageSize: pageSize ?? 10, includeArtists: true)
                .Select(m => ModelFactoryInstance.Create(m));
            return new { count = totalCount, movies = movies };
        }

        public MovieModel GetMovie(int id)
        {
            MovieModel movieModel = null;
            var movie = _unitOfWork.Repository<Movie>().GetMovieById(id, true, true);            

            if (movie != null)
            {
                movieModel = ModelFactoryInstance.Create(movie);
            }
            return movieModel;
        }
    }
}
