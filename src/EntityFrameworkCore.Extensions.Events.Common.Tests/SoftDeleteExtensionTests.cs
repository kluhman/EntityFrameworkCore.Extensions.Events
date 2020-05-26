using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class SoftDeleteExtensionTests : IDisposable
    {
        private readonly TestDbContext _context;

        public SoftDeleteExtensionTests()
        {
            _context = new TestDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void IsNotDeleted_ShouldFilterOutDeletedEntities()
        {
            var undeletedEntity = new Person();
            var deletedEntity = new Person().SoftDelete();

            _context.People.AddRange(deletedEntity, undeletedEntity);
            _context.SaveChanges();

            Assert.Equal(undeletedEntity, _context.People.IsNotDeleted().Single());
        }

        [Fact]
        public void IsDeleted_ShouldFilterOutDeletedEntities()
        {
            var undeletedEntity = new Person();
            var deletedEntity = new Person().SoftDelete();

            _context.People.AddRange(deletedEntity, undeletedEntity);
            _context.SaveChanges();

            Assert.Equal(deletedEntity, _context.People.IsDeleted().Single());
        }

        [Fact]
        public void SoftDelete_OnEntity_ShouldMarkEntityDeleted()
        {
            var entity = new Person();
            entity.SoftDelete();

            Assert.True(entity.IsDeleted);
        }

        [Fact]
        public void SoftDelete_OnDbSet_ShouldMarkEntityDeleted()
        {
            var entity = new Person();
            _context.People.Add(entity);
            _context.SaveChanges();

            _context.People.SoftDelete(entity.Id);

            Assert.True(entity.IsDeleted);
        }

        [Fact]
        public void SoftDelete_OnDbContext_ShouldMarkEntityDeleted()
        {
            var entity = new Person();
            _context.People.Add(entity);
            _context.SaveChanges();

            _context.SoftDelete<Person>(entity.Id);

            Assert.True(entity.IsDeleted);
        }
    }
}