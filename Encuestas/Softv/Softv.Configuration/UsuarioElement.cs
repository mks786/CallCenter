
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class UsuarioElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Usuario class
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
        /// Gets class name for Usuario
        ///</summary>
        [ConfigurationProperty("DataClassUsuario", DefaultValue = "Softv.DAO.UsuarioData")]
        public String DataClass
        {
            get { return (string)base["DataClassUsuario"]; }
        }

        /// <summary>
        /// Gets connection string for database Usuario access
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

