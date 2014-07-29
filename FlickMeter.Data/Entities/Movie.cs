using FlickMeter.Data.Enums;
using FlickMeter.Infrastructure;
using FlickMeter.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Data.Entities
{
    public class Movie : IObjectState
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual DateTime ReleaseDate { get; set; }

        public virtual ICollection<MovieArtist> Artists { get; set; }
        public virtual ICollection<MovieReview> Reviews { get; set; }

        public ObjectState State { get; set; }
    }
}
