using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlickMeter.Data;
using FlickMeter.Infrastructure;
using FlickMeter.Data.Entities;

namespace FlickMeter.Test
{
    [TestClass]
    public class RepoTest
    {
        [TestMethod]
        public void GetMovieByIDTest()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new FlickMeterContext()))
            {
                var movie = unitOfWork.Repository<Movie>().GetMovieById(1);
                Assert.AreEqual(1, movie.Id);
            }
        }
    }
}
