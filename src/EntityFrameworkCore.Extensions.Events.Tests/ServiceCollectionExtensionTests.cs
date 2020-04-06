using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Tests
{
    public class ServiceCollectionExtensionTests
    {
        private readonly ServiceCollection _services;

        public ServiceCollectionExtensionTests()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddEventHandlers_ShouldAddEventHandlersInAssembly()
        {
            _services.AddEventHandlers(GetType().Assembly);

            Assert.Contains(_services, x => x.ImplementationType == typeof(TestEventHandler));
        }

        [Fact]
        public void AddEventHandlers_ShouldAddEventHandlersInAppDomain()
        {
            _services.AddEventHandlers();

            Assert.Contains(_services, x => x.ImplementationType == typeof(TestEventHandler));
        }

        private sealed class TestEventHandler : BaseEventHandler
        {
        }
    }
}