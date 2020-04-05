﻿using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Events
{
    public interface IEventHandler
    {
        void OnInserting(DbContext context, object entity);
        void OnInserted(DbContext context, object entity);
        void OnUpdating(DbContext context, object originalEntity, object currentEntity);
        void OnUpdated(DbContext context, object originalEntity, object currentEntity);
        void OnDeleting(DbContext context, object entity);
        void OnDeleted(DbContext context, object entity);
    }
}