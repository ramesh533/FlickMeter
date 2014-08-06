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
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Language Language { get; set; }

        public virtual ICollection<MovieArtist> Artists { get; set; }
        public virtual ICollection<MovieReview> Reviews { get; set; }

        public ObjectState State { get; set; }

        public string GetArtistName(ArtistRole role)
        {
            string artist = string.Empty;
            if (this.Artists !=null && this.Artists.Any(ma => ma.Role == role))
            {
                artist = string.Join(",", this.Artists.Where(ma => ma.Role == role).Select(ma => ma.Artist.Name));
            }
            return artist;
        }
    }
}
