
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class RelEncuestaClientesElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for RelEncuestaClientes class
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
        /// Gets class name for RelEncuestaClientes
        ///</summary>
        [ConfigurationProperty("DataClassRelEncuestaClientes", DefaultValue = "Softv.DAO.RelEncuestaClientesData")]
        public String DataClass
        {
            get { return (string)base["DataClassRelEncuestaClientes"]; }
        }

        /// <summary>
        /// Gets connection string for database RelEncuestaClientes access
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

