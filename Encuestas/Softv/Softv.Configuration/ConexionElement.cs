
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class ConexionElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Conexion class
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
        /// Gets class name for Conexion
        ///</summary>
        [ConfigurationProperty("DataClassConexion", DefaultValue = "Softv.DAO.ConexionData")]
        public String DataClass
        {
          get { return (string)base["DataClassConexion"]; }
        }

        /// <summary>
        /// Gets connection string for database Conexion access
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

  