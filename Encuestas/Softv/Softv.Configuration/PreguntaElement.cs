
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class PreguntaElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Pregunta class
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
        /// Gets class name for Pregunta
        ///</summary>
        [ConfigurationProperty("DataClassPregunta", DefaultValue = "Softv.DAO.PreguntaData")]
        public String DataClass
        {
          get { return (string)base["DataClassPregunta"]; }
        }

        /// <summary>
        /// Gets connection string for database Pregunta access
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

  