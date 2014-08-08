using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlickMeter.Web.Controllers.Admin
{
    [Authorize(Roles="Admin")]
    public class MovieController : Controller
    {
    }
}