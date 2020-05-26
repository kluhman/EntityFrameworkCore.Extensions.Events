using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class Person : ITrackCreateDate, ITrackUpdateDate, ISupportSoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public Address Address { get; set; } = new Address();
        public PhoneNumber PhoneNumber { get; set; } = new PhoneNumber();
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }
    }

    [Owned]
    public class PhoneNumber : ITrackCreateDate, ITrackUpdateDate
    {
        public string Value { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    [Owned]
    public class Address
    {
        public string Line1 { get; set; } = default!;
        public string? Line2 { get; set; }
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
        public string Country { get; set; } = default!;
    }
}