using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public class AuditEventHandler : BaseEventHandler
    {
        public override void OnInserting(DbContext context, EntityEntry entry)
        {
            var moment = DateTime.UtcNow;
            SetCreateDate(context, entry, moment);
            SetUpdateDate(context, entry, moment);
        }

        public override void OnUpdating(DbContext context, EntityEntry entry)
        {
            SetUpdateDate(context, entry, DateTime.UtcNow);
        }

        private static void SetCreateDate(DbContext context, EntityEntry entry, DateTime moment)
        {
            if (entry.Entity is ITrackCreateDate createdEntity)
            {
                createdEntity.CreateDate = moment;
            }
            else if (entry.Metadata.IsOwned() && GetOwner(context, entry)?.Entity is ITrackUpdateDate updatedOwner) 
            {
                // since owner can be created independently of owned type, we track it as an update rather than a create
                updatedOwner.UpdateDate = moment;
            }
        }

        private static void SetUpdateDate(DbContext context, EntityEntry entry, DateTime moment)
        {
            if (entry.Entity is ITrackUpdateDate updatedEntity)
            {
                updatedEntity.UpdateDate = moment;
            }
            else if (entry.Metadata.IsOwned() && GetOwner(context, entry)?.Entity is ITrackUpdateDate updatedOwner)
            {
                updatedOwner.UpdateDate = moment;
            }
        }

        private static EntityEntry? GetOwner(DbContext context, EntityEntry entry)
        {
            return context
                .ChangeTracker
                .Entries()
                .SingleOrDefault(x => x.References.Any(r => r.Metadata.ForeignKey.IsOwnership && r.TargetEntry.Entity == entry.Entity));
        }
    }
}