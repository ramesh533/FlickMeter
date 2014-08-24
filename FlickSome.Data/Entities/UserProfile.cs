using FlickSome.Infrastructure;
using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Data.Entities
{
    public class UserProfile : IObjectState
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }

        public ObjectState State { get; set; }
    }
}
