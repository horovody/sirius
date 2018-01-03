using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sirius.Shared;

namespace Sirius.Data.Access
{
    public class SiriusDataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx =>
                {
                    var dbContext = new SiriusDbContext(ctx.Resolve<IConfiguration>());
                    return dbContext;
                })
                .As<IUnitOfWork>()
                .As<DbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Store<>))
                .As(typeof(IStore<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<Repository>()
                .As<IRepository>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
