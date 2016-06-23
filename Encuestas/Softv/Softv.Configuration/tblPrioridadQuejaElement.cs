
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class tblPrioridadQuejaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for tblPrioridadQueja class
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
        /// Gets class name for tblPrioridadQueja
        ///</summary>
        [ConfigurationProperty("DataClasstblPrioridadQueja", DefaultValue = "Softv.DAO.tblPrioridadQuejaData")]
        public String DataClass
        {
          get { return (string)base["DataClasstblPrioridadQueja"]; }
        }

        /// <summary>
        /// Gets connection string for database tblPrioridadQueja access
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

  