
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class BusquedaIndividualElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for BusquedaIndividual class
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
        /// Gets class name for BusquedaIndividual
        ///</summary>
        [ConfigurationProperty("DataClassBusquedaIndividual", DefaultValue = "Softv.DAO.BusquedaIndividualData")]
        public String DataClass
        {
          get { return (string)base["DataClassBusquedaIndividual"]; }
        }

        /// <summary>
        /// Gets connection string for database BusquedaIndividual access
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

  