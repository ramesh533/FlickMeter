using FlickSome.Data;
using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using FlickSome.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FlickSome.Web.Controllers.Admin
{
    [Authorize]
    public class MoviesController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private const string _PATH = "~/Content/images/";
        private IRepository<Movie> _movieRepo
        {
            get 
            {
                return _unitOfWork.Repository<Movie>();
            }
        }

        public MoviesController(IUnitOfWork unitOfWork)
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            MovieModel movieModel = null;

            if (id != null)
            {
                var movie = _movieRepo.GetMovieById((int) id, true, true);

                if (movie != null)
                {
                    movieModel = ModelFactoryInstance.Create(movie);
                }
            }
            
            if(movieModel == null)
            {
                movieModel = new MovieModel();
            }

            return View(movieModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                Movie movie;
                bool hasNewImage = false;
                string oldImagePath = string.Empty;

                if (Request.Files["Image"] != null)
                {
                    HttpPostedFileBase image = Request.Files["Image"];
                    if (image.ContentLength > 0)
                    {
                        movieModel.ImagePath = movieModel.Title + "_" + image.FileName;
                        image.SaveAs(Server.MapPath(_PATH) + "\\" + movieModel.ImagePath);
                        hasNewImage = true;
                    }
                }

                if (id == default(int))
                {
                    movie = ModelFactoryInstance.Parse(new Movie(), movieModel, _unitOfWork);
                    movie.State = ObjectState.Added;
                    _movieRepo.InsertGraph(movie);
                }
                else
                {
                    movie = _movieRepo.GetMovieById(id, includeArtists: true);
                    if (hasNewImage)
                    {
                        oldImagePath = Server.MapPath(_PATH) + "\\" + movie.ImagePath;
                    }
                    movie = ModelFactoryInstance.Parse(movie, movieModel, _unitOfWork);
                    movie.State = ObjectState.Modified;
                    _movieRepo.Update(movie);
                }
                _unitOfWork.Save();

                if (hasNewImage)
                {
                    if (!string.IsNullOrEmpty(oldImagePath) && System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    return RedirectToAction("Crop", new { id = movie.Id, path = movie.ImagePath });
                }
                return RedirectToAction("Index");
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

        public ActionResult Crop(int id, string path)
        {
            var image = new WebImage(_PATH + path);
            ImageModel imageModel = new ImageModel()
            {
                ImagePath = path,
                Height = image.Height,
                Width = image.Width,
                Top = Convert.ToInt32(image.Height * 0.1),
                Left = Convert.ToInt32(image.Width * 0.1),
                Right = Convert.ToInt32(image.Width * 0.9),
                Bottom = Convert.ToInt32(image.Height * 0.9)
            };
            return View(imageModel);
        }

        [HttpPost]
        public ActionResult Crop(ImageModel imageModel)
        {
            WebImage image = new WebImage(_PATH + imageModel.ImagePath);
            var height = image.Height;
            var width = image.Width;
            var top = imageModel.Top;
            var left = imageModel.Left;
            var bottom = imageModel.Bottom;
            var right = imageModel.Right;

            image.Crop(top, left, height - bottom, width - right);
            image.Resize(140, 200, false, false);
            image.Save(_PATH + imageModel.ImagePath);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Heroes(string query)
        {
            return GetArtists(query, ArtistRole.Hero);
        }

        private ActionResult GetArtists(string query, ArtistRole role)
        {
            int totalCount;
            var result = _unitOfWork.Repository<Artist>().Query()
                .Filter(a => a.PrimaryRole == role && a.Name.ToLower().StartsWith(query.ToLower()))
                .OrderBy(a => a.OrderBy(artist => artist.Name))
                .GetPage(1, 10, out totalCount).Select(a => a.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}