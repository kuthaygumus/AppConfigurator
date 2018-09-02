using System.Collections.Generic;
using System.Threading.Tasks;
using AppConfigurator.Library.Interfaces;

namespace AppConfigurator.Service.Redis
{
    public interface IRedisManager
    {
        Task<IList<IApplicationModel>> GetAllAsync();
        Task<int> AddOrUpdateAsync(IList<IApplicationModel> models);
        Task<bool> AddOrUpdateAsync(IApplicationModel model);
        Task<IApplicationModel> GetItemAsync(string applicationName, string configurationName);
        Task<bool> DeleteAsync(string key);
    }
}