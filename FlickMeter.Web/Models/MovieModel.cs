using FlickMeter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlickMeter.Web.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Artists { get; set; }
        public string Director { get; set; }
        public string MusicDirector { get; set; }
        public string Producer { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}