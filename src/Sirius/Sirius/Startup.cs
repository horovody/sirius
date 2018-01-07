using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sirius.Data.Access;
using Sirius.Data.Access.Auth;
using Sirius.DictionaryApi;
using Sirius.Logic;
using Sirius.Modules;
using Sirius.Shared.Auth;
using Sirius.Shared.Constants;

namespace Sirius
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserContext>(provider =>
                new ClaimsUserContext(provider.GetService<IHttpContextAccessor>().HttpContext.User));
            this.AddIdentity(services);

            services.AddMvc();

            var builder = new ContainerBuilder();
            builder.RegisterModule<SiriusDataAccessModule>();
            builder.RegisterModule<SiriusLogicModule>();
            builder.RegisterModule<SiriusDictApiModule>();
            builder.RegisterModule<SiriusWebModule>();

            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "AngularRoute",
                  template: "{*path}",
                  defaults: new { controller = "Home", action = "Index" },
                  constraints: new { path = new NotApiRouteConstraint() }
                );
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<SiriusDbContext>().Database.Migrate();
            }

            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

        private void AddIdentity(IServiceCollection services)
        {
            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<SiriusDbContext>()
                .AddDefaultTokenProviders();
            // Identity options.
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                // Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
            });
            // Role based Authorization: policy based role checks.
            services.AddAuthorization(options =>
            {
                // Policy for dashboard: only administrator role.
                options.AddPolicy("Manage", policy => policy.RequireRole(AuthRoles.Admin));
                // Policy for resources: user or administrator roles. 
                options.AddPolicy("Access", policy => policy.RequireRole(AuthRoles.Admin, AuthRoles.User));
            });
            // Adds IdentityServer.
            services.AddIdentityServer()
                // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
                // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
                // See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
                //TODO: make some seriuos key for production
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                // TODO: configure IdentityServer to use EntityFramework (EF) as the storage mechanism for configuration data
                // see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
                .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                .AddInMemoryClients(IdentityConfig.GetClients())
                .AddAspNetIdentity<UserEntity>();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:59136";//TODO: move to config
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "WebAPI";
                });
        }
    }
}
