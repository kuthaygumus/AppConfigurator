namespace AppConfigurator.Library.Interfaces
{
    public interface IConfiguration
    {
        T GetValue<T>(string key);
    }
}