using System.Threading.Tasks;
using AppConfigurator.Library.Application;
using AppConfigurator.Service.Redis;
using Microsoft.AspNetCore.Mvc;

namespace AppConfigurator.Controllers
{
    public class ConfigurationApiController : ControllerBase
    {
        private readonly IRedisManager _redisManager;
        public ConfigurationApiController(IRedisManager redisManager)
        {
            _redisManager = redisManager;
        }

        [Route("api/config")]
        public async Task<IActionResult> GetAllConfigurations()
        {
            return Ok(await _redisManager.GetAllAsync());
        }

        [Route("api/config"), HttpPost]
        public async Task<IActionResult> UpdateConfig([FromBody]ApplicationModel config)
        {
            return Ok(await _redisManager.AddOrUpdateAsync(config));
        }

        [Route("api/config/{key}"), HttpDelete]
        public async Task<IActionResult> UpdateConfig(string key)
        {
            return Ok(await _redisManager.DeleteAsync(key));
        }
    }
}