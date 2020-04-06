using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class SoftDeleteExtensionTests
    {
        private readonly TestDbContext _context;

        public SoftDeleteExtensionTests()
        {
            _context =  new TestDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
        }
        
        [Fact]
        public void IsNotDeleted_ShouldFilterOutDeletedEntities()
        {
            var undeletedEntity = new TestEntity();
            var deletedEntity = new TestEntity().SoftDelete();
            
            _context.TestEntities.AddRange(deletedEntity, undeletedEntity);
            _context.SaveChanges();
            
            Assert.Equal(undeletedEntity, _context.TestEntities.IsNotDeleted().Single());
        }
        
        [Fact]
        public void IsDeleted_ShouldFilterOutDeletedEntities()
        {
            var undeletedEntity = new TestEntity();
            var deletedEntity = new TestEntity().SoftDelete();
            
            _context.TestEntities.AddRange(deletedEntity, undeletedEntity);
            _context.SaveChanges();
            
            Assert.Equal(deletedEntity, _context.TestEntities.IsDeleted().Single());
        }
        
        [Fact]
        public void SoftDelete_OnEntity_ShouldMarkEntityDeleted()
        {
            var entity = new TestEntity();
            entity.SoftDelete();
            
            Assert.True(entity.IsDeleted);
        }
        
        [Fact]
        public void SoftDelete_OnDbSet_ShouldMarkEntityDeleted()
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);
            _context.SaveChanges();

            _context.TestEntities.SoftDelete(entity.Id);
            
            Assert.True(entity.IsDeleted);
        }
        
        [Fact]
        public void SoftDelete_OnDbContext_ShouldMarkEntityDeleted()
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);
            _context.SaveChanges();

            _context.SoftDelete<TestEntity>(entity.Id);
            
            Assert.True(entity.IsDeleted);
        }
    }
}