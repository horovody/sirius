using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Sirius.Shared.Attributes;

namespace Sirius.Infrastructure.Modules
{
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// Register all classes marked with attribute <see cref="ServiceClassAttribute"/> 
        /// from specified assembly as implementation of all his public interfaces
        /// </summary>
        /// <param name="builder">
        ///     Container builder. </param>
        /// <param name="assembly">
        /// The assembly   </param>
        public static void RegisterCustomAssemblyServices(this ContainerBuilder builder, Assembly assembly)
        {
            var serviceImplementTypes =
                from type in assembly.GetTypes()
                where type.IsClass
                      && !type.IsAbstract
                      && !type.IsGenericType
                let serviceAttr = type.GetCustomAttribute<ServiceClassAttribute>()
                where serviceAttr != null
                from iface in type.GetInterfaces()
                where iface.IsPublic
                group iface by type into grouping
                select grouping;

            foreach (var group in serviceImplementTypes)
            {
                builder.RegisterType(group.Key)
                    .As(group.ToArray())
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Register all classes marked with attribute <see cref="ServiceClassAttribute"/> 
        /// from specified assembly as implementation of an interface denoted in property <see cref="Type"/>
        /// </summary>
        /// <param name="builder">
        ///     Container builder. </param>
        /// <param name="assembly">
        /// The assembly   </param>
        public static void RegisterCustomAssemblyImplementations(this ContainerBuilder builder, Assembly assembly)
        {
            var implementTypes =
                from type in assembly.GetTypes()
                where type.IsClass
                      && !type.IsAbstract
                      && !type.IsGenericType
                let implement = type.GetCustomAttribute<ImplementClassAttribute>()
                where implement != null
                group implement.Type by type into grouping
                select grouping;

            foreach (var group in implementTypes)
            {
                builder.RegisterType(group.Key)
                    .As(group.ToArray())
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Register all AutoMapper properties in assembly
        /// </summary>
        /// <param name="builder">
        ///     Container builder. </param>
        /// <param name="assembly">
        /// The assembly   </param>
        public static void RegisterAutoMapperProfiles(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(type =>
                    type.IsClass && !type.IsAbstract && !type.IsGenericType &&
                    typeof(AutoMapper.Profile).IsAssignableFrom(type))
                .As<AutoMapper.Profile>();
        }
    }
}
