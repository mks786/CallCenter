
using System;
using System.Configuration;

namespace SoftvConfiguration
{
    public class QuejaElement : ConfigurationElement
    {
        /// <summary>
        /// Gets assembly name for Queja class
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
        /// Gets class name for Queja
        ///</summary>
        [ConfigurationProperty("DataClassQueja", DefaultValue = "Softv.DAO.QuejaData")]
        public String DataClass
        {
            get { return (string)base["DataClassQueja"]; }
        }

        /// <summary>
        /// Gets connection string for database Queja access
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

