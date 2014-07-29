using FlickMeter.Data;
using FlickMeter.Data.Entities;
using FlickMeter.Infrastructure;
using FlickMeter.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlickMeter.Service.Controllers
{
    public class MoviesController : BaseApiController
    {
        private IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MovieModel> Get()
        {
            return _unitOfWork.Repository<Movie>().GetMovies(true).Select(m => ModelFactoryInstance.Create(m));
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
