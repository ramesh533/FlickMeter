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
    public class ReviewsController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int? pageIndex = null)
        {
            int totalCount = 0;
            pageIndex = pageIndex ?? 1;
            //var reviews = _movieRepo.GetMovies(Language.Telugu, out totalCount, page: pageIndex ?? 1, pageSize: PAGESIZE, includeArtists: false)
            //    .Select(m => ModelFactoryInstance.Create(m));

            ViewBag.PageSize = PAGESIZE;
            ViewBag.CurrentPage = pageIndex;
            ViewBag.TotalCount = totalCount;
            return View();
        }
    }
}