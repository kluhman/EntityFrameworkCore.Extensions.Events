using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events
{
    public interface IEventHandler
    {
        void OnInserting(DbContext context, EntityEntry entry);
        void OnInserted(DbContext context, EntityEntry entry);
        void OnUpdating(DbContext context, EntityEntry entry);
        void OnUpdated(DbContext context, EntityEntry entry);
        void OnDeleting(DbContext context, EntityEntry entry);
        void OnDeleted(DbContext context, EntityEntry entry);
        void BeforeUnchanged(DbContext context, EntityEntry entry);
        void AfterUnchanged(DbContext context, EntityEntry entry);
    }
}