﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using Softv.Entities;
using Softv.Providers;
using SoftvConfiguration;
using Globals;

namespace Softv.DAO
{
    /// <summary>
    /// Class                   : Softv.DAO.CatalogoPeriodosCorteData
    /// Generated by            : Class Generator (c) 2014
    /// Description             : CatalogoPeriodosCorte Data Access Object
    /// File                    : CatalogoPeriodosCorteDAO.cs
    /// Creation date           : 20/05/2016
    /// Creation time           : 06:36 p. m.
    ///</summary>
    public class CatalogoPeriodosCorteData : CatalogoPeriodosCorteProvider
    {
        /// <summary>
        ///</summary>
        /// <param name="CatalogoPeriodosCorte"> Object CatalogoPeriodosCorte added to List</param>
        public override int AddCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity entity_CatalogoPeriodosCorte)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteAdd", connection);

                AssingParameter(comandoSql, "@Clv_Periodo", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@Descripcion", entity_CatalogoPeriodosCorte.Descripcion);

                AssingParameter(comandoSql, "@Habilitar", entity_CatalogoPeriodosCorte.Habilitar);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@Clv_Periodo"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a CatalogoPeriodosCorte
        ///</summary>
        /// <param name="">  Clv_Periodo to delete </param>
        public override int DeleteCatalogoPeriodosCorte(int? Clv_Periodo)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteDelete", connection);

                AssingParameter(comandoSql, "@Clv_Periodo", Clv_Periodo);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Edits a CatalogoPeriodosCorte
        ///</summary>
        /// <param name="CatalogoPeriodosCorte"> Objeto CatalogoPeriodosCorte a editar </param>
        public override int EditCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity entity_CatalogoPeriodosCorte)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteEdit", connection);

                AssingParameter(comandoSql, "@Clv_Periodo", entity_CatalogoPeriodosCorte.Clv_Periodo);

                AssingParameter(comandoSql, "@Descripcion", entity_CatalogoPeriodosCorte.Descripcion);

                AssingParameter(comandoSql, "@Habilitar", entity_CatalogoPeriodosCorte.Habilitar);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Gets all CatalogoPeriodosCorte
        ///</summary>
        public override List<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCorte()
        {
            List<CatalogoPeriodosCorteEntity> CatalogoPeriodosCorteList = new List<CatalogoPeriodosCorteEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CatalogoPeriodosCorteList.Add(GetCatalogoPeriodosCorteFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CatalogoPeriodosCorteList;
        }

        /// <summary>
        /// Gets all CatalogoPeriodosCorte by List<int>
        ///</summary>
        public override List<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCorte(List<int> lid)
        {
            List<CatalogoPeriodosCorteEntity> CatalogoPeriodosCorteList = new List<CatalogoPeriodosCorteEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CatalogoPeriodosCorteList.Add(GetCatalogoPeriodosCorteFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CatalogoPeriodosCorteList;
        }

        /// <summary>
        /// Gets CatalogoPeriodosCorte by
        ///</summary>
        public override CatalogoPeriodosCorteEntity GetCatalogoPeriodosCorteById(int? Clv_Periodo)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetById", connection);
                CatalogoPeriodosCorteEntity entity_CatalogoPeriodosCorte = null;


                AssingParameter(comandoSql, "@Clv_Periodo", Clv_Periodo);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_CatalogoPeriodosCorte = GetCatalogoPeriodosCorteFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_CatalogoPeriodosCorte;
            }

        }



        /// <summary>
        ///Get CatalogoPeriodosCorte
        ///</summary>
        public override SoftvList<CatalogoPeriodosCorteEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<CatalogoPeriodosCorteEntity> entities = new SoftvList<CatalogoPeriodosCorteEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetPaged", connection);

                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@pageSize", pageSize);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetCatalogoPeriodosCorteFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCatalogoPeriodosCorteCount();
                return entities ?? new SoftvList<CatalogoPeriodosCorteEntity>();
            }
        }

        /// <summary>
        ///Get CatalogoPeriodosCorte
        ///</summary>
        public override SoftvList<CatalogoPeriodosCorteEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<CatalogoPeriodosCorteEntity> entities = new SoftvList<CatalogoPeriodosCorteEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetPagedXml", connection);

                AssingParameter(comandoSql, "@pageSize", pageSize);
                AssingParameter(comandoSql, "@pageIndex", pageIndex);
                AssingParameter(comandoSql, "@xml", xml);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);
                    while (rd.Read())
                    {
                        entities.Add(GetCatalogoPeriodosCorteFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCatalogoPeriodosCorteCount(xml);
                return entities ?? new SoftvList<CatalogoPeriodosCorteEntity>();
            }
        }

        /// <summary>
        ///Get Count CatalogoPeriodosCorte
        ///</summary>
        public int GetCatalogoPeriodosCorteCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }


        /// <summary>
        ///Get Count CatalogoPeriodosCorte
        ///</summary>
        public int GetCatalogoPeriodosCorteCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CatalogoPeriodosCorte.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CatalogoPeriodosCorteGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CatalogoPeriodosCorte " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                }
            }
            return result;
        }

        #region Customs Methods

        #endregion
    }
}
