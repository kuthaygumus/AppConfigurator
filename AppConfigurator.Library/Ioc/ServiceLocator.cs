using System;

namespace AppConfigurator.Library.Ioc
{
    public class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}