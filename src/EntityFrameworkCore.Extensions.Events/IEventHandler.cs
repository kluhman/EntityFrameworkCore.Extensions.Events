using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events
{
    public interface IEventHandler
    {
        [Obsolete("This method will be removed in a future release. Switch to OnInserting(DbContext, EntityEntry) instead")]
        void OnInserting(DbContext context, object entity);

        void OnInserting(DbContext context, EntityEntry entry);

        [Obsolete("This method will be removed in a future release. Switch to OnInserted(DbContext, EntityEntry) instead")]
        void OnInserted(DbContext context, object entity);

        void OnInserted(DbContext context, EntityEntry entry);

        [Obsolete("This method will be removed in a future release. Switch to OnUpdating(DbContext, EntityEntry) instead")]
        void OnUpdating(DbContext context, object originalEntity, object currentEntity);

        void OnUpdating(DbContext context, EntityEntry entry);

        [Obsolete("This method will be removed in a future release. Switch to OnUpdated(DbContext, EntityEntry) instead")]
        void OnUpdated(DbContext context, object entity);

        void OnUpdated(DbContext context, EntityEntry entry);

        [Obsolete("This method will be removed in a future release. Switch to OnDeleting(DbContext, EntityEntry) instead")]
        void OnDeleting(DbContext context, object entity);

        void OnDeleting(DbContext context, EntityEntry entry);

        [Obsolete("This method will be removed in a future release. Switch to OnDeleted(DbContext, EntityEntry) instead")]
        void OnDeleted(DbContext context, object entity);

        void OnDeleted(DbContext context, EntityEntry entry);
    }
}