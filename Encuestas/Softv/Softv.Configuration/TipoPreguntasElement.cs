
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class TipoPreguntasElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for TipoPreguntas class
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
        /// Gets class name for TipoPreguntas
        ///</summary>
        [ConfigurationProperty("DataClassTipoPreguntas", DefaultValue = "Softv.DAO.TipoPreguntasData")]
        public String DataClass
        {
            get { return (string)base["DataClassTipoPreguntas"]; }
        }

        /// <summary>
        /// Gets connection string for database TipoPreguntas access
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

