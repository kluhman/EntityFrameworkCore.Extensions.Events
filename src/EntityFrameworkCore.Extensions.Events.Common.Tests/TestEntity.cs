using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class TestEntity : ITrackCreateDate, ITrackUpdateDate, ISupportSoftDelete
    {
        public int Id { get; set; }
        public string Value { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}