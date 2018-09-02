using AppConfigurator.Library.Enums;

namespace AppConfigurator.Library.Interfaces
{
    public interface IApplicationModel
    {
        int Id { get; set; }

        string Name { get; set; }

        ApplicationValueType Type { get; set; }

        object Value { get; set; }

        bool IsActive { get; set; }

        string ApplicationName { get; set; }
    }
}