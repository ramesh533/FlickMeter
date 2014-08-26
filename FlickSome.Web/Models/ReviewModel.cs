using FlickMeter.Web.Models;
using FlickSome.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlickSome.Web.Models
{
    public class ReviewModel : BaseModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage = "ReviewedDate is required")]
        public DateTime ReviewedDate { get; set; }
        [Required(ErrorMessage = "Rating is required")]
        [Range(0, 5)]
        public double Rating { get; set; }
        [Required(ErrorMessage = "TagLine is required")]
        public string TagLine { get; set; }
        [Required(ErrorMessage = "Review is required")]
        public string Review { get; set; }
        [Required(ErrorMessage = "Movie is required")]
        public string Movie { get; set; }
        [Required(ErrorMessage = "Reviewer is required")]
        public string Reviewer { get; set; }
    }
}