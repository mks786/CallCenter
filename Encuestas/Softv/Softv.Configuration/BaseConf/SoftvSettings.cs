using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftvConfiguration
{
    public class SoftvSettings
    {
        public readonly static SoftvSection Settings =
            (SoftvSection)ConfigurationManager.GetSection("SoftvSection");
    }
}
