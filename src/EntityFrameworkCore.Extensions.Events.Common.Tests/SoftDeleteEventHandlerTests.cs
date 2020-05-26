using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class SoftDeleteEventHandlerTests : IDisposable
    {
        private readonly SoftDeleteEventHandler _handler;
        private readonly TestDbContext _context;

        public SoftDeleteEventHandlerTests()
        {
            _handler = new SoftDeleteEventHandler();
            _context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void OnInserting_ShouldNotSetDeleteDate_IfEntityIsNotDeleted()
        {
            var entity = new Person { IsDeleted = false };

            _handler.OnInserting(_context, _context.Entry(entity));

            Assert.Null(entity.DeletedDate);
        }

        [Fact]
        public void OnInserting_ShouldSetDeleteDate_IfEntityIsDeleted()
        {
            var entity = new Person { IsDeleted = true };

            _handler.OnInserting(_context, _context.Entry(entity));

            Assert.NotNull(entity.DeletedDate);
        }

        [Fact]
        public void OnUpdate_ShouldSetDeleteDate_IfEntityWasNotDeletedAndNowIsDeleted()
        {
            var entity = new Person { IsDeleted = false };
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.IsDeleted = true;
            _handler.OnUpdating(_context, _context.Entry(entity));

            Assert.NotNull(entity.DeletedDate);
        }

        [Fact]
        public void OnUpdate_ShouldUnsetDeleteDate_IfEntityWasDeletedAndNowIsNotDeleted()
        {
            var entity = new Person { IsDeleted = true, DeletedDate = DateTime.UtcNow };
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.IsDeleted = false;
            _handler.OnUpdating(_context, _context.Entry(entity));

            Assert.Null(entity.DeletedDate);
        }

        [Fact]
        public void OnUpdate_ShouldLeaveDeleteDateUnchanged_WhenEntityWasAlreadyDeleted()
        {
            var entity = new Person { Name = "old", IsDeleted = true, DeletedDate = DateTime.MinValue };
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.Name = "new";
            _handler.OnUpdating(_context, _context.Entry(entity));

            Assert.Equal(DateTime.MinValue, entity.DeletedDate);
        }
    }
}