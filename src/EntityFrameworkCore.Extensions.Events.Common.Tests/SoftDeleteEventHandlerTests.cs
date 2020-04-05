using System;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class SoftDeleteEventHandlerTests
    {
        private readonly SoftDeleteEventHandler _handler;

        public SoftDeleteEventHandlerTests()
        {
            _handler = new SoftDeleteEventHandler();
        }

        [Fact]
        public void OnInserting_ShouldNotSetDeleteDate_IfEntityIsNotDeleted()
        {
            var entity = new TestEntity { IsDeleted = false };
            
            _handler.OnInserting(null!, entity);
            
            Assert.Null(entity.DeletedDate);
        }
        
        [Fact]
        public void OnInserting_ShouldSetDeleteDate_IfEntityIsDeleted()
        {
            var entity = new TestEntity { IsDeleted = true };
            
            _handler.OnInserting(null!, entity);
            
            Assert.NotNull(entity.DeletedDate);
        }
        
        [Fact]
        public void OnUpdate_ShouldSetDeleteDate_IfEntityWasNotDeletedAndNowIsDeleted()
        {
            var originalEntity = new TestEntity { IsDeleted = false };
            var currentEntity = new TestEntity { IsDeleted = true };
            
            _handler.OnUpdating(null!, originalEntity, currentEntity);
            
            Assert.NotNull(currentEntity.DeletedDate);
        }
        
        [Fact]
        public void OnUpdate_ShouldUnsetDeleteDate_IfEntityWasDeletedAndNowIsNotDeleted()
        {
            var originalEntity = new TestEntity { IsDeleted = true };
            var currentEntity = new TestEntity { IsDeleted = false };
            
            _handler.OnUpdating(null!, originalEntity, currentEntity);
            
            Assert.Null(currentEntity.DeletedDate);
        }
        
        [Fact]
        public void OnUpdate_ShouldLeaveDeleteDateUnchanged_WhenEntityWasAlreadyDeleted()
        {
            var originalEntity = new TestEntity { IsDeleted = true, DeletedDate = DateTime.MinValue };
            var currentEntity = new TestEntity { IsDeleted = true, DeletedDate = DateTime.MinValue };
            
            _handler.OnUpdating(null!, originalEntity, currentEntity);
            
            Assert.Equal(DateTime.MinValue, currentEntity.DeletedDate);
        }
    }
}