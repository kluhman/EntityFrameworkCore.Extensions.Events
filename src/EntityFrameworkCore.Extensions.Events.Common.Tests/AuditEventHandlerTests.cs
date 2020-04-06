using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class AuditEventHandlerTests
    {
        private readonly AuditEventHandler _handler;

        public AuditEventHandlerTests()
        {
            _handler = new AuditEventHandler();
        }

        [Fact]
        public void OnInserting_ShouldSetCreateDate()
        {
            var entity = new TestEntity();

            _handler.OnInserting(null!, entity);

            Assert.NotEqual(default, entity.CreateDate);
        }

        [Fact]
        public void OnInserting_ShouldSetUpdateDate()
        {
            var entity = new TestEntity();

            _handler.OnInserting(null!, entity);

            Assert.NotEqual(default, entity.UpdateDate);
        }

        [Fact]
        public void OnUpdating_ShouldSetUpdateDate()
        {
            var entity = new TestEntity();

            _handler.OnUpdating(null!, null!, entity);

            Assert.NotEqual(default, entity.UpdateDate);
        }
    }
}