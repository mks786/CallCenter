
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class ProcesoEncuestaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for ProcesoEncuesta class
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
        /// Gets class name for ProcesoEncuesta
        ///</summary>
        [ConfigurationProperty("DataClassProcesoEncuesta", DefaultValue = "Softv.DAO.ProcesoEncuestaData")]
        public String DataClass
        {
          get { return (string)base["DataClassProcesoEncuesta"]; }
        }

        /// <summary>
        /// Gets connection string for database ProcesoEncuesta access
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

  