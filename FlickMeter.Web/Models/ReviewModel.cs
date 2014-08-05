using FlickMeter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlickMeter.Web.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public DateTime ReviewedDate { get; set; }
        public int Rating { get; set; }
        public string TagLine { get; set; }
        public string Review { get; set; }
        public ReviewerModel Reviewer { get; set; }
    }
}