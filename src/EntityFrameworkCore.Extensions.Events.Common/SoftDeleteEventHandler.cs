using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public class SoftDeleteEventHandler : BaseEventHandler
    {
        public override void OnInserting(DbContext context, object entity)
        {
            if (entity is ISupportSoftDelete deletableEntity && deletableEntity.IsDeleted)
            {
                deletableEntity.DeletedDate = DateTime.UtcNow;
            }
        }

        public override void OnUpdating(DbContext context, object originalEntity, object currentEntity)
        {
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