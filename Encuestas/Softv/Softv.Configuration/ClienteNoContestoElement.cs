
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class ClienteNoContestoElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for ClienteNoContesto class
        /// </summary>
        [ConfigurationProperty( "Assembly")]
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
        /// Gets class name for ClienteNoContesto
        ///</summary>
        [ConfigurationProperty("DataClassClienteNoContesto", DefaultValue = "Softv.DAO.ClienteNoContestoData")]
        public String DataClass
        {
          get { return (string)base["DataClassClienteNoContesto"]; }
        }

        /// <summary>
        /// Gets connection string for database ClienteNoContesto access
        ///</summary>
        [ConfigurationProperty("ConnectionString")]
        public String ConnectionString
        {
          get
          {
            string connectionString = (string)base["ConnectionString"];
            connectionString = String.IsNullOrEmpty(connectionString) ? SoftvSettings.Settings.ConnectionString :  (string)base["ConnectionString"];
            return connectionString;
          }
        }
      }
    }

  