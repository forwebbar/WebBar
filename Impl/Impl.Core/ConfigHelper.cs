using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace Impl.Core
{
    public static class ConfigHelper
    {
        public static List<string> GetClientListFromAppConfig()
        {
            var list = new List<string>();
            var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            var serviceModelSectionGroup = ServiceModelSectionGroup.GetSectionGroup(configuration);
            if (serviceModelSectionGroup != null)
            {
                var clientSection = serviceModelSectionGroup.Client;
                for (var i = 0; i < clientSection.Endpoints.Count; i++)
                {
                    list.Add(clientSection.Endpoints[i].Name);
                }
            }
            return list;
        }
    }
}
