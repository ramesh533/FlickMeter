using FlickSome.Data.Entities;
using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlickMeter.Web.Controllers.Admin
{
    [Authorize]
    public class LookupController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public LookupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult Movies(string query)
        {
            int totalCount;
            var result = _unitOfWork.Repository<Movie>().Query()
                .Filter(m => m.Title.ToLower().StartsWith(query.ToLower()))
                .OrderBy(m => m.OrderBy(movie => movie.Title))
                .GetPage(1, 20, out totalCount).Select(m => m.Title);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Reviewers(string query)
        {
            int totalCount;
            var result = _unitOfWork.Repository<Reviewer>().Query()
                .Filter(r => r.Name.ToLower().StartsWith(query.ToLower()))
                .OrderBy(r => r.OrderBy(reviewer => reviewer.Name))
                .GetPage(1, 20, out totalCount).Select(r => r.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Heroes(string query)
        {
            return GetArtists(query, ArtistRole.Hero);
        }

        [HttpGet]
        public ActionResult Heroins(string query)
        {
            return GetArtists(query, ArtistRole.Heroin);
        }

        [HttpGet]
        public ActionResult Directors(string query)
        {
            return GetArtists(query, ArtistRole.Director);
        }

        [HttpGet]
        public ActionResult Producers(string query)
        {
            return GetArtists(query, ArtistRole.Producer);
        }

        [HttpGet]
        public ActionResult MusicDirectors(string query)
        {
            return GetArtists(query, ArtistRole.MusicDirector);
        }

        private ActionResult GetArtists(string query, ArtistRole role)
        {
            int totalCount;
            var result = _unitOfWork.Repository<Artist>().Query()
                .Filter(a => a.PrimaryRole == role && a.Name.ToLower().StartsWith(query.ToLower()))
                .OrderBy(a => a.OrderBy(artist => artist.Name))
                .GetPage(1, 20, out totalCount).Select(a => a.Name);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}