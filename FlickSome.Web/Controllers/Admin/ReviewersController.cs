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
    public class ReviewersController : BaseController
    {
        private IUnitOfWork _unitOfWork;

        private IRepository<Reviewer> _reviewerRepo
        {
            get
            {
                return _unitOfWork.Repository<Reviewer>();
            }
        }

        public ReviewersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(int? pageIndex = null)
        {
            int totalCount = 0;
            pageIndex = pageIndex ?? 1;
            var reviewers = _reviewerRepo.Query().OrderBy(r => r.OrderBy(reviewer => reviewer.Name)).GetPage(pageIndex ?? 1, PAGESIZE, out totalCount)
                .Select(r => ModelFactoryInstance.Create(r));

            ViewBag.PageSize = PAGESIZE;
            ViewBag.CurrentPage = pageIndex;
            ViewBag.TotalCount = totalCount;
            return View(reviewers);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ReviewerModel reviewerModel = null;

            if (id != null)
            {
                var reviewer = _reviewerRepo.FindById(id);

                if (reviewer != null)
                {
                    reviewerModel = ModelFactoryInstance.Create(reviewer);
                }
            }

            if (reviewerModel == null)
            {
                reviewerModel = new ReviewerModel();
            }

            return View(reviewerModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, ReviewerModel reviewerModel)
        {
            if (ModelState.IsValid)
            {
                Reviewer reviewer;                

                if (id == default(int))
                {
                    reviewer = ModelFactoryInstance.Parse(new Reviewer(), reviewerModel);
                    reviewer.State = ObjectState.Added;
                    _reviewerRepo.Insert(reviewer);
                }
                else
                {
                    reviewer = _reviewerRepo.FindById(id);
                    reviewer = ModelFactoryInstance.Parse(reviewer, reviewerModel);
                    reviewer.State = ObjectState.Modified;
                    _reviewerRepo.Update(reviewer);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(reviewerModel);
        }

        public ActionResult Delete(int id)
        {
            var reviewer = _reviewerRepo.FindById(id);

            if (reviewer != null)
            {
                reviewer.State = ObjectState.Deleted;
                _reviewerRepo.Delete(reviewer);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }
    }
}