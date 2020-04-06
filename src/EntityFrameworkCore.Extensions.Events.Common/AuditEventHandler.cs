using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public class AuditEventHandler : BaseEventHandler
    {
        public override void OnInserting(DbContext context, object entity)
        {
            var moment = DateTime.UtcNow;
            if (entity is ITrackCreateDate createdEntity)
            {
                createdEntity.CreateDate = moment;
            }

            if (entity is ITrackUpdateDate updatedEntity)
            {
                updatedEntity.UpdateDate = moment;
            }
        }

        public override void OnUpdating(DbContext context, object originalEntity, object currentEntity)
        {
            if (currentEntity is ITrackUpdateDate updatedEntity)
            {
                updatedEntity.UpdateDate = DateTime.UtcNow;
            }
        }
    }
}