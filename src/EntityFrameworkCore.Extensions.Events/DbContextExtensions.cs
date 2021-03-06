﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events
{
    public static class DbContextExtensions
    {
        public static int SaveChangesWithEvents(this DbContext context, ICollection<IEventHandler> eventHandlers, Func<int> saveChanges)
        {
            var entries = context.HandlePreSaveEvents(eventHandlers);
            var result = saveChanges();
            context.HandlePostSaveEvents(eventHandlers, entries);

            return result;
        }

        public static int SaveChangesWithEvents(this DbContext context, bool acceptAllChangesOnSuccess, ICollection<IEventHandler> eventHandlers, Func<bool, int> saveChanges)
        {
            var entries = context.HandlePreSaveEvents(eventHandlers);
            var result = saveChanges(acceptAllChangesOnSuccess);
            context.HandlePostSaveEvents(eventHandlers, entries);

            return result;
        }

        public static async Task<int> SaveChangesWithEventsAsync(this DbContext context, CancellationToken cancellationToken, ICollection<IEventHandler> eventHandlers, Func<CancellationToken, Task<int>> saveChanges)
        {
            var entries = context.HandlePreSaveEvents(eventHandlers);
            var result = await saveChanges(cancellationToken);
            context.HandlePostSaveEvents(eventHandlers, entries);

            return result;
        }

        public static async Task<int> SaveChangesWithEventsAsync(this DbContext context, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken, ICollection<IEventHandler> eventHandlers, Func<bool, CancellationToken, Task<int>> saveChanges)
        {
            var entries = context.HandlePreSaveEvents(eventHandlers);
            var result = await saveChanges(acceptAllChangesOnSuccess, cancellationToken);
            context.HandlePostSaveEvents(eventHandlers, entries);

            return result;
        }

        private static List<KeyValuePair<EntityState, EntityEntry>> HandlePreSaveEvents(this DbContext context, ICollection<IEventHandler> eventHandlers)
        {
            var entries = new List<KeyValuePair<EntityState, EntityEntry>>();
            foreach (var entry in context.ChangeTracker.Entries().ToList())
            {
                entries.Add(new KeyValuePair<EntityState, EntityEntry>(entry.State, entry));
                foreach (var eventHandler in eventHandlers)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            eventHandler.OnInserting(context, entry);
                            break;
                        case EntityState.Modified:
                            eventHandler.OnUpdating(context, entry);
                            break;
                        case EntityState.Deleted:
                            eventHandler.OnDeleting(context, entry);
                            break;
                        case EntityState.Unchanged:
                            eventHandler.BeforeUnchanged(context, entry);
                            break;
                    }
                }
            }

            return entries;
        }

        private static void HandlePostSaveEvents(this DbContext context, ICollection<IEventHandler> eventHandlers, IEnumerable<KeyValuePair<EntityState, EntityEntry>> entries)
        {
            foreach (var (state, entry) in entries)
            {
                foreach (var eventHandler in eventHandlers)
                {
                    switch (state)
                    {
                        case EntityState.Added:
                            eventHandler.OnInserted(context, entry);
                            break;
                        case EntityState.Modified:
                            eventHandler.OnUpdated(context, entry);
                            break;
                        case EntityState.Deleted:
                            eventHandler.OnDeleted(context, entry);
                            break;
                        case EntityState.Unchanged:
                            eventHandler.AfterUnchanged(context, entry);
                            break;
                    }
                }
            }
        }
    }
}