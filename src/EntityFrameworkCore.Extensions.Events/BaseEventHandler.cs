using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseEventHandler : IEventHandler
    {
        public virtual void OnInserting(DbContext context, EntityEntry entry)
        {
        }

        public virtual void OnInserted(DbContext context, EntityEntry entry)
        {
        }

        public virtual void OnUpdating(DbContext context, EntityEntry entry)
        {
        }

        public virtual void OnUpdated(DbContext context, EntityEntry entry)
        {
        }

        public virtual void OnDeleting(DbContext context, EntityEntry entry)
        {
        }

        public virtual void OnDeleted(DbContext context, EntityEntry entry)
        {
        }

        public virtual void BeforeUnchanged(DbContext context, EntityEntry entry)
        {
        }

        public virtual void AfterUnchanged(DbContext context, EntityEntry entry)
        {
        }
    }
}