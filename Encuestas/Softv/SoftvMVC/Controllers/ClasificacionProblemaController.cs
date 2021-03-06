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
    /// Class                   : SoftvMVC.Controllers.ClasificacionProblemaController.cs
    /// Generated by            : Class Generator (c) 2015
    /// Description             : ClasificacionProblemaController
    /// File                    : ClasificacionProblemaController.cs
    /// Creation date           : 28/07/2016
    /// Creation time           : 06:23 p. m.
    ///</summary>
    public partial class ClasificacionProblemaController : BaseController, IDisposable
    {
        private SoftvService.ClasificacionProblemaClient proxy = null;
        private SoftvService.ConexionClient proxycon = null;

        public ClasificacionProblemaController()
        {

            proxy = new SoftvService.ClasificacionProblemaClient();
            proxycon = new SoftvService.ConexionClient();

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
            //PermisosAcceso("ClasificacionProblema");
            //ViewData["Title"] = "ClasificacionProblema";
            //ViewData["Message"] = "ClasificacionProblema";
            //int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            //int pageNumber = (page ?? 1);
            //SoftvList<ClasificacionProblemaEntity> listResult = proxy.GetClasificacionProblemaPagedListXml(pageNumber, pSize, SerializeTool.Serialize<ClasificacionProblemaEntity>(new ClasificacionProblemaEntity()));


            //CheckNotify();
            //ViewBag.CustomScriptsDefault = BuildScriptsDefault("ClasificacionProblema");
            //return View(new StaticPagedList<ClasificacionProblemaEntity>(listResult.ToList(), pageNumber, pSize, listResult.totalCount));
            List<ClasificacionProblemaEntity> ClasificacionProblema = proxy.GetClasificacionProblemaList();
            return View();
        }

        public ActionResult Details(int id = 0)
        {
            ClasificacionProblemaEntity objClasificacionProblema = proxy.GetClasificacionProblema(id);
            if (objClasificacionProblema == null)
            {
                return HttpNotFound();
            }
            return PartialView(objClasificacionProblema);
        }

        public ActionResult Create()
        {
            PermisosAccesoDeniedCreate("ClasificacionProblema");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ClasificacionProblemaEntity objClasificacionProblema)
        {
            if (ModelState.IsValid)
            {

                objClasificacionProblema.BaseRemoteIp = RemoteIp;
                objClasificacionProblema.BaseIdUser = LoggedUserName;
                int result = proxy.AddClasificacionProblema(objClasificacionProblema);
                List<ConexionEntity> plazas = proxycon.GetConexionList();
                foreach (var item in plazas)
                {
                    string id_palza = item.IdConexion.ToString();
                    ConexionController c = new ConexionController();
                    SqlCommand comandoSql;
                    SqlConnection conexionSQL = new SqlConnection(c.DameConexion(int.Parse(id_palza)));
                    try
                    {
                        conexionSQL.Open();
                    }
                    catch{ }
                    try
                    {
                        int aux=0;
                        if (objClasificacionProblema.Activo == true)
                        {
                            aux = 1;
                        }
                        else
                        {
                            aux = 0;
                        }
                        comandoSql = new SqlCommand("INSERT INTO tblClasificacionProblemas(descripcion,activo) VALUES('" + objClasificacionProblema.Descripcion + "', " + aux+ ")");
                        comandoSql.Connection = conexionSQL;
                        SqlDataReader reader = comandoSql.ExecuteReader();
                    }
                    catch { }
                }
                if (result == -1)
                {

                    AssingMessageScript("El ClasificacionProblema ya existe en el sistema.", "error", "Error", true);
                    CheckNotify();
                    return View(objClasificacionProblema);
                }
                if (result > 0)
                {
                    AssingMessageScript("Se dio de alta el ClasificacionProblema en el sistema.", "success", "Éxito", true);
                    return RedirectToAction("Index");
                }

            }
            return View(objClasificacionProblema);
        }

        public ActionResult Edit(int id = 0)
        {
            PermisosAccesoDeniedEdit("ClasificacionProblema");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
            ClasificacionProblemaEntity objClasificacionProblema = proxy.GetClasificacionProblema(id);

            if (objClasificacionProblema == null)
            {
                return HttpNotFound();
            }
            return View(objClasificacionProblema);
        }

        //
        // POST: /ClasificacionProblema/Edit/5
        [HttpPost]
        public ActionResult Edit(ClasificacionProblemaEntity objClasificacionProblema)
        {
            if (ModelState.IsValid)
            {
                objClasificacionProblema.BaseRemoteIp = RemoteIp;
                objClasificacionProblema.BaseIdUser = LoggedUserName;
                ClasificacionProblemaEntity lista = proxy.GetClasificacionProblema(objClasificacionProblema.ClvProblema);
                List<ConexionEntity> plazas = proxycon.GetConexionList();
                foreach (var item in plazas)
                {
                    string id_palza = item.IdConexion.ToString();
                    ConexionController c = new ConexionController();
                    SqlCommand comandoSql;
                    SqlConnection conexionSQL = new SqlConnection(c.DameConexion(int.Parse(id_palza)));
                    try
                    {
                        conexionSQL.Open();
                    }
                    catch { }
                    try
                    {
                        int aux = 0;
                        if (objClasificacionProblema.Activo == true)
                        {
                            aux = 1;
                        }
                        else
                        {
                            aux = 0;
                        }
                        comandoSql = new SqlCommand("UPDATE tblClasificacionProblemas SET descripcion='" + objClasificacionProblema.Descripcion + "',activo='" + aux + "' WHERE descripcion= '" + lista.Descripcion + "'");
                        comandoSql.Connection = conexionSQL;
                        SqlDataReader reader = comandoSql.ExecuteReader();
                    }
                    catch { }
                }
                int result = proxy.UpdateClasificacionProblema(objClasificacionProblema);
                if (result == -1)
                {
                    ClasificacionProblemaEntity objClasificacionProblemaOld = proxy.GetClasificacionProblema(objClasificacionProblema.ClvProblema);

                    AssingMessageScript("El ClasificacionProblema ya existe en el sistema, .", "error", "Error", true);
                    CheckNotify();
                    return View(objClasificacionProblema);
                }
                if (result > 0)
                {
                    AssingMessageScript("El ClasificacionProblema se modifico en el sistema.", "success", "Éxito", true);
                    CheckNotify();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(objClasificacionProblema);
        }

        public ActionResult QuickIndex(int? page, int? pageSize, String Descripcion, bool? Activo)
        {
            int pageNumber = (page ?? 1);
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            SoftvList<ClasificacionProblemaEntity> listResult = null;
            List<ClasificacionProblemaEntity> listClasificacionProblema = new List<ClasificacionProblemaEntity>();
            ClasificacionProblemaEntity objClasificacionProblema = new ClasificacionProblemaEntity();
            ClasificacionProblemaEntity objGetClasificacionProblema = new ClasificacionProblemaEntity();


            if ((Descripcion != null && Descripcion.ToString() != ""))
            {
                objClasificacionProblema.Descripcion = Descripcion;
            }

            if ((Activo != null))
            {
                objClasificacionProblema.Activo = Activo;
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            listResult = proxy.GetClasificacionProblemaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objClasificacionProblema));
            if (listResult.Count == 0)
            {
                int tempPageNumber = (int)(listResult.totalCount / pSize);
                pageNumber = (int)(listResult.totalCount / pSize) == 0 ? 1 : tempPageNumber;
                listResult = proxy.GetClasificacionProblemaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objClasificacionProblema));
            }
            listResult.ToList().ForEach(x => listClasificacionProblema.Add(x));

            var ClasificacionProblemaAsIPagedList = new StaticPagedList<ClasificacionProblemaEntity>(listClasificacionProblema, pageNumber, pSize, listResult.totalCount);
            if (ClasificacionProblemaAsIPagedList.Count > 0)
            {
                return PartialView(ClasificacionProblemaAsIPagedList);
            }
            else
            {
                var result = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "No se encontraron registros con los criterios de búsqueda ingresados." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id = 0)
        {
            ClasificacionProblemaEntity lista = proxy.GetClasificacionProblema(id);
            List<ConexionEntity> plazas = proxycon.GetConexionList();
            foreach (var item in plazas)
            {
                string id_palza = item.IdConexion.ToString();
                ConexionController c = new ConexionController();
                SqlCommand comandoSql;
                SqlConnection conexionSQL = new SqlConnection(c.DameConexion(int.Parse(id_palza)));
                try
                {
                    conexionSQL.Open();
                }
                catch { }
                try
                {

                    comandoSql = new SqlCommand("DELETE FROM tblClasificacionProblemas WHERE descripcion= '" + lista.Descripcion + "'");
                    comandoSql.Connection = conexionSQL;
                    SqlDataReader reader = comandoSql.ExecuteReader();
                }
                catch { }
            }
            int result = proxy.DeleteClasificacionProblema(RemoteIp, LoggedUserName, id);
            if (result > 0)
            {
                var resultOk = new { tipomsj = "success", titulomsj = "Aviso", Success = "True", Message = "Registro de ClasificacionProblema Eliminado." };
                return Json(resultOk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultNg = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "El Registro de ClasificacionProblema No puede ser Eliminado validar dependencias." };
                return Json(resultNg, JsonRequestBehavior.AllowGet);
            }
        }







        public ActionResult GetList(string cadena, int draw, int start, int length)
        {
            DataTableData dataTableData = new DataTableData();
            dataTableData.draw = draw;
            dataTableData.recordsTotal = 0;
            int recordsFiltered = 0;
            if (cadena == null)
            {
                dataTableData.data = FiltrarContenido(ref recordsFiltered, start, length);
            }
            else if (cadena != null)
            {
                try
                {
                    dataTableData.data = FiltrarContenido(ref recordsFiltered, start, length).Where(o => o.ClvProblema == int.Parse(cadena)).ToList();

                }
                catch
                {
                    dataTableData.data = FiltrarContenido(ref recordsFiltered, start, length).Where(o => o.Descripcion.ToUpper().Contains(cadena.ToUpper())).ToList();
                }
            }

            dataTableData.recordsFiltered = recordsFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        private List<ClasificacionProblemaEntity> FiltrarContenido(ref int recordFiltered, int start, int length)
        {

            List<ClasificacionProblemaEntity> lista = proxy.GetClasificacionProblemaList();
            recordFiltered = lista.Count;
            int rango = start + length;
            return lista.Skip(start).Take(length).ToList();
        }

        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<ClasificacionProblemaEntity> data { get; set; }
        }

        public ActionResult getAllServicios()
        {
            var lista = proxycon.GetConexionList();
            List<TipServ> clasificacion = new List<TipServ>();
            foreach (var item in lista)
            {
                ConexionController c = new ConexionController();
                SqlCommand comandoSql;
                int id = Int32.Parse(item.IdConexion.ToString());
                SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(id));
                try
                {
                    conexionSQL2.Open();
                }
                catch
                { }
                try
                {
                    comandoSql = new SqlCommand("SELECT * FROM TipServ WHERE Habilitar = 0");
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader = comandoSql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TipServ datos = new TipServ();
                            datos.clvProblema = Int32.Parse(reader[0].ToString());
                            datos.descripcion = reader[1].ToString();
                            clasificacion.Add(datos);
                        }
                    }
                }
                catch { }
            }
            var listado = clasificacion.GroupBy(x => x.descripcion).Select(y => y.First());
            return Json(listado, JsonRequestBehavior.AllowGet);
        }

        public class TipServ
        {
            public int clvProblema { get; set; }
            public string descripcion { get; set; }
        }
    }

}

