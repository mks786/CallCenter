
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class MotivoCancelacionElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for MotivoCancelacion class
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
        /// Gets class name for MotivoCancelacion
        ///</summary>
        [ConfigurationProperty("DataClassMotivoCancelacion", DefaultValue = "Softv.DAO.MotivoCancelacionData")]
        public String DataClass
        {
            get { return (string)base["DataClassMotivoCancelacion"]; }
        }

        /// <summary>
        /// Gets connection string for database MotivoCancelacion access
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

