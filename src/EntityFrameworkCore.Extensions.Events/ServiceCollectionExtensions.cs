using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameworkCore.Extensions.Events
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                services.AddEventHandlers(assembly);
            }

            return services;
        }

        public static IServiceCollection AddEventHandlers(this IServiceCollection services, Assembly assembly)
        {
            var eventHandlersTypes = assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && typeof(IEventHandler).IsAssignableFrom(x));

            foreach (var eventHandlerType in eventHandlersTypes)
            {
                services.AddTransient(typeof(IEventHandler), eventHandlerType);
            }

            return services;
        }
    }
}