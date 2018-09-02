using System.Collections.Generic;
using AppConfigurator.Library.Application;
using AppConfigurator.Library.Interfaces;

namespace AppConfigurator.Data.DataProvider
{
    public class Database
    {
        public static List<IApplicationModel> GetConfigurations() => new List<IApplicationModel>
        {
            #region Service-A
            new ApplicationModel
            {
                Id = 1,
                Name = "SiteName",
                Type = Library.Enums.ApplicationValueType.String,
                IsActive = true,
                ApplicationName = "service-a",
                Value = "kuthaygumus.com"
            },
            new ApplicationModel
            {
                Id = 2,
                Name = "IsBasketEnabled",
                Type = Library.Enums.ApplicationValueType.Bool,
                IsActive = true,
                ApplicationName = "service-a",
                Value = 1
            },
            new ApplicationModel
            {
                Id = 3,
                Name = "MaxItemCount",
                Type = Library.Enums.ApplicationValueType.Int,
                IsActive = false,
                ApplicationName = "service-a",
                Value = 0
            },
            #endregion

            #region Service-B
            new ApplicationModel
            {
                Id = 4,
                Name = "SiteName",
                Type = Library.Enums.ApplicationValueType.String,
                IsActive = true,
                ApplicationName = "service-b",
                Value = "gumuskuthay.com"
            },
            new ApplicationModel
            {
                Id = 5,
                Name = "IsBasketEnabled",
                Type = Library.Enums.ApplicationValueType.Bool,
                IsActive = true,
                ApplicationName = "service-b",
                Value = 0
            },
            new ApplicationModel
            {
                Id = 6,
                Name = "MaxItemCount",
                Type = Library.Enums.ApplicationValueType.Int,
                IsActive = false,
                ApplicationName = "service-b",
                Value = 60
            },
            #endregion

            #region Service-C
            new ApplicationModel
            {
                Id = 7,
                Name = "SiteName",
                Type = Library.Enums.ApplicationValueType.String,
                IsActive = true,
                ApplicationName = "service-c",
                Value = "nedirneresidir.com"
            },
            new ApplicationModel
            {
                Id = 8,
                Name = "IsBasketEnabled",
                Type = Library.Enums.ApplicationValueType.Bool,
                IsActive = true,
                ApplicationName = "service-c",
                Value = 1
            },
            new ApplicationModel
            {
                Id = 9,
                Name = "MaxItemCount",
                Type = Library.Enums.ApplicationValueType.Int,
                IsActive = false,
                ApplicationName = "service-c",
                Value = 70
            }
            #endregion
        };
    }
}