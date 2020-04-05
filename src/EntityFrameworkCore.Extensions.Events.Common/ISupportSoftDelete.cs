using System;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public interface ISupportSoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}