# EntityFrameworkCore.Extensions.Events

[![Build Status](https://dev.azure.com/kluhman/EntityFrameworkCore.Extensions.Events/_apis/build/status/EntityFrameworkCore.Extensions.Events?branchName=master)](https://dev.azure.com/kluhman/EntityFrameworkCore.Extensions.Events/_build/latest?definitionId=4&branchName=master)
[![Nuget](https://img.shields.io/nuget/v/EntityFrameworkCore.Extensions.Events)](https://www.nuget.org/packages/EntityFrameworkCore.Extensions.Events/)

## Overview

This package provides a set of interfaces and supporting logic to add event handling into the Entity Framework Core Lifecycle. 

Additionally, an additional package is provided which implements a common set of event handlers for supporting create/update date tracking and soft deletion of entities.

## Why use this package?

Entity Framework Core provides a lot of useful functionality for change tracking and managing state in your database, but doesn't currently provide any built in way to plug into that tracking lifecycle. For common, simple tasks, this lack of native support can mean your only alternative is to retreat to use of triggers in your database, making it difficult to understand how changes are made to your models and separating business logic from your code.

This package aims to solve these problems by providing simple interfaces designed to add event handling into your database state management lifecycle in a way that is easy to implement and unit test.

## Installation

The package can be installed using the dotnet CLI or via the Package Manager. 

**.NET CLI**
```bash
dotnet add package EntityFrameworkCore.Extensions.Events

dotnet add package EntityFrameworkCore.Extensions.Events.Common
```

**Package Manager**

```Powershell
Install-Package EntityFrameworkCore.Extensions.Events

Install-Package EntityFrameworkCore.Extensions.Events.Common
```

## Usage

To create a handler, create a class inheriting from `BaseEventHandler`. All the supported events are virtual, so you only need to override the events you care about.

```c#
public class MyHandler : BaseEventHandler
{
    public override void OnInserting(DbContext context, object entity)
    {
        // your code here
    }

    public override void OnInserted(DbContext context, object entity)
    {
        // your code here
    }

    public override void OnUpdating(DbContext context, object originalEntity, object currentEntity)
    {
        // your code here
    }

    public override void OnUpdated(DbContext context, object entity)
    {
        // your code here
    }

    public override void OnDeleting(DbContext context, object entity)
    {
        // your code here
    }

    public override void OnDeleted(DbContext context, object entity)
    {
        // your code here
    }
}
```

Next, your database context should inherit from `DbContextWithEvents` in order to fire the lifecycle events. If you are using another package such as Identity Framework that prevents you from using this base class, you can override the methods in your context to allow the events to be fired.

```c#

public class MyDbContext : DbContextWithEvents
{
    public MyDbContext(IEnumerable<IEventHandler> eventHandlers) : base(eventHandlers)
    {
    }

    public MyDbContext(DbContextOptions options, ICollection<IEventHandler> eventHandlers) : base(options, eventHandlers)
    {
    }
}

// OR

public class MyDbContext : IdentityDbContext
{
    private readonly ICollection<IEventHandler> _eventHandlers;

    public MyDbContext(IEnumerable<IEventHandler> eventHandlers) : base(eventHandlers)
    {
        _eventHandlers = eventHanders.ToList();
    }

    public MyDbContext(DbContextOptions options, ICollection<IEventHandler> eventHandlers) : base(options, eventHandlers)
    {
        _eventHandlers = eventHanders.ToList();
    }

    public override int SaveChanges()
        {
            return this.SaveChangesWithEvents(_eventHandlers, base.SaveChanges);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.SaveChangesWithEvents(acceptAllChangesOnSuccess, _eventHandlers, base.SaveChanges);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithEventsAsync(cancellationToken, _eventHandlers, base.SaveChangesAsync);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return this.SaveChangesWithEventsAsync(acceptAllChangesOnSuccess, cancellationToken, _eventHandlers, base.SaveChangesAsync);
        }
}
```

Lastly, you need to register your event handlers with the DI system. There is an extension method provided for this for Microsoft's DI package.

```c#
// Startup.cs
services.AddEventHandlers();
```