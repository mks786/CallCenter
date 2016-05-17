
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class RoleElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Role class
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
        /// Gets class name for Role
        ///</summary>
        [ConfigurationProperty("DataClassRole", DefaultValue = "Softv.DAO.RoleData")]
        public String DataClass
        {
            get { return (string)base["DataClassRole"]; }
        }

        /// <summary>
        /// Gets connection string for database Role access
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

