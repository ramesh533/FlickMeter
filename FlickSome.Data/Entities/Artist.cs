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
    public class Artist : IObjectState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArtistRole PrimaryRole { get; set; }

        public ObjectState State { get; set; }
    }
}
