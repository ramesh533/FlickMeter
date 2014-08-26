using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlickMeter.Web.Models
{
    public abstract class BaseModel
    {
        public string WebRoot
        {
            get
            {
                return "http://localhost/FlickSome";
            }
        }
    }
}