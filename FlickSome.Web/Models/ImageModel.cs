using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlickSome.Web.Models
{
    public class ImageModel
    {
        public string ImagePath {get; set;}
        public int Height {get; set;}
        public int Width {get; set;}
        public int Top {get; set;}
        public int Left {get; set;}
        public int Right {get; set;}
        public int Bottom { get; set; }
    }
}