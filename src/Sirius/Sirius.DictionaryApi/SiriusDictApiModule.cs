using Autofac;

namespace Sirius.DictionaryApi
{
    public class SiriusDictApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DictApiClient>()
                .As<IDictApiClient>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
