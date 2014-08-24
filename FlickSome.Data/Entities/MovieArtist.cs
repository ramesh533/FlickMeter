using FlickSome.Data.Enums;
using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data.Entities
{
    public class MovieArtist : IObjectState
    {
        public int Id { get; set; }
        public ArtistRole Role { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Artist Artist { get; set; }

        public ObjectState State { get; set; }
    }
}
