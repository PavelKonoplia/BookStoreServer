using System;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BookStore.BLL;
using BookStore.Common.Interfaces;
using BookStore.DAL;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(BookStore.Startup))]
namespace BookStore
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // autofac configuration zone

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            // Register your Web API controllers.

            // app.CreatePerOwinContext<AppRoleManager>(AppRoleManager.Create);

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            builder.RegisterType<IdentityDatabaseContext>().As(typeof(DbContext)).SingleInstance();
            builder.RegisterType<CustomRoleStore>().As(typeof(IRoleStore<CustomRole, long>)).InstancePerDependency();
            builder.RegisterType<CustomUserStore>().As(typeof(IUserStore<User, long>)).InstancePerDependency();
            builder.RegisterType<IdentityUserManager>().InstancePerDependency();
            builder.RegisterType<IdentityRoleManager>().InstancePerDependency();
            builder.RegisterType<IdentityFactoryOptions<IdentityUserManager>>().InstancePerDependency();
          //  builder.RegisterType<IdentityFactoryOptions<IdentityRoleManager>>().InstancePerDependency();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            CreateRoles(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            
            // end of autofac configure 


            
            ConfigureAuth(app, container.Resolve<IdentityUserManager>());

            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }


        public void ConfigureAuth(IAppBuilder app, IdentityUserManager userManager)
        {           
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ApplicationOAuthProvider(userManager),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            
            app.UseOAuthBearerTokens(OAuthOptions);
        }

        public void CreateRoles(IContainer container)
        {
            IdentityRoleManager roleManager = container.Resolve<IdentityRoleManager>();

            IdentityUserManager userManager = container.Resolve<IdentityUserManager>();

            userManager.AddToRoleAsync(6, "Admin");

            if (roleManager.FindByNameAsync("Admin").Result == null)
            {
                roleManager.CreateAsync(new CustomRole("Admin"));
                userManager.AddToRoleAsync(1,"Admin");
            }
            if (roleManager.FindByNameAsync("Seller").Result == null)
            {
                roleManager.CreateAsync(new CustomRole("Seller"));
            }
            if (roleManager.FindByNameAsync("Customer").Result == null)
            {
                roleManager.CreateAsync(new CustomRole("Customer"));
            }
        }
    }
}