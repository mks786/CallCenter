
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class DatosLlamadaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for DatosLlamada class
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
        /// Gets class name for DatosLlamada
        ///</summary>
        [ConfigurationProperty("DataClassDatosLlamada", DefaultValue = "Softv.DAO.DatosLlamadaData")]
        public String DataClass
        {
          get { return (string)base["DataClassDatosLlamada"]; }
        }

        /// <summary>
        /// Gets connection string for database DatosLlamada access
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

  