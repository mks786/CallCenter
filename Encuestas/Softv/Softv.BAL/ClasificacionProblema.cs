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
/// Description             : ClasificacionProblemaBussines
/// File                    : ClasificacionProblemaBussines.cs
/// Creation date           : 28/07/2016
/// Creation time           : 06:23 p. m.
///</summary>
namespace Softv.BAL
{

    [DataObject]
    [Serializable]
    public class ClasificacionProblema
    {

        #region Constructors
        public ClasificacionProblema() { }
        #endregion

        /// <summary>
        ///Adds ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public static int Add(ClasificacionProblemaEntity objClasificacionProblema)
        {
            int result = ProviderSoftv.ClasificacionProblema.AddClasificacionProblema(objClasificacionProblema);
            return result;
        }

        /// <summary>
        ///Delete ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public static int Delete(long? ClvProblema)
        {
            int resultado = ProviderSoftv.ClasificacionProblema.DeleteClasificacionProblema(ClvProblema);
            return resultado;
        }

        /// <summary>
        ///Update ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public static int Edit(ClasificacionProblemaEntity objClasificacionProblema)
        {
            int result = ProviderSoftv.ClasificacionProblema.EditClasificacionProblema(objClasificacionProblema);
            return result;
        }

        /// <summary>
        ///Get ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<ClasificacionProblemaEntity> GetAll()
        {
            List<ClasificacionProblemaEntity> entities = new List<ClasificacionProblemaEntity>();
            entities = ProviderSoftv.ClasificacionProblema.GetClasificacionProblema();

            return entities ?? new List<ClasificacionProblemaEntity>();
        }

        /// <summary>
        ///Get ClasificacionProblema List<lid>
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static List<ClasificacionProblemaEntity> GetAll(List<int> lid)
        {
            List<ClasificacionProblemaEntity> entities = new List<ClasificacionProblemaEntity>();
            entities = ProviderSoftv.ClasificacionProblema.GetClasificacionProblema(lid);
            return entities ?? new List<ClasificacionProblemaEntity>();
        }

        /// <summary>
        ///Get ClasificacionProblema By Id
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ClasificacionProblemaEntity GetOne(long? ClvProblema)
        {
            ClasificacionProblemaEntity result = ProviderSoftv.ClasificacionProblema.GetClasificacionProblemaById(ClvProblema);

            return result;
        }

        /// <summary>
        ///Get ClasificacionProblema By Id
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static ClasificacionProblemaEntity GetOneDeep(long? ClvProblema)
        {
            ClasificacionProblemaEntity result = ProviderSoftv.ClasificacionProblema.GetClasificacionProblemaById(ClvProblema);

            return result;
        }



        /// <summary>
        ///Get ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SoftvList<ClasificacionProblemaEntity> GetPagedList(int pageIndex, int pageSize)
        {
            SoftvList<ClasificacionProblemaEntity> entities = new SoftvList<ClasificacionProblemaEntity>();
            entities = ProviderSoftv.ClasificacionProblema.GetPagedList(pageIndex, pageSize);

            return entities ?? new SoftvList<ClasificacionProblemaEntity>();
        }

        /// <summary>
        ///Get ClasificacionProblema
        ///</summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public static SoftvList<ClasificacionProblemaEntity> GetPagedList(int pageIndex, int pageSize, String xml)
        {
            SoftvList<ClasificacionProblemaEntity> entities = new SoftvList<ClasificacionProblemaEntity>();
            entities = ProviderSoftv.ClasificacionProblema.GetPagedList(pageIndex, pageSize, xml);

            return entities ?? new SoftvList<ClasificacionProblemaEntity>();
        }


    }




    #region Customs Methods

    #endregion
}