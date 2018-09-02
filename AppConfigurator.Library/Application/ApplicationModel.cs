using AppConfigurator.Library.Enums;
using AppConfigurator.Library.Interfaces;

namespace AppConfigurator.Library.Application
{
    public class ApplicationModel : IApplicationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ApplicationValueType Type { get; set; }
        public object Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationName { get; set; }
    }
}