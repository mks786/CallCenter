﻿
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class NoClienteElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for NoCliente class
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
        /// Gets class name for NoCliente
        ///</summary>
        [ConfigurationProperty("DataClassNoCliente", DefaultValue = "Softv.DAO.NoClienteData")]
        public String DataClass
        {
          get { return (string)base["DataClassNoCliente"]; }
        }

        /// <summary>
        /// Gets connection string for database NoCliente access
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

  