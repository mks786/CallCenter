
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class TapElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Tap class
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
        /// Gets class name for Tap
        ///</summary>
        [ConfigurationProperty("DataClassTap", DefaultValue = "Softv.DAO.TapData")]
        public String DataClass
        {
          get { return (string)base["DataClassTap"]; }
        }

        /// <summary>
        /// Gets connection string for database Tap access
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

  