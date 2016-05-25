
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class CVECOLCIUElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for CVECOLCIU class
        /// </summary>
        [ConfigurationProperty("Assembly")]
        public String Assembly
        {
            get
            {
                string assembly = (string)base["Assembly"];
                assembly = String.IsNullOrEmpty(assembly) ?
                SoftvSettings.Settings.Assembly :
                (string)base["Assembly"];
                return assembly;
            }
        }

        /// <summary>
        /// Gets class name for CVECOLCIU
        ///</summary>
        [ConfigurationProperty("DataClassCVECOLCIU", DefaultValue = "Softv.DAO.CVECOLCIUData")]
        public String DataClass
        {
            get { return (string)base["DataClassCVECOLCIU"]; }
        }

        /// <summary>
        /// Gets connection string for database CVECOLCIU access
        ///</summary>
        [ConfigurationProperty("ConnectionString")]
        public String ConnectionString
        {
            get
            {
                string connectionString = (string)base["ConnectionString"];
                connectionString = String.IsNullOrEmpty(connectionString) ? SoftvSettings.Settings.ConnectionString : (string)base["ConnectionString"];
                return connectionString;
            }
        }
    }
}

