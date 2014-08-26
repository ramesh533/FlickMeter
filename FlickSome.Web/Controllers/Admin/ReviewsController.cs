using FlickSome.Data.Entities;
using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using FlickSome.Web.Controllers;
using FlickSome.Web.Models;
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

        private IRepository<MovieReview> _reviewRepo
        {
            get
            {
                return _unitOfWork.Repository<MovieReview>();
            }
        }

        public ReviewsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int? pageIndex = null)
        {
            int totalCount = 0;
            pageIndex = pageIndex ?? 1;
            var reviews = _reviewRepo.Query()
                .Include(r => r.Reviewer)
                .OrderBy(r => r.OrderByDescending(review => review.ReviewedDate))
                .GetPage(pageIndex ?? 1, PAGESIZE, out totalCount)
                .Select(r => ModelFactoryInstance.Create(r));

            ViewBag.PageSize = PAGESIZE;
            ViewBag.CurrentPage = pageIndex;
            ViewBag.TotalCount = totalCount;
            return View(reviews);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ReviewModel reviewModel = null;

            if (id != null)
            {
                var reviewer = _reviewRepo.Query().Filter(r => r.Id == id)
                    .Include(r => r.Reviewer)
                    .Include(r => r.Movie)
                    .FirstOrDefault();

                if (reviewer != null)
                {
                    reviewModel = ModelFactoryInstance.Create(reviewer);
                }
            }

            if (reviewModel == null)
            {
                reviewModel = new ReviewModel();
                reviewModel.ReviewedDate = DateTime.Now;
            }

            return View(reviewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                MovieReview review;

                if (id == default(int))
                {
                    review = new MovieReview();
                    if (!ModelFactoryInstance.TryParse(review, reviewModel, _unitOfWork, ModelState))
                    {
                        return View(reviewModel);
                    }
                    review.State = ObjectState.Added;
                    _reviewRepo.Insert(review);
                }
                else
                {
                    review = _reviewRepo.FindById(id);
                    if (!ModelFactoryInstance.TryParse(review, reviewModel, _unitOfWork, ModelState))
                    {
                        return View(reviewModel);
                    }
                    review.State = ObjectState.Modified;
                    _reviewRepo.Update(review);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(reviewModel);
        }

        public ActionResult Delete(int id)
        {
            var review = _reviewRepo.FindById(id);

            if (review != null)
            {
                review.State = ObjectState.Deleted;
                _reviewRepo.Delete(review);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}