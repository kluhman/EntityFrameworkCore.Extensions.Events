using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Tests
{
    public class TestDbContext : BaseDbContext
    {
        public TestDbContext(IEnumerable<IEventHandler> eventHandlers) : base(eventHandlers)
        {
        }

        public TestDbContext(DbContextOptions options, ICollection<IEventHandler> eventHandlers) : base(options, eventHandlers)
        {
        }
        
        public DbSet<TestEntity> TestEntities { get; set; }
    }
    
    public class TestEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}