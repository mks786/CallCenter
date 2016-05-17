
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class PermisoElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Permiso class
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
        /// Gets class name for Permiso
        ///</summary>
        [ConfigurationProperty("DataClassPermiso", DefaultValue = "Softv.DAO.PermisoData")]
        public String DataClass
        {
            get { return (string)base["DataClassPermiso"]; }
        }

        /// <summary>
        /// Gets connection string for database Permiso access
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

