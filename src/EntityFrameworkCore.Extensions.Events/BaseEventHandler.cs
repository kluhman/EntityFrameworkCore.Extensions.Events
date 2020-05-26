using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEventHandler : IEventHandler
    {
        [Obsolete("This method will be removed in a future release. Switch to OnInserting(DbContext, EntityEntry) instead")]
        public virtual void OnInserting(DbContext context, object entity)
        {
        }

        public virtual void OnInserting(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnInserting(context, entry.Entity);
#pragma warning restore 618
        }

        [Obsolete("This method will be removed in a future release. Switch to OnInserted(DbContext, EntityEntry) instead")]
        public virtual void OnInserted(DbContext context, object entity)
        {
        }

        public virtual void OnInserted(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnInserted(context, entry.Entity);
#pragma warning restore 618
        }

        [Obsolete("This method will be removed in a future release. Switch to OnUpdating(DbContext, EntityEntry) instead")]
        public virtual void OnUpdating(DbContext context, object originalEntity, object currentEntity)
        {
        }

        public virtual void OnUpdating(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnUpdating(context, entry.OriginalValues.ToObject(), entry.Entity);
#pragma warning restore 618
        }

        [Obsolete("This method will be removed in a future release. Switch to OnUpdated(DbContext, EntityEntry) instead")]
        public virtual void OnUpdated(DbContext context, object entity)
        {
        }

        public virtual void OnUpdated(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnUpdated(context, entry.Entity);
#pragma warning restore 618
        }

        [Obsolete("This method will be removed in a future release. Switch to OnDeleting(DbContext, EntityEntry) instead")]
        public virtual void OnDeleting(DbContext context, object entity)
        {
        }

        public virtual void OnDeleting(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnDeleting(context, entry.Entity);
#pragma warning restore 618
        }

        [Obsolete("This method will be removed in a future release. Switch to OnDeleted(DbContext, EntityEntry) instead")]
        public virtual void OnDeleted(DbContext context, object entity)
        {
        }

        public virtual void OnDeleted(DbContext context, EntityEntry entry)
        {
#pragma warning disable 618
            OnDeleted(context, entry.Entity);
#pragma warning restore 618
        }
    }
}