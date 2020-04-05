using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public interface ITrackUpdateDate
    {
        DateTime UpdateDate { get; set; }
    }
}