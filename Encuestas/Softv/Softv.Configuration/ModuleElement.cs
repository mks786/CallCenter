
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class ModuleElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Module class
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
        /// Gets class name for Module
        ///</summary>
        [ConfigurationProperty("DataClassModule", DefaultValue = "Softv.DAO.ModuleData")]
        public String DataClass
        {
            get { return (string)base["DataClassModule"]; }
        }

        /// <summary>
        /// Gets connection string for database Module access
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

