
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class EstadisticaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Estadistica class
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
        /// Gets class name for Estadistica
        ///</summary>
        [ConfigurationProperty("DataClassEstadistica", DefaultValue = "Softv.DAO.EstadisticaData")]
        public String DataClass
        {
          get { return (string)base["DataClassEstadistica"]; }
        }

        /// <summary>
        /// Gets connection string for database Estadistica access
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

  