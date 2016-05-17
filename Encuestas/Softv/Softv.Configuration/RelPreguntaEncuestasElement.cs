
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class RelPreguntaEncuestasElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for RelPreguntaEncuestas class
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
        /// Gets class name for RelPreguntaEncuestas
        ///</summary>
        [ConfigurationProperty("DataClassRelPreguntaEncuestas", DefaultValue = "Softv.DAO.RelPreguntaEncuestasData")]
        public String DataClass
        {
          get { return (string)base["DataClassRelPreguntaEncuestas"]; }
        }

        /// <summary>
        /// Gets connection string for database RelPreguntaEncuestas access
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

  