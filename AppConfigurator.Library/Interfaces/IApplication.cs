using System.Collections.Generic;

namespace AppConfigurator.Library.Interfaces
{
    public interface IApplication
    {
        bool IsConnected();
        IList<IApplicationModel> GetAll(string applicationName);
    }
}