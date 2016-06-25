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
    /// Class                   : Softv.DAO.EstadisticaData
    /// Generated by            : Class Generator (c) 2014
    /// Description             : Estadistica Data Access Object
    /// File                    : EstadisticaDAO.cs
    /// Creation date           : 24/06/2016
    /// Creation time           : 01:55 p. m.
    ///</summary>
    public class EstadisticaData : EstadisticaProvider
    {


        /// <summary>
        /// Gets all Estadistica
        ///</summary>
        //public override List<EstadisticaEntity> GetEstadistica()
        //{
        //    List<EstadisticaEntity> EstadisticaList = new List<EstadisticaEntity>();
        //    using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Estadistica.ConnectionString))
        //    {

        //        SqlCommand comandoSql = CreateCommand("Softv_EstadisticaGet", connection);
        //        IDataReader rd = null;
        //        try
        //        {
        //            if (connection.State == ConnectionState.Closed)
        //                connection.Open();
        //            rd = ExecuteReader(comandoSql);

        //            while (rd.Read())
        //            {
        //                EstadisticaList.Add(GetEstadisticaFromReader(rd));
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error getting data Estadistica " + ex.Message, ex);
        //        }
        //        finally
        //        {
        //            if (connection != null)
        //                connection.Close();
        //            if (rd != null)
        //                rd.Close();
        //        }
        //    }
        //    return EstadisticaList;
        //}




        public override List<EstadisticaEntity> GetEstadistica(int plaza, int idencuesta, DateTime finicio, DateTime ffin)
        {
            List<EstadisticaEntity> EncuestaList = new List<EstadisticaEntity>();
            using (SqlConnection connection = new SqlConnection(SoftvSettings.Settings.Estadistica.ConnectionString))
            {

                SqlCommand comandoSql = CreateCommand("GraficasPreguntas", connection);
                AssingParameter(comandoSql, "@IdConexion", plaza);
                AssingParameter(comandoSql, "@IdEncuesta", idencuesta);
                AssingParameter(comandoSql, "@FechaInicio", finicio);
                AssingParameter(comandoSql, "@FechaFin", ffin);
                IDataReader rd = null;
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    rd = ExecuteReader(comandoSql);

                    while (rd.Read())
                    {
                        EstadisticaEntity a = new EstadisticaEntity();
                        a.NombreEncuesta = rd[0].ToString();
                        a.IdTipoPregunta = Int32.Parse(rd[1].ToString());
                        a.IdPregunta = Int32.Parse(rd[2].ToString());
                        a.Pregunta = rd[3].ToString();
                        a.Respuesta = rd[4].ToString();
                        a.Cantidad = Int32.Parse(rd[5].ToString());
                        EncuestaList.Add(a);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error getting data Encuesta " + ex.Message, ex);
                }
                finally
                {
                    if (connection != null)
                        connection.Close();
                    if (rd != null)
                        rd.Close();
                }
            }

            //List<pregunta> pregunta = new List<pregunta>();

                
            //  var r=  EncuestaList.GroupBy(o=>o.IdPregunta).Select(o=>o.Key);
            //  foreach (var d in r)
            //  {
            //      var e = EncuestaList.Where(i=>i.IdPregunta==d.Value);

            //      foreach(var  f in e){

            //           pregunta p = new pregunta();
            //          p.titulo = f.Pregunta;
            //           List<respuestas> l = new List<respuestas>();
            //          foreach (var a in EncuestaList.Where(o => o.IdPregunta == f.IdPregunta))
            //          {
            //              respuestas r1 =new respuestas();
            //              r1.cuantos=a.Cantidad.Value;
            //              r1.respuesta = a.Respuesta;
            //              l.Add(r1);
            //          }
            //          p.respuesta = l;

            //          pregunta.Add(p);                                           
            //      }                 
            //  }


            return EncuestaList;
        }

        public class pregunta
        {
            public string titulo { get; set; }
            public List<respuestas> respuesta { get; set; }

        }

        public class respuestas {
            public int cuantos {get; set;}

            public string respuesta  {get; set;}
        }

















    







        #region Customs Methods

        #endregion
    }
}
