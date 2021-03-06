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
    /// Class                   : Softv.DAO.CVECAROLData
    /// Generated by            : Class Generator (c) 2014
    /// Description             : CVECAROL Data Access Object
    /// File                    : CVECAROLDAO.cs
    /// Creation date           : 20/05/2016
    /// Creation time           : 06:39 p. m.
    ///</summary>
    public class CVECAROLData : CVECAROLProvider
    {
        /// <summary>
        ///</summary>
        /// <param name="CVECAROL"> Object CVECAROL added to List</param>
        public override int AddCVECAROL(CVECAROLEntity entity_CVECAROL)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLAdd", connection);

                AssingParameter(comandoSql, "@Clv_Colonia", null, pd: ParameterDirection.Output, IsKey: true);

                AssingParameter(comandoSql, "@Clv_Calle", entity_CVECAROL.Clv_Calle);

                AssingParameter(comandoSql, "@NumCasas", entity_CVECAROL.NumCasas);

                AssingParameter(comandoSql, "@NumNegocios", entity_CVECAROL.NumNegocios);

                AssingParameter(comandoSql, "@NumBaldios", entity_CVECAROL.NumBaldios);

                AssingParameter(comandoSql, "@Sector", entity_CVECAROL.Sector);

                AssingParameter(comandoSql, "@Troncal", entity_CVECAROL.Troncal);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error adding CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    connection.Close();
                }
                result = (int)comandoSql.Parameters["@Clv_Colonia"].Value;
            }
            return result;
        }

        /// <summary>
        /// Deletes a CVECAROL
        ///</summary>
        /// <param name="">  Clv_Calle,Clv_Colonia to delete </param>
        public override int DeleteCVECAROL(int? Clv_Colonia)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLDelete", connection);

                AssingParameter(comandoSql, "@Clv_Colonia", Clv_Colonia);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = ExecuteNonQuery(comandoSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error deleting CVECAROL " + ex.Message, ex);
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
        /// Edits a CVECAROL
        ///</summary>
        /// <param name="CVECAROL"> Objeto CVECAROL a editar </param>
        public override int EditCVECAROL(CVECAROLEntity entity_CVECAROL)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLEdit", connection);

                AssingParameter(comandoSql, "@Clv_Calle", entity_CVECAROL.Clv_Calle);

                AssingParameter(comandoSql, "@Clv_Colonia", entity_CVECAROL.Clv_Colonia);

                AssingParameter(comandoSql, "@NumCasas", entity_CVECAROL.NumCasas);

                AssingParameter(comandoSql, "@NumNegocios", entity_CVECAROL.NumNegocios);

                AssingParameter(comandoSql, "@NumBaldios", entity_CVECAROL.NumBaldios);

                AssingParameter(comandoSql, "@Sector", entity_CVECAROL.Sector);

                AssingParameter(comandoSql, "@Troncal", entity_CVECAROL.Troncal);

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    result = int.Parse(ExecuteNonQuery(comandoSql).ToString());

                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating CVECAROL " + ex.Message, ex);
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
        /// Gets all CVECAROL
        ///</summary>
        public override List<CVECAROLEntity> GetCVECAROL()
        {
            List<CVECAROLEntity> CVECAROLList = new List<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGet", connection);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CVECAROLList.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CVECAROLList;
        }

        /// <summary>
        /// Gets all CVECAROL by List<int>
        ///</summary>
        public override List<CVECAROLEntity> GetCVECAROL(List<int> lid)
        {
            List<CVECAROLEntity> CVECAROLList = new List<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {
                DataTable IdDT = BuildTableID(lid);

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetByIds", connection);
                AssingParameter(comandoSql, "@IdTable", IdDT);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CVECAROLList.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CVECAROLList;
        }

        /// <summary>
        /// Gets CVECAROL by
        ///</summary>
        public override CVECAROLEntity GetCVECAROLById(int? Clv_Colonia)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetById", connection);
                CVECAROLEntity entity_CVECAROL = null;

                AssingParameter(comandoSql, "@Clv_Colonia", Clv_Colonia);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_CVECAROL = GetCVECAROLFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_CVECAROL;
            }

        }






        public override CVECAROLEntity GetCVECAROLByCalle(int? Clv_Calle)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_", connection);
                CVECAROLEntity entity_CVECAROL = null;


                AssingParameter(comandoSql, "@Clv_Calle", Clv_Calle);


                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_CVECAROL = GetCVECAROLFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_CVECAROL;
            }

        }


        public override CVECAROLEntity GetCVECAROLByColonia(int? Clv_Colonia)
        {
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetById", connection);
                CVECAROLEntity entity_CVECAROL = null;


                AssingParameter(comandoSql, "@Clv_Colonia", Clv_Colonia);

                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql, CommandBehavior.SingleRow);
                    if (rd.Read())
                        entity_CVECAROL = GetCVECAROLFromReader(rd);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                return entity_CVECAROL;
            }

        }












































        public override List<CVECAROLEntity> GetCVECAROLByClv_Calle(int? Clv_Calle)
        {
            List<CVECAROLEntity> CVECAROLList = new List<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetByClv_Calle", connection);

                AssingParameter(comandoSql, "@Clv_Calle", Clv_Calle);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CVECAROLList.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CVECAROLList;
        }

        public override List<CVECAROLEntity> GetCVECAROLByClv_Colonia(int? Clv_Colonia)
        {
            List<CVECAROLEntity> CVECAROLList = new List<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetByClv_Colonia", connection);

                AssingParameter(comandoSql, "@Clv_Colonia", Clv_Colonia);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        CVECAROLList.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }
            return CVECAROLList;
        }


        /// <summary>
        ///Get CVECAROL
        ///</summary>
        public override SoftvList<CVECAROLEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<CVECAROLEntity> entities = new SoftvList<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetPaged", connection);

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
                        entities.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCVECAROLCount();
                return entities ?? new SoftvList<CVECAROLEntity>();
            }
        }

        /// <summary>
        ///Get CVECAROL
        ///</summary>
        public override SoftvList<CVECAROLEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<CVECAROLEntity> entities = new SoftvList<CVECAROLEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetPagedXml", connection);

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
                        entities.Add(GetCVECAROLFromReader(rd));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
                entities.totalCount = GetCVECAROLCount(xml);
                return entities ?? new SoftvList<CVECAROLEntity>();
            }
        }

        /// <summary>
        ///Get Count CVECAROL
        ///</summary>
        public int GetCVECAROLCount()
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetCount", connection);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
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
        ///Get Count CVECAROL
        ///</summary>
        public int GetCVECAROLCount(String xml)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.CVECAROL.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("Softv_CVECAROLGetCountXml", connection);

                AssingParameter(comandoSql, "@xml", xml);
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    result = (int)ExecuteScalar(comandoSql);


                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data CVECAROL " + ex.Message, ex);
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
