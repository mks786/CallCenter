﻿
using SoftvMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Softv.Entities;
using Globals;
using System.Data;
using System.Data.SqlClient;

namespace SoftvMVC.Controllers
{
    /// <summary>
    /// Class                   : SoftvMVC.Controllers.MotivoCancelacionController.cs
    /// Generated by            : Class Generator (c) 2015
    /// Description             : MotivoCancelacionController
    /// File                    : MotivoCancelacionController.cs
    /// Creation date           : 20/05/2016
    /// Creation time           : 06:35 p. m.
    ///</summary>
    public partial class MotivoCancelacionController : BaseController, IDisposable
    {
        private SoftvService.MotivoCancelacionClient proxy = null;

        public MotivoCancelacionController()
        {

            proxy = new SoftvService.MotivoCancelacionClient();

        }

        new public void Dispose()
        {
            if (proxy != null)
            {
                if (proxy.State != System.ServiceModel.CommunicationState.Closed)
                {
                    proxy.Close();
                }
            }

        }

        public ActionResult Index(int? page, int? pageSize)
        {
            PermisosAcceso("MotivoCancelacion");
            ViewData["Title"] = "MotivoCancelacion";
            ViewData["Message"] = "MotivoCancelacion";
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            int pageNumber = (page ?? 1);
            SoftvList<MotivoCancelacionEntity> listResult = proxy.GetMotivoCancelacionPagedListXml(pageNumber, pSize, SerializeTool.Serialize<MotivoCancelacionEntity>(new MotivoCancelacionEntity()));


            CheckNotify();
            ViewBag.CustomScriptsDefault = BuildScriptsDefault("MotivoCancelacion");
            return View(new StaticPagedList<MotivoCancelacionEntity>(listResult.ToList(), pageNumber, pSize, listResult.totalCount));
        }

        public ActionResult Details(int id = 0)
        {
            MotivoCancelacionEntity objMotivoCancelacion = proxy.GetMotivoCancelacion(id);
            if (objMotivoCancelacion == null)
            {
                return HttpNotFound();
            }
            return PartialView(objMotivoCancelacion);
        }

        public ActionResult Create()
        {
            PermisosAccesoDeniedCreate("MotivoCancelacion");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();

            return View();
        }

        [HttpPost]
        public ActionResult Create(MotivoCancelacionEntity objMotivoCancelacion)
        {
            if (ModelState.IsValid)
            {

                objMotivoCancelacion.BaseRemoteIp = RemoteIp;
                objMotivoCancelacion.BaseIdUser = LoggedUserName;
                int result = proxy.AddMotivoCancelacion(objMotivoCancelacion);
                if (result == -1)
                {

                    AssingMessageScript("El MotivoCancelacion ya existe en el sistema.", "error", "Error", true);
                    CheckNotify();
                    return View(objMotivoCancelacion);
                }
                if (result > 0)
                {
                    AssingMessageScript("Se dio de alta el MotivoCancelacion en el sistema.", "success", "Éxito", true);
                    return RedirectToAction("Index");
                }

            }
            return View(objMotivoCancelacion);
        }

        public ActionResult Edit(int id = 0)
        {
            PermisosAccesoDeniedEdit("MotivoCancelacion");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
            MotivoCancelacionEntity objMotivoCancelacion = proxy.GetMotivoCancelacion(id);

            if (objMotivoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(objMotivoCancelacion);
        }

        //
        // POST: /MotivoCancelacion/Edit/5
        [HttpPost]
        public ActionResult Edit(MotivoCancelacionEntity objMotivoCancelacion)
        {
            if (ModelState.IsValid)
            {
                objMotivoCancelacion.BaseRemoteIp = RemoteIp;
                objMotivoCancelacion.BaseIdUser = LoggedUserName;
                int result = proxy.UpdateMotivoCancelacion(objMotivoCancelacion);
                if (result == -1)
                {
                    MotivoCancelacionEntity objMotivoCancelacionOld = proxy.GetMotivoCancelacion(objMotivoCancelacion.Clv_MOTCAN);

                    AssingMessageScript("El MotivoCancelacion ya existe en el sistema, .", "error", "Error", true);
                    CheckNotify();
                    return View(objMotivoCancelacion);
                }
                if (result > 0)
                {
                    AssingMessageScript("El MotivoCancelacion se modifico en el sistema.", "success", "Éxito", true);
                    CheckNotify();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(objMotivoCancelacion);
        }

        public ActionResult QuickIndex(int? page, int? pageSize, String MOTCAN)
        {
            int pageNumber = (page ?? 1);
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            SoftvList<MotivoCancelacionEntity> listResult = null;
            List<MotivoCancelacionEntity> listMotivoCancelacion = new List<MotivoCancelacionEntity>();
            MotivoCancelacionEntity objMotivoCancelacion = new MotivoCancelacionEntity();
            MotivoCancelacionEntity objGetMotivoCancelacion = new MotivoCancelacionEntity();


            if ((MOTCAN != null && MOTCAN.ToString() != ""))
            {
                objMotivoCancelacion.MOTCAN = MOTCAN;
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            listResult = proxy.GetMotivoCancelacionPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objMotivoCancelacion));
            if (listResult.Count == 0)
            {
                int tempPageNumber = (int)(listResult.totalCount / pSize);
                pageNumber = (int)(listResult.totalCount / pSize) == 0 ? 1 : tempPageNumber;
                listResult = proxy.GetMotivoCancelacionPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objMotivoCancelacion));
            }
            listResult.ToList().ForEach(x => listMotivoCancelacion.Add(x));

            var MotivoCancelacionAsIPagedList = new StaticPagedList<MotivoCancelacionEntity>(listMotivoCancelacion, pageNumber, pSize, listResult.totalCount);
            if (MotivoCancelacionAsIPagedList.Count > 0)
            {
                return PartialView(MotivoCancelacionAsIPagedList);
            }
            else
            {
                var result = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "No se encontraron registros con los criterios de búsqueda ingresados." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id = 0)
        {
            int result = proxy.DeleteMotivoCancelacion(RemoteIp, LoggedUserName, id);
            if (result > 0)
            {
                var resultOk = new { tipomsj = "success", titulomsj = "Aviso", Success = "True", Message = "Registro de MotivoCancelacion Eliminado." };
                return Json(resultOk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultNg = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "El Registro de MotivoCancelacion No puede ser Eliminado validar dependencias." };
                return Json(resultNg, JsonRequestBehavior.AllowGet);
            }
        }
        //public ActionResult GetMotivoCancelacion()
        //{
        //    return Json(proxy.GetMotivoCancelacionList(), JsonRequestBehavior.AllowGet);
        //}

        public class DatosMotCan
        {
            public int Clv_MOTCAN { get; set; }
            public String MOTCAN { get; set; }
        }

        public ActionResult GetMotivoCancelacion(int numModal, int idConexion)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idConexion));
            List<DatosMotCan> lista = new List<DatosMotCan>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec Muestra_MotCanc_Reporte ");

                //comandoSql = new SqlCommand("exec DatosTipoCliente " + numModal + ", " + idConexion + "");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DatosMotCan datos = new DatosMotCan();
                        datos.Clv_MOTCAN = Convert.ToInt32(reader[0]);
                        datos.MOTCAN = reader[1].ToString();
                        lista.Add(datos);
                    }
                }
            }
            catch
            { }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }


    }

}

