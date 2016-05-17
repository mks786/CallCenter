using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Globals
{
    /// <summary>
    /// Clase           : Epsilon.Global.DataAccess
    /// Descripc      : Funciona como clase base para todos los objetos DAO, contiene elementos clave como cache, conexión y duración del caché
    /// Archivo       : DataAccess.cs
    /// Fecha crea  : 12 Febrero del 2014
    /// Fecha ult     : 12 Febrero del 2014
    /// Versión        : 1.0
    /// </summary>
    public abstract class DataAccess
    {
        private static string globalConectionString;

        /// <summary>
        /// Gets connection String from App.config or Web.config
        /// </summary>
        public static string GlobalConectionString
        {
            get
            {

                //if (String.IsNullOrEmpty(globalConectionString))
                //    globalConectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\ASPNETDB.MDF;Integrated Security=True;User Instance=True";
                //else
                //{
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    try
                    {
                        cnn.Open();
                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                            globalConectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                        }
                        else
                        {
                            globalConectionString = ConfigurationManager.ConnectionStrings["ConnectionStringFailover"].ConnectionString;
                        }
                    }
                    catch (Exception ex)
                    {
                        globalConectionString = ConfigurationManager.ConnectionStrings["ConnectionStringFailover"].ConnectionString;
                    }
                }
                //}

                return globalConectionString;
            }

            set
            {
                globalConectionString = value;
            }
        }
        /// <summary>
        /// Gets connection String from App.config or Web.config
        /// </summary>
        public static string GlobalConectionStringHistory
        {
            get
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["HistoryConnectionString"].ConnectionString))
                {
                    try
                    {
                        cnn.Open();
                        if (cnn.State == ConnectionState.Open)
                        {
                            cnn.Close();
                            globalConectionString = ConfigurationManager.ConnectionStrings["HistoryConnectionString"].ConnectionString;
                        }
                        else
                        {
                            globalConectionString = ConfigurationManager.ConnectionStrings["HistoryConnectionStringFailover"].ConnectionString;
                        }
                    }
                    catch (Exception ex)
                    {
                        globalConectionString = ConfigurationManager.ConnectionStrings["HistoryConnectionStringFailover"].ConnectionString;
                    }
                }
                return globalConectionString;
            }

            set
            {
                globalConectionString = value;
            }
        }

        #region Aquí están los métodos
        /// <summary>
        /// Método que ejecuta sentencias de acción
        /// </summary>
        protected int ExecuteNonQuery(DbCommand cmd)
        {
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// Método que ejecuta sentencias de selección de solo lectura
        /// </summary>
        protected IDataReader ExecuteReader(DbCommand cmd)
        {
            return ExecuteReader(cmd, CommandBehavior.Default);
        }
        /// <summary>
        /// Método que ejecuta sentencias de selección de solo lectura
        /// </summary>
        protected IDataReader ExecuteReader(DbCommand cmd, CommandBehavior behavior)
        {
            return cmd.ExecuteReader(behavior);
        }
        /// <summary>
        /// Método que obtiene un dato de la bd
        /// </summary>
        protected object ExecuteScalar(DbCommand cmd)
        {
            return cmd.ExecuteScalar();
        }

        protected DataTable BuildTableID(List<int> lid)
        {
            DataTable myDataTable = new DataTable("IdTableType");
            myDataTable.Columns.Add("Id", typeof(Int32));
            lid.ForEach(λ => myDataTable.Rows.Add(λ));
            return myDataTable;
        }
        protected DataTable BuildTableID(List<String> lid)
        {
            DataTable myDataTable = new DataTable("IdTableType");
            myDataTable.Columns.Add("Id", typeof(Int32));
            lid.ForEach(λ => myDataTable.Rows.Add(λ));
            return myDataTable;
        }


        protected DataTable BuildTableID(List<long> lid)
        {
            DataTable myDataTable = new DataTable("IdTableType");
            myDataTable.Columns.Add("Id", typeof(Int32));
            lid.ForEach(λ => myDataTable.Rows.Add(λ));
            return myDataTable;
        }




        protected Object GetFromReader(IDataReader reader, String FieldName, bool IsString = false)
        {
            return IsString ? (reader[FieldName].ToString().Trim() == "" || reader[FieldName] == DBNull.Value ? null : reader[FieldName])
                : (reader[FieldName] == DBNull.Value ? null : reader[FieldName]);
        }

        public static SqlCommand CreateCommand(String cmd, SqlConnection cnn, CommandType ct = CommandType.StoredProcedure)
        {
            return new SqlCommand(cmd, cnn) { CommandType = ct };
        }

        public static void AssingParameter(SqlCommand cmd, String ParameterName, object Value, ParameterDirection pd = ParameterDirection.Input, bool IsKey = false)
        {
            if (IsKey == true && pd == ParameterDirection.Output)
            {
                AssingParameterOuputInteger(cmd, ParameterName);
            }
            else
            {
                if (Value != null && Value.GetType() == typeof(System.String))
                    cmd.Parameters.AddWithValue(ParameterName, String.IsNullOrEmpty(Value.ToString()) ? DBNull.Value : (object)Value.ToString().TrimStart().TrimEnd());
                else if (Value != null && Value.GetType() == typeof(System.Byte[]))
                    cmd.Parameters.Add(new SqlParameter(ParameterName, SqlDbType.VarBinary) { Value = Value ?? (object)Value });
                else if (Value != null && Value.GetType() == typeof(DataTable))
                    cmd.Parameters.Add(new SqlParameter(ParameterName, System.Data.SqlDbType.Structured) { Value = Value });
                else
                    cmd.Parameters.AddWithValue(ParameterName, Value ?? (object)DBNull.Value);
            }
        }

        public static void AssingParameterOuputInteger(SqlCommand cmd, String ParameterName, SqlDbType dbt = SqlDbType.Int)
        {
            cmd.Parameters.Add(ParameterName, dbt);
            cmd.Parameters[ParameterName].Direction = ParameterDirection.Output;
        }
        #endregion
    }
}
