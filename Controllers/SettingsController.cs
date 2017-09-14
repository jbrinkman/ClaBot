using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaBot.Controllers
{
    public class SettingsController
    {
        public string EndpointUrl => ConfigurationManager.AppSettings["Endpoint"];
        public string PrimaryKey => ConfigurationManager.AppSettings["PrimaryKey"];

        public static SettingsController Instance() {
            return new SettingsController();
        }

    }
}
