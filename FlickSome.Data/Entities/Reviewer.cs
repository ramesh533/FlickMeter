﻿using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data.Entities
{
    public class Reviewer : IObjectState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SiteUrl { get; set; }

        public virtual ICollection<MovieReview> Reviews { get; set; }

        public ObjectState State { get; set; }
    }
}
