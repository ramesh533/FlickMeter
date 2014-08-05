using FlickMeter.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlickMeter.Web.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;

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
