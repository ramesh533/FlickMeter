using FlickSome.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickSome.Infrastructure
{
    public interface IObjectState
    {
        ObjectState State { get; set; }
    }
}
