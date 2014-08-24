using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickSome.Data;
using FlickSome.Infrastructure;
using FlickSome.Data.Entities;

namespace FlickSome.Test
{
    [TestClass]
    public class RepoTest
    {
        [TestMethod]
        public void GetMovieByIDTest()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new FlickSomeContext()))
            {
                var movie = unitOfWork.Repository<Movie>().GetMovieById(2);

                movie.State = Infrastructure.Enums.ObjectState.Deleted;

                unitOfWork.Save();

                movie = unitOfWork.Repository<Movie>().GetMovieById(2);

                Assert.IsNull(movie); ;
            }
        }

        [TestMethod]
        public void ReviewAddTest()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new FlickSomeContext()))
            {
                var reviewRepo = unitOfWork.Repository<MovieReview>();
                reviewRepo.Insert(new MovieReview()
                {
                    Movie = new Movie() { Id = 1, State = Infrastructure.Enums.ObjectState.Unchanged },
                    Rating = 5,
                    Review = "Review goes here...",
                    ReviewedDate = DateTime.Now,
                    Reviewer = new Reviewer() { Name = "TestReviewer", SiteUrl = "testreviewer.com", State = Infrastructure.Enums.ObjectState.Added},
                    State = Infrastructure.Enums.ObjectState.Added,
                    TagLine = "Test review tagline"
                });
                unitOfWork.Save();
            }
        }
    }
}
