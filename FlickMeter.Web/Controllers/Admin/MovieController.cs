using FlickMeter.Data;
using FlickMeter.Data.Entities;
using FlickMeter.Data.Enums;
using FlickMeter.Infrastructure;
using FlickMeter.Infrastructure.Enums;
using FlickMeter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlickMeter.Web.Controllers.Admin
{
    [Authorize]
    public class MovieController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Movie> _movieRepo
        {
            get 
            {
                return _unitOfWork.Repository<Movie>();
            }
        }

        public MovieController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int? pageIndex = null)
        {
            int totalCount = 0;
            pageIndex = pageIndex ?? 1;
            var movies = _movieRepo.GetMovies(Language.Telugu, out totalCount, page: pageIndex ?? 1, pageSize: PAGESIZE, includeArtists: false)
                .Select(m => ModelFactoryInstance.Create(m));

            ViewBag.PageSize = PAGESIZE;
            ViewBag.CurrentPage = pageIndex;
            ViewBag.TotalCount = totalCount;
            return View(movies);
        }

        public ActionResult Edit(int id)
        {
            MovieModel movieModel = null;

            var movie = _movieRepo.GetMovieById(id, true, true);

            if (movie != null)
            {
                movieModel = ModelFactoryInstance.Create(movie);
            }

            return View(movieModel);
        }

        public ActionResult Delete(int id)
        {
            var movie = _movieRepo.FindById(id);

            if (movie != null)
            {
                movie.State = ObjectState.Deleted;
                _movieRepo.Delete(movie);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}