using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public interface ISupportSoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}