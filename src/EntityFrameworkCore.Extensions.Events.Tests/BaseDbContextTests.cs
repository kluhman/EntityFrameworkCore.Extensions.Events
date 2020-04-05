using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Moq;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Tests
{
    public class BaseDbContextTests
    {
        private readonly Mock<IEventHandler> _eventHandler;
        private readonly TestDbContext _context;

        public BaseDbContextTests()
        {
            _eventHandler = new Mock<IEventHandler>();
            _context = new TestDbContext(new DbContextOptionsBuilder()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options, new [] { _eventHandler.Object });
        }

        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnInsertingHandlers_WhenEntityIsInserted(string saveChanges)
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);

            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnInserting(_context, entity));
        }
        
        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnInsertedHandlers_WhenEntityIsInserted(string saveChanges)
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);

            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnInserted(_context, entity));
        }
        
        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnUpdatingHandlers_WhenEntityIsUpdated(string saveChanges)
        {
            var entity = new TestEntity { Value = "old" };
            _context.TestEntities.Add(entity);
            await ExecuteSaveChanges(saveChanges);

            entity.Value = "new";
            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnUpdating(_context, It.Is<TestEntity>(e => e.Value == "old"), entity));
        }
        
        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnUpdatedHandlers_WhenEntityIsUpdated(string saveChanges)
        {
            var entity = new TestEntity { Value = "old" };
            _context.TestEntities.Add(entity);
            await ExecuteSaveChanges(saveChanges);

            entity.Value = "new";
            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnUpdated(_context, entity));
        }
        
        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnDeletingHandlers_WhenEntityIsDeleted(string saveChanges)
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);
            await ExecuteSaveChanges(saveChanges);

            _context.TestEntities.Remove(entity);
            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnDeleting(_context, entity));
        }
        
        [Theory]
        [InlineData(nameof(SaveChanges))]
        [InlineData(nameof(SaveChangesAsync))]
        [InlineData(nameof(SaveChangesWithOptions))]
        [InlineData(nameof(SaveChangesWithOptionsAsync))]
        public async Task SaveChanges_ShouldExecuteOnDeletedHandlers_WhenEntityIsDeleted(string saveChanges)
        {
            var entity = new TestEntity();
            _context.TestEntities.Add(entity);
            await ExecuteSaveChanges(saveChanges);

            _context.TestEntities.Remove(entity);
            await ExecuteSaveChanges(saveChanges);
            
            _eventHandler.Verify(x => x.OnDeleted(_context, entity));
        }

        private Task ExecuteSaveChanges(string methodName)
        {
            var method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new ArgumentException($"'{methodName}' could not be found on class");
            }
            
            return method.Invoke(this, new object?[0]) as Task;
        }

        private Task SaveChanges()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        
        private Task SaveChangesWithOptions()
        {
            _context.SaveChanges(true);
            return Task.CompletedTask;
        }
        
        private Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync(CancellationToken.None);
        }
        
        private Task SaveChangesWithOptionsAsync()
        {
            return _context.SaveChangesAsync(true, CancellationToken.None);
        }
    }
}