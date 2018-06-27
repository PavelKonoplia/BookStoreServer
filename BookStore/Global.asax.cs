using System.Web.Http;
using System.Web.Routing;

namespace BookStore
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // System.Data.Entity.Database.SetInitializer(new DatabaseInitializer());
            // System.Data.Entity.Database.SetInitializer(new IdentityDbInit());
        }
    }
}
