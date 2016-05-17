
    using System;
    using System.Configuration;

    namespace SoftvConfiguration
    {
      public class Rel_Clientes_TiposClientesElement: ConfigurationElement
      {
        /// <summary>
        /// Gets assembly name for Rel_Clientes_TiposClientes class
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
        /// Gets class name for Rel_Clientes_TiposClientes
        ///</summary>
        [ConfigurationProperty("DataClassRel_Clientes_TiposClientes", DefaultValue = "Softv.DAO.Rel_Clientes_TiposClientesData")]
        public String DataClass
        {
          get { return (string)base["DataClassRel_Clientes_TiposClientes"]; }
        }

        /// <summary>
        /// Gets connection string for database Rel_Clientes_TiposClientes access
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

  