using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaBot.Controllers
{
    class LicenseController
    {
        public bool HasValidLicenseAgreement(string owner, string reponame, string username)
        {
            return true;
        }

        public static LicenseController Instance()
        {
            return new LicenseController();
        }
    }
}
