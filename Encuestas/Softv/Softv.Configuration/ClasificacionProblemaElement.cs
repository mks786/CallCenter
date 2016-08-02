﻿
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class ClasificacionProblemaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for ClasificacionProblema class
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
        /// Gets class name for ClasificacionProblema
        ///</summary>
        [ConfigurationProperty("DataClassClasificacionProblema", DefaultValue = "Softv.DAO.ClasificacionProblemaData")]
        public String DataClass
        {
          get { return (string)base["DataClassClasificacionProblema"]; }
        }

        /// <summary>
        /// Gets connection string for database ClasificacionProblema access
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

  