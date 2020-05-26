using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class AuditEventHandlerTests : IDisposable
    {
        private readonly AuditEventHandler _handler;
        private readonly TestDbContext _context;

        public AuditEventHandlerTests()
        {
            _handler = new AuditEventHandler();
            _context = new TestDbContext(new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void OnInserting_ShouldSetCreateDate()
        {
            var entity = new Person();

            _handler.OnInserting(_context, _context.Entry(entity));

            Assert.NotEqual(default, entity.CreateDate);
        }

        [Fact]
        public void OnInserting_ShouldSetUpdateDate()
        {
            var entity = new Person();

            _handler.OnInserting(_context, _context.Entry(entity));

            Assert.NotEqual(default, entity.UpdateDate);
        }

        [Fact]
        public void OnInserting_ShouldNotSetCreateDateOnOwner_WhenOwnedTypeIsAddedAndOwnedTypeTracksCreateDate()
        {
            var entity = new Person { PhoneNumber = { Value = "555-555-5555" } };
            _context.People.Add(entity);

            _handler.OnInserting(_context, _context.Entry(entity.PhoneNumber));

            Assert.Equal(default, entity.CreateDate);
        }

        [Fact]
        public void OnInserting_ShouldNotSetUpdateDateOnOwner_WhenOwnedTypeIsAddedAndOwnedTypeTracksCreateDate()
        {
            var entity = new Person { PhoneNumber = { Value = "555-555-5555" } };
            _context.People.Add(entity);

            _handler.OnInserting(_context, _context.Entry(entity.PhoneNumber));

            Assert.Equal(default, entity.UpdateDate);
        }

        [Fact]
        public void OnInserting_ShouldNotSetCreateDateOnOwner_WhenOwnedTypeIsAddedAndOwnedTypeDoesNotTrackCreateDate()
        {
            var entity = new Person { Address = { Line1 = "Apt 1" } };
            _context.People.Add(entity);

            _handler.OnInserting(_context, _context.Entry(entity.Address));

            Assert.Equal(default, entity.CreateDate);
        }

        [Fact]
        public void OnInserting_ShouldSetUpdateDateOnOwner_WhenOwnedTypeIsAddedAndOwnedTypeDoesNotTrackCreateDate()
        {
            var entity = new Person { Address = { Line1 = "Apt 1" } };
            _context.People.Add(entity);

            _handler.OnInserting(_context, _context.Entry(entity.Address));

            Assert.NotEqual(default, entity.UpdateDate);
        }

        [Fact]
        public void OnUpdating_ShouldSetUpdateDate()
        {
            var entity = new Person();

            _handler.OnUpdating(_context, _context.Entry(entity));

            Assert.NotEqual(default, entity.UpdateDate);
        }

        [Fact]
        public void OnUpdating_ShouldNotSetUpdateDateOnOwner_WhenOwnedTypeIsChangedAndOwnedTypeTracksUpdateDate()
        {
            var entity = new Person();
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.PhoneNumber.Value = "555-555-5555";
            _handler.OnUpdating(_context, _context.Entry(entity.PhoneNumber));

            Assert.Equal(default, entity.UpdateDate);
        }

        [Fact]
        public void OnUpdating_ShouldSetUpdateDateOnOwner_WhenOwnedTypeIsChangedAndOwnedTypeDoesNotTrackUpdateDate()
        {
            var entity = new Person();
            _context.People.Add(entity);
            _context.SaveChanges();

            entity.Address.Line2 = "Apt 1";
            _handler.OnUpdating(_context, _context.Entry(entity.Address));

            Assert.NotEqual(default, entity.UpdateDate);
        }
    }
}