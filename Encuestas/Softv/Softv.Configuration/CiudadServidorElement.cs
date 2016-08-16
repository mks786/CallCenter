
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class CiudadServidorElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for CiudadServidor class
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
        /// Gets class name for CiudadServidor
        ///</summary>
        [ConfigurationProperty("DataClassCiudadServidor", DefaultValue = "Softv.DAO.CiudadServidorData")]
        public String DataClass
        {
          get { return (string)base["DataClassCiudadServidor"]; }
        }

        /// <summary>
        /// Gets connection string for database CiudadServidor access
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

  