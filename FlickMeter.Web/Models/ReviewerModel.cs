using FlickMeter.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlickMeter.Web.Models
{
    public class ReviewerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteUrl { get; set; }
    }
}