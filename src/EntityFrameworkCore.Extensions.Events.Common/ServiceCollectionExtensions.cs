using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCore.Extensions.Events.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonEventHandlers(this IServiceCollection services)
        {
            return services.AddEventHandlers(Assembly.GetExecutingAssembly());
        }
    }
}