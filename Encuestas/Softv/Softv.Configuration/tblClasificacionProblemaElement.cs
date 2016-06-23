﻿
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class tblClasificacionProblemaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for tblClasificacionProblema class
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
        /// Gets class name for tblClasificacionProblema
        ///</summary>
        [ConfigurationProperty("DataClasstblClasificacionProblema", DefaultValue = "Softv.DAO.tblClasificacionProblemaData")]
        public String DataClass
        {
          get { return (string)base["DataClasstblClasificacionProblema"]; }
        }

        /// <summary>
        /// Gets connection string for database tblClasificacionProblema access
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

  