using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Tests
{
    public class TestDbContext : DbContextWithEvents
    {
        public TestDbContext(IEnumerable<IEventHandler> eventHandlers) : base(eventHandlers)
        {
        }

        public TestDbContext(DbContextOptions options, ICollection<IEventHandler> eventHandlers) : base(options, eventHandlers)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; } = default!;
    }

    public class TestEntity
    {
        public int Id { get; set; }
        public string Value { get; set; } = default!;
    }
}