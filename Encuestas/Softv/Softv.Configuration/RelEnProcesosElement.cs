
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class RelEnProcesosElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for RelEnProcesos class
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
        /// Gets class name for RelEnProcesos
        ///</summary>
        [ConfigurationProperty("DataClassRelEnProcesos", DefaultValue = "Softv.DAO.RelEnProcesosData")]
        public String DataClass
        {
          get { return (string)base["DataClassRelEnProcesos"]; }
        }

        /// <summary>
        /// Gets connection string for database RelEnProcesos access
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

  