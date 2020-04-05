using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events
{
    public abstract class BaseEventHandler : IEventHandler
    {
        public virtual void OnInserting(DbContext context, object entity)
        {
        }

        public virtual void OnInserted(DbContext context, object entity)
        {
        }

        public virtual void OnUpdating(DbContext context, object entity)
        {
        }

        public virtual void OnUpdated(DbContext context, object entity)
        {
        }
        
        public virtual void OnDeleting(DbContext context, object entity)
        {
        }

        public virtual void OnDeleted(DbContext context, object entity)
        {
        }
    }
}