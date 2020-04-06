using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events
{
    public abstract class DbContextWithEvents : DbContext
    {
        private readonly ICollection<IEventHandler> _eventHandlers;

        protected DbContextWithEvents(IEnumerable<IEventHandler> eventHandlers)
        {
            _eventHandlers = eventHandlers.ToList();
        }

        protected DbContextWithEvents(DbContextOptions options, IEnumerable<IEventHandler> eventHandlers) : base(options)
        {
            _eventHandlers = eventHandlers.ToList();
        }

        public override int SaveChanges()
        {
            return this.SaveChangesWithEvents(_eventHandlers, base.SaveChanges);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithEvents(acceptAllChangesOnSuccess, _eventHandlers, base.SaveChanges);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithEventsAsync(cancellationToken, _eventHandlers, base.SaveChangesAsync);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithEventsAsync(acceptAllChangesOnSuccess, cancellationToken, _eventHandlers, base.SaveChangesAsync);
        }
    }
}