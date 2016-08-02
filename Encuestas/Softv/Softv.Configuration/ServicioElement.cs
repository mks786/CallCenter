
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class ServicioElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Servicio class
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
        /// Gets class name for Servicio
        ///</summary>
        [ConfigurationProperty("DataClassServicio", DefaultValue = "Softv.DAO.ServicioData")]
        public String DataClass
        {
            get { return (string)base["DataClassServicio"]; }
        }

        /// <summary>
        /// Gets connection string for database Servicio access
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

