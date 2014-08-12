using FlickMeter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace FlickMeter.Web.Controllers
{
    public class BaseController : Controller
    {
        private ModelFactory _modelFactory;
        public const int PAGESIZE = 10;

        protected ModelFactory ModelFactoryInstance
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory();
                }
                return _modelFactory;
            }
        }
    }
}
