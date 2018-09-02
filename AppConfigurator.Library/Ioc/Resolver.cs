using Microsoft.Extensions.DependencyInjection;

namespace AppConfigurator.Library.Ioc
{
    public static class Resolver
    {
        public static T Resolve<T>() where T : class
        {
            return ServiceLocator.Instance.GetService<T>();
        }
    }
}
