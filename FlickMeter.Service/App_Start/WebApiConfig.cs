﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace FlickMeter.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "MovieListApi",
                routeTemplate: "api/movies/{page}",
                defaults: new { controller = "movies", page = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "MovieApi",
                routeTemplate: "api/movie/{id}",
                defaults: new { controller = "movies" }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //todo: need to set response type configurable.
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
