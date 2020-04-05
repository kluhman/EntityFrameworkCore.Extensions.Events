using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class TestDbContext : BaseDbContext
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
        
        public DbSet<TestEntity> TestEntities { get; set; } = default!;
    }
}