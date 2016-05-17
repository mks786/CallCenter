
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class DatoFiscalElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for DatoFiscal class
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
        /// Gets class name for DatoFiscal
        ///</summary>
        [ConfigurationProperty("DataClassDatoFiscal", DefaultValue = "Softv.DAO.DatoFiscalData")]
        public String DataClass
        {
          get { return (string)base["DataClassDatoFiscal"]; }
        }

        /// <summary>
        /// Gets connection string for database DatoFiscal access
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

  