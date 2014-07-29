using FlickMeter.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickMeter.Infrastructure
{
    public interface IObjectState
    {
        ObjectState State { get; set; }
    }
}
