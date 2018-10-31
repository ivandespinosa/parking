using Microsoft.Practices.Unity.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace WebApiManagerParking
{
    [ExcludeFromCodeCoverage]
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.LoadConfiguration();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}