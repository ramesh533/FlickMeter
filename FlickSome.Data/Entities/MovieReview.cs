using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data.Entities
{
    public class MovieReview : IObjectState
    {
        public int Id { get; set; }
        public virtual DateTime ReviewedDate { get; set; }
        public double Rating { get; set; }
        public string TagLine { get; set; }
        public string Review { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Reviewer Reviewer { get; set; }

        public ObjectState State { get; set; }
    }
}
