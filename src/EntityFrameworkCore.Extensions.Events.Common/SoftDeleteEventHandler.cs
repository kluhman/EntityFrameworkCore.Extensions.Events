using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public class SoftDeleteEventHandler : BaseEventHandler
    {
        public override void OnInserting(DbContext context, EntityEntry entry)
        {
            if (entry.Entity is ISupportSoftDelete deletableEntity && deletableEntity.IsDeleted)
            {
                deletableEntity.DeletedDate = DateTime.UtcNow;
            }
        }

        public override void OnUpdating(DbContext context, EntityEntry entry)
        {
            var currentEntity = entry.Entity;
            var originalEntity = entry.OriginalValues.ToObject();

            if (!(originalEntity is ISupportSoftDelete originalDeletableEntity))
            {
                return;
            }

            if (!(currentEntity is ISupportSoftDelete currentDeletableEntity))
            {
                return;
            }

            if (originalDeletableEntity.IsDeleted == currentDeletableEntity.IsDeleted)
            {
                return;
            }

            currentDeletableEntity.DeletedDate = currentDeletableEntity.IsDeleted
                ? DateTime.UtcNow
                : default(DateTime?);
        }
    }
}