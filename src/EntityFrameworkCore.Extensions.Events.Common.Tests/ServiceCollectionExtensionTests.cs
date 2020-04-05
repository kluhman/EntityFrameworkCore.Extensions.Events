using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace EntityFrameworkCore.Extensions.Events.Common.Tests
{
    public class ServiceCollectionExtensionTests
    {
        private readonly ServiceCollection _services;

        public ServiceCollectionExtensionTests()
        {
            _services = new ServiceCollection();
        }

        [Fact]
        public void AddCommonEventHandlers_ShouldAddAuditEventHandler()
        {
            _services.AddCommonEventHandlers();
            
            Assert.Contains(_services, x => x.ImplementationType == typeof(AuditEventHandler));
        }
        
        [Fact]
        public void AddCommonEventHandlers_ShouldAddSoftDeleteEventHandler()
        {
            _services.AddCommonEventHandlers();
            
            Assert.Contains(_services, x => x.ImplementationType == typeof(AuditEventHandler));
        }
    }
}