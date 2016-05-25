
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class CIUDADElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for CIUDAD class
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
        /// Gets class name for CIUDAD
        ///</summary>
        [ConfigurationProperty("DataClassCIUDAD", DefaultValue = "Softv.DAO.CIUDADData")]
        public String DataClass
        {
            get { return (string)base["DataClassCIUDAD"]; }
        }

        /// <summary>
        /// Gets connection string for database CIUDAD access
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

