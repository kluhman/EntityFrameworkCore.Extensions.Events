using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public interface ITrackCreateDate
    {
        DateTime CreateDate { get; set; }
    }
}