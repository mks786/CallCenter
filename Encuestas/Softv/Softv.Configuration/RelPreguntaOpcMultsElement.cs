
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class RelPreguntaOpcMultsElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for RelPreguntaOpcMults class
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
        /// Gets class name for RelPreguntaOpcMults
        ///</summary>
        [ConfigurationProperty("DataClassRelPreguntaOpcMults", DefaultValue = "Softv.DAO.RelPreguntaOpcMultsData")]
        public String DataClass
        {
          get { return (string)base["DataClassRelPreguntaOpcMults"]; }
        }

        /// <summary>
        /// Gets connection string for database RelPreguntaOpcMults access
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

  