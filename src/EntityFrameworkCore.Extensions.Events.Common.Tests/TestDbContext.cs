using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class TestDbContext : DbContextWithEvents
    {
        public TestDbContext(DbContextOptions options) : base(options, new IEventHandler[0])
        {
        }

        public TestDbContext(IEnumerable<IEventHandler> eventHandlers) : base(eventHandlers)
        {
        }

        public TestDbContext(DbContextOptions options, ICollection<IEventHandler> eventHandlers) : base(options, eventHandlers)
        {
        }

        public DbSet<Person> People { get; set; } = default!;
    }
}