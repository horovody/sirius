using Autofac;
using Sirius.Infrastructure.Modules;
using Sirius.Logic.DataServices;

namespace Sirius.Logic
{
    public class SiriusLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // automatically register all services with special attributes
            builder.RegisterCustomAssemblyServices(this.ThisAssembly);
            builder.RegisterCustomAssemblyImplementations(this.ThisAssembly);

            // register generic CRUD data services
            builder.RegisterGeneric(typeof(DataServiceBase<,>))
                .As(typeof(ICrudDataService<,>))
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
