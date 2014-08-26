using FlickMeter.Web.Models;
using FlickSome.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlickSome.Web.Models
{
    public class ReviewerModel : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "SiteUrl is required")]
        public string SiteUrl { get; set; }
    }
}