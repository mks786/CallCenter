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
using System.Data.SqlClient;

namespace SoftvMVC.Controllers
{
    /// <summary>
    /// Class                   : SoftvMVC.Controllers.tblClasificacionProblemaController.cs
    /// Generated by            : Class Generator (c) 2015
    /// Description             : tblClasificacionProblemaController
    /// File                    : tblClasificacionProblemaController.cs
    /// Creation date           : 08/06/2016
    /// Creation time           : 10:52 a. m.
    ///</summary>
    public partial class tblClasificacionProblemaController : BaseController, IDisposable
    {
        private SoftvService.ItblClasificacionProblemaClient proxy = null;
        private SoftvService.ClasificacionProblemaClient proxyclass = null;

        public tblClasificacionProblemaController()
        {

            proxy = new SoftvService.ItblClasificacionProblemaClient();
            proxyclass = new SoftvService.ClasificacionProblemaClient();

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
            PermisosAcceso("tblClasificacionProblema");
            ViewData["Title"] = "tblClasificacionProblema";
            ViewData["Message"] = "tblClasificacionProblema";
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            int pageNumber = (page ?? 1);
            SoftvList<tblClasificacionProblemaEntity> listResult = proxy.GettblClasificacionProblemaPagedListXml(pageNumber, pSize, SerializeTool.Serialize<tblClasificacionProblemaEntity>(new tblClasificacionProblemaEntity()));


            CheckNotify();
            ViewBag.CustomScriptsDefault = BuildScriptsDefault("tblClasificacionProblema");
            return View(new StaticPagedList<tblClasificacionProblemaEntity>(listResult.ToList(), pageNumber, pSize, listResult.totalCount));
        }

        public ActionResult Details(int id = 0)
        {
            tblClasificacionProblemaEntity objtblClasificacionProblema = proxy.GettblClasificacionProblema(id);
            if (objtblClasificacionProblema == null)
            {
                return HttpNotFound();
            }
            return PartialView(objtblClasificacionProblema);
        }

        public ActionResult Create()
        {
            PermisosAccesoDeniedCreate("tblClasificacionProblema");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();

            return View();
        }

        [HttpPost]
        public ActionResult Create(tblClasificacionProblemaEntity objtblClasificacionProblema)
        {
            if (ModelState.IsValid)
            {

                objtblClasificacionProblema.BaseRemoteIp = RemoteIp;
                objtblClasificacionProblema.BaseIdUser = LoggedUserName;
                int result = proxy.AddtblClasificacionProblema(objtblClasificacionProblema);
                if (result == -1)
                {

                    AssingMessageScript("El tblClasificacionProblema ya existe en el sistema.", "error", "Error", true);
                    CheckNotify();
                    return View(objtblClasificacionProblema);
                }
                if (result > 0)
                {
                    AssingMessageScript("Se dio de alta el tblClasificacionProblema en el sistema.", "success", "Éxito", true);
                    return RedirectToAction("Index");
                }

            }
            return View(objtblClasificacionProblema);
        }

        public ActionResult GetClasficacionProblema(int IdPlaza, string idServicio)
        {
            List<ClasificacionProblemaEntity> lista = new List<ClasificacionProblemaEntity>();
            lista = proxyclass.GetClasificacionProblemaList().Where(o => o.TipServ == idServicio || o.TipServ == "Todos").ToList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClasficacionProblemas()
        {
            List<ClasificacionProblemaEntity> lista = new List<ClasificacionProblemaEntity>();
            lista = proxyclass.GetClasificacionProblemaList();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClasficacionSolucion(int IdPlaza, int ? idServicio)
        {
            
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            List<clasificacion_soluciones> lista = new List<clasificacion_soluciones>();
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(IdPlaza));
            try
            {
                conexionSQL.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec MUESTRATRABAJOSQUEJAS "+idServicio);
                comandoSql.Connection = conexionSQL;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        clasificacion_soluciones clasificacion = new clasificacion_soluciones();
                        clasificacion.CLV_TRABAJO = Int32.Parse(reader[0].ToString());
                        clasificacion.DESCRIPCION = reader[1].ToString();
                        lista.Add(clasificacion);
                    }
                }
            }
            catch { }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public class clasificacion_soluciones
        {
            public int CLV_TRABAJO { get; set; }
            public string DESCRIPCION { get; set; }

        }

        public ActionResult GetDepartamentoResponsable(int IdPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            List<departamento_responsable> lista = new List<departamento_responsable>();
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(IdPlaza));
            try
            {
                conexionSQL.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec MUESTRACLASIFICACIONQUEJAS");
                comandoSql.Connection = conexionSQL;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        departamento_responsable clasificacion = new departamento_responsable();
                        clasificacion.Clave = reader[0].ToString();
                        clasificacion.Concepto = reader[1].ToString();
                        lista.Add(clasificacion);
                    }
                }
            }
            catch { }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public class departamento_responsable
        {
            public string Clave { get; set; }
            public string Concepto { get; set; }

        }
        public ActionResult GetTurno(int IdPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            List<turnos> lista = new List<turnos>();
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(IdPlaza));
            try
            {
                conexionSQL.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("select * from tbl_Turnos");
                comandoSql.Connection = conexionSQL;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        turnos turno = new turnos();
                        turno.IdTurno = Int32.Parse(reader[0].ToString());
                        turno.Turno = reader[1].ToString();
                        lista.Add(turno);
                    }
                }
            }
            catch { }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public class turnos
        {
            public int IdTurno { get; set; }
            public string Turno { get; set; }

        }
        public ActionResult GetPrioridad(int IdPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            List<prioridad> lista = new List<prioridad>();
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(IdPlaza));
            try
            {
                conexionSQL.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("select * from tblPrioridadQueja");
                comandoSql.Connection = conexionSQL;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prioridad clasificacion = new prioridad();
                        clasificacion.clvPrioridadQueja = Int32.Parse(reader[0].ToString());
                        clasificacion.Descripcion = reader[1].ToString();
                        lista.Add(clasificacion);
                    }
                }
            }
            catch { }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public class prioridad
        {
            public int clvPrioridadQueja { get; set; }
            public string Descripcion { get; set; }

        }
        public ActionResult Edit(int id = 0)
        {
            PermisosAccesoDeniedEdit("tblClasificacionProblema");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
            tblClasificacionProblemaEntity objtblClasificacionProblema = proxy.GettblClasificacionProblema(id);

            if (objtblClasificacionProblema == null)
            {
                return HttpNotFound();
            }
            return View(objtblClasificacionProblema);
        }

        //
        // POST: /tblClasificacionProblema/Edit/5
        [HttpPost]
        public ActionResult Edit(tblClasificacionProblemaEntity objtblClasificacionProblema)
        {
            if (ModelState.IsValid)
            {
                objtblClasificacionProblema.BaseRemoteIp = RemoteIp;
                objtblClasificacionProblema.BaseIdUser = LoggedUserName;
                int result = proxy.UpdatetblClasificacionProblema(objtblClasificacionProblema);
                if (result == -1)
                {
                    tblClasificacionProblemaEntity objtblClasificacionProblemaOld = proxy.GettblClasificacionProblema(objtblClasificacionProblema.clvProblema);

                    AssingMessageScript("El tblClasificacionProblema ya existe en el sistema, .", "error", "Error", true);
                    CheckNotify();
                    return View(objtblClasificacionProblema);
                }
                if (result > 0)
                {
                    AssingMessageScript("El tblClasificacionProblema se modifico en el sistema.", "success", "Éxito", true);
                    CheckNotify();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(objtblClasificacionProblema);
        }

        public ActionResult QuickIndex(int? page, int? pageSize, String descripcion, bool? activo)
        {
            int pageNumber = (page ?? 1);
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            SoftvList<tblClasificacionProblemaEntity> listResult = null;
            List<tblClasificacionProblemaEntity> listtblClasificacionProblema = new List<tblClasificacionProblemaEntity>();
            tblClasificacionProblemaEntity objtblClasificacionProblema = new tblClasificacionProblemaEntity();
            tblClasificacionProblemaEntity objGettblClasificacionProblema = new tblClasificacionProblemaEntity();


            if ((descripcion != null && descripcion.ToString() != ""))
            {
                objtblClasificacionProblema.descripcion = descripcion;
            }

            if ((activo != null))
            {
                objtblClasificacionProblema.activo = activo;
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            listResult = proxy.GettblClasificacionProblemaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objtblClasificacionProblema));
            if (listResult.Count == 0)
            {
                int tempPageNumber = (int)(listResult.totalCount / pSize);
                pageNumber = (int)(listResult.totalCount / pSize) == 0 ? 1 : tempPageNumber;
                listResult = proxy.GettblClasificacionProblemaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objtblClasificacionProblema));
            }
            listResult.ToList().ForEach(x => listtblClasificacionProblema.Add(x));

            var tblClasificacionProblemaAsIPagedList = new StaticPagedList<tblClasificacionProblemaEntity>(listtblClasificacionProblema, pageNumber, pSize, listResult.totalCount);
            if (tblClasificacionProblemaAsIPagedList.Count > 0)
            {
                return PartialView(tblClasificacionProblemaAsIPagedList);
            }
            else
            {
                var result = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "No se encontraron registros con los criterios de búsqueda ingresados." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id = 0)
        {
            int result = proxy.DeletetblClasificacionProblema(RemoteIp, LoggedUserName, id);
            if (result > 0)
            {
                var resultOk = new { tipomsj = "success", titulomsj = "Aviso", Success = "True", Message = "Registro de tblClasificacionProblema Eliminado." };
                return Json(resultOk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultNg = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "El Registro de tblClasificacionProblema No puede ser Eliminado validar dependencias." };
                return Json(resultNg, JsonRequestBehavior.AllowGet);
            }
        }


    }

}

