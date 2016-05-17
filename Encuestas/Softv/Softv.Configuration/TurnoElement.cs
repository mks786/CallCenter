
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class TurnoElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Turno class
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
        /// Gets class name for Turno
        ///</summary>
        [ConfigurationProperty("DataClassTurno", DefaultValue = "Softv.DAO.TurnoData")]
        public String DataClass
        {
          get { return (string)base["DataClassTurno"]; }
        }

        /// <summary>
        /// Gets connection string for database Turno access
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

  