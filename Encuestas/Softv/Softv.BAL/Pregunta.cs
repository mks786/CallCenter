﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Linq;
using Softv.Providers;
using Softv.Entities;
using Globals;

/// <summary>
/// Class                   : Softv.BAL.Client.cs
/// Generated by            : Class Generator (c) 2014
/// Description             : PreguntaBussines
/// File                    : PreguntaBussines.cs
/// Creation date           : 27/04/2016
/// Creation time           : 05:16 p. m.
///</summary>
namespace Softv.BAL
{

    [DataObject]
    [Serializable]
    public class Pregunta
    {

        #region Constructors
        public Pregunta() { }
        #endregion

        /// <summary>
        ///Adds Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int Add(PreguntaEntity objPregunta)
        {
            //int result = ProviderSoftv.Pregunta.AddPregunta(objPregunta);
            int result = 0;

            if (objPregunta.IdTipoPregunta == 1)
            {
                objPregunta.Cerrada = true;
                objPregunta.OpcMultiple = false;
                objPregunta.Abierta = false;

                result = ProviderSoftv.Pregunta.AddPregunta(objPregunta);

            }

            if (objPregunta.IdTipoPregunta == 2)
            {
                objPregunta.Cerrada = false;
                objPregunta.OpcMultiple = true;
                objPregunta.Abierta = false;

                result = ProviderSoftv.Pregunta.AddPregunta(objPregunta);

            }

            if (objPregunta.IdTipoPregunta == 3)
            {
                objPregunta.Cerrada = false;
                objPregunta.OpcMultiple = false;
                objPregunta.Abierta = true;

                result = ProviderSoftv.Pregunta.AddPregunta(objPregunta);

            }



            return result;
        }

        /// <summary>
        ///Delete Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(int? IdPregunta)
        {
            int resultado = ProviderSoftv.Pregunta.DeletePregunta(IdPregunta);
            return resultado;
        }

        /// <summary>
        ///Update Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Edit(string xml)
        {
            int result = 0;
            result = ProviderSoftv.Pregunta.EditPregunta(xml);
            return result;




        }

        /// <summary>
        ///Get Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<PreguntaEntity> GetAll()
        {
            List<PreguntaEntity> entities = new List<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPregunta();

            List<TipoPreguntasEntity> lTipoPreguntas = ProviderSoftv.TipoPreguntas.GetTipoPreguntas(entities.Where(x => x.IdTipoPregunta.HasValue).Select(x => x.IdTipoPregunta.Value).ToList());
            lTipoPreguntas.ForEach(XTipoPreguntas => entities.Where(x => x.IdTipoPregunta.HasValue).Where(x => x.IdTipoPregunta == XTipoPreguntas.IdTipoPregunta).ToList().ForEach(y => y.TipoPreguntas = XTipoPreguntas));

            List<RelPreguntaEncuestasEntity> lRelPreguntaEncuestas = ProviderSoftv.RelPreguntaEncuestas.GetRelPreguntaEncuestas(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).ToList());
            lRelPreguntaEncuestas.ForEach(XRelPreguntaEncuestas => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaEncuestas.IdPregunta).ToList().ForEach(y => y.RelPreguntaEncuestas = XRelPreguntaEncuestas));

            List<RelPreguntaOpcMultsEntity> lRelPreguntaOpcMults = ProviderSoftv.RelPreguntaOpcMults.GetRelPreguntaOpcMults(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).ToList());
            lRelPreguntaOpcMults.ForEach(XRelPreguntaOpcMults => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaOpcMults.IdPregunta).ToList().ForEach(y => y.RelPreguntaOpcMults = XRelPreguntaOpcMults));

            List<RelEnProcesosEntity> lRelEnProcesos = ProviderSoftv.RelEnProcesos.GetRelEnProcesos(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).ToList());
            lRelEnProcesos.ForEach(XRelEnProcesos => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelEnProcesos.IdPregunta).ToList().ForEach(y => y.RelEnProcesos = XRelEnProcesos));

            return entities ?? new List<PreguntaEntity>();
        }

        /// <summary>
        ///Get Pregunta List<lid>
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<PreguntaEntity> GetAll(List<int> lid)
        {
            List<PreguntaEntity> entities = new List<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPregunta(lid);
            return entities ?? new List<PreguntaEntity>();
        }

        /// <summary>
        ///Get Pregunta By Id
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static PreguntaEntity GetOne(int? IdPregunta)
        {
            PreguntaEntity result = ProviderSoftv.Pregunta.GetPreguntaById(IdPregunta);

            if (result.IdTipoPregunta != null)
                result.TipoPreguntas = ProviderSoftv.TipoPreguntas.GetTipoPreguntasById(result.IdTipoPregunta);

            //if (result.IdPregunta != null)
            //    result.RelPreguntaEncuestas = ProviderSoftv.RelPreguntaEncuestas.GetRelPreguntaEncuestasById(result.IdPregunta);

            //if (result.IdPregunta != null)
            //    result.RelPreguntaOpcMults = ProviderSoftv.RelPreguntaOpcMults.GetRelPreguntaOpcMultsById(result.IdPregunta);

            //if (result.IdPregunta != null)
            //    result.RelEnProcesos = ProviderSoftv.RelEnProcesos.GetRelEnProcesosById(result.IdPregunta);

            return result;
        }

        /// <summary>
        ///Get Pregunta By Id
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static PreguntaEntity GetOneDeep(int? IdPregunta)
        {
            PreguntaEntity result = ProviderSoftv.Pregunta.GetPreguntaById(IdPregunta);

            if (result.IdTipoPregunta != null)
                result.TipoPreguntas = ProviderSoftv.TipoPreguntas.GetTipoPreguntasById(result.IdTipoPregunta);

            //if (result.IdPregunta != null)
            //    result.RelPreguntaEncuestas = ProviderSoftv.RelPreguntaEncuestas.GetRelPreguntaEncuestasById(result.IdPregunta);

            //if (result.IdPregunta != null)
            //    result.RelPreguntaOpcMults = ProviderSoftv.RelPreguntaOpcMults.GetRelPreguntaOpcMultsById(result.IdPregunta);

            //if (result.IdPregunta != null)
            //    result.RelEnProcesos = ProviderSoftv.RelEnProcesos.GetRelEnProcesosById(result.IdPregunta);

            return result;
        }

        public static List<PreguntaEntity> GetPreguntaByIdTipoPregunta(int? IdTipoPregunta)
        {
            List<PreguntaEntity> entities = new List<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPreguntaByIdTipoPregunta(IdTipoPregunta);
            return entities ?? new List<PreguntaEntity>();
        }

        public static List<PreguntaEntity> GetPreguntaByIdPregunta(int? IdPregunta)
        {
            List<PreguntaEntity> entities = new List<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPreguntaByIdPregunta(IdPregunta);
            return entities ?? new List<PreguntaEntity>();
        }





        /// <summary>
        ///Get Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SoftvList<PreguntaEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<PreguntaEntity> entities = new SoftvList<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPagedList(pageIndex, pageSize);

            List<TipoPreguntasEntity> lTipoPreguntas = ProviderSoftv.TipoPreguntas.GetTipoPreguntas(entities.Where(x => x.IdTipoPregunta.HasValue).Select(x => x.IdTipoPregunta.Value).Distinct().ToList());
            lTipoPreguntas.ForEach(XTipoPreguntas => entities.Where(x => x.IdTipoPregunta.HasValue).Where(x => x.IdTipoPregunta == XTipoPreguntas.IdTipoPregunta).ToList().ForEach(y => y.TipoPreguntas = XTipoPreguntas));

            //List<RelPreguntaEncuestasEntity> lRelPreguntaEncuestas = ProviderSoftv.RelPreguntaEncuestas.GetRelPreguntaEncuestas(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelPreguntaEncuestas.ForEach(XRelPreguntaEncuestas => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaEncuestas.IdPregunta).ToList().ForEach(y => y.RelPreguntaEncuestas = XRelPreguntaEncuestas));

            //List<RelPreguntaOpcMultsEntity> lRelPreguntaOpcMults = ProviderSoftv.RelPreguntaOpcMults.GetRelPreguntaOpcMults(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelPreguntaOpcMults.ForEach(XRelPreguntaOpcMults => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaOpcMults.IdPregunta).ToList().ForEach(y => y.RelPreguntaOpcMults = XRelPreguntaOpcMults));

            //List<RelEnProcesosEntity> lRelEnProcesos = ProviderSoftv.RelEnProcesos.GetRelEnProcesos(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelEnProcesos.ForEach(XRelEnProcesos => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelEnProcesos.IdPregunta).ToList().ForEach(y => y.RelEnProcesos = XRelEnProcesos));

            return entities ?? new SoftvList<PreguntaEntity>();
        }

        /// <summary>
        ///Get Pregunta
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SoftvList<PreguntaEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<PreguntaEntity> entities = new SoftvList<PreguntaEntity>();
            entities = ProviderSoftv.Pregunta.GetPagedList(pageIndex, pageSize, xml);

            List<TipoPreguntasEntity> lTipoPreguntas = ProviderSoftv.TipoPreguntas.GetTipoPreguntas(entities.Where(x => x.IdTipoPregunta.HasValue).Select(x => x.IdTipoPregunta.Value).Distinct().ToList());
            lTipoPreguntas.ForEach(XTipoPreguntas => entities.Where(x => x.IdTipoPregunta.HasValue).Where(x => x.IdTipoPregunta == XTipoPreguntas.IdTipoPregunta).ToList().ForEach(y => y.TipoPreguntas = XTipoPreguntas));

            //List<RelPreguntaEncuestasEntity> lRelPreguntaEncuestas = ProviderSoftv.RelPreguntaEncuestas.GetRelPreguntaEncuestas(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelPreguntaEncuestas.ForEach(XRelPreguntaEncuestas => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaEncuestas.IdPregunta).ToList().ForEach(y => y.RelPreguntaEncuestas = XRelPreguntaEncuestas));

            //List<RelPreguntaOpcMultsEntity> lRelPreguntaOpcMults = ProviderSoftv.RelPreguntaOpcMults.GetRelPreguntaOpcMults(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelPreguntaOpcMults.ForEach(XRelPreguntaOpcMults => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelPreguntaOpcMults.IdPregunta).ToList().ForEach(y => y.RelPreguntaOpcMults = XRelPreguntaOpcMults));

            //List<RelEnProcesosEntity> lRelEnProcesos = ProviderSoftv.RelEnProcesos.GetRelEnProcesos(entities.Where(x => x.IdPregunta.HasValue).Select(x => x.IdPregunta.Value).Distinct().ToList());
            //lRelEnProcesos.ForEach(XRelEnProcesos => entities.Where(x => x.IdPregunta.HasValue).Where(x => x.IdPregunta == XRelEnProcesos.IdPregunta).ToList().ForEach(y => y.RelEnProcesos = XRelEnProcesos));

            return entities ?? new SoftvList<PreguntaEntity>();
        }


    }




    #region Customs Methods

    #endregion
}
