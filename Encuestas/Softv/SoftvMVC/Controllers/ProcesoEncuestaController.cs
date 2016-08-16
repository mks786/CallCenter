﻿
using SoftvMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Softv.Entities;
using System.Data.SqlClient;
using Globals;

namespace SoftvMVC.Controllers
{
    /// <summary>
    /// Class                   : SoftvMVC.Controllers.ProcesoEncuestaController.cs
    /// Generated by            : Class Generator (c) 2015
    /// Description             : ProcesoEncuestaController
    /// File                    : ProcesoEncuestaController.cs
    /// Creation date           : 12/08/2016
    /// Creation time           : 12:44 p. m.
    ///</summary>
    public partial class ProcesoEncuestaController : BaseController, IDisposable
    {
        private SoftvService.ProcesoEncuestaClient proxy = null;
        private SoftvService.UsuarioClient proxyusuario = null;
        private SoftvService.UniversoEncuestaClient proxyuniverso = null;
        private SoftvService.EncuestaClient proxyencuesta = null;
        private SoftvService.ResOpcMultsClient Respuestas = null;
        private SoftvService.RelPreguntaOpcMultsClient relpregunta_resp = null;
        private SoftvService.RelEncuestaClientesClient relenc_clientes = null;
        private SoftvService.RelEnProcesosClient rel_en_proces = null;
        private SoftvService.RelPreguntaEncuestasClient rel_preg_encuesta = null;

        public ProcesoEncuestaController()
        {

            proxy = new SoftvService.ProcesoEncuestaClient();
            proxyusuario = new SoftvService.UsuarioClient();
            proxyuniverso = new SoftvService.UniversoEncuestaClient();
            proxyencuesta = new SoftvService.EncuestaClient();
            Respuestas = new SoftvService.ResOpcMultsClient();

            relpregunta_resp = new SoftvService.RelPreguntaOpcMultsClient();

            relenc_clientes = new SoftvService.RelEncuestaClientesClient();

            rel_en_proces = new SoftvService.RelEnProcesosClient();

            rel_preg_encuesta = new SoftvService.RelPreguntaEncuestasClient();

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
            PermisosAcceso("ProcesoEncuesta");
            ViewData["Title"] = "ProcesoEncuesta";
            ViewData["Message"] = "ProcesoEncuesta";
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            int pageNumber = (page ?? 1);
            SoftvList<ProcesoEncuestaEntity> listResult = proxy.GetProcesoEncuestaPagedListXml(pageNumber, pSize, SerializeTool.Serialize<ProcesoEncuestaEntity>(new ProcesoEncuestaEntity()));


            CheckNotify();
            ViewBag.CustomScriptsDefault = BuildScriptsDefault("ProcesoEncuesta");
            return View(new StaticPagedList<ProcesoEncuestaEntity>(listResult.ToList(), pageNumber, pSize, listResult.totalCount));
        }

        //public ActionResult Details(int id = 0)
        //{
        //    ProcesoEncuestaEntity objProcesoEncuesta = proxy.GetProcesoEncuesta(id);
        //    if (objProcesoEncuesta == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(objProcesoEncuesta);
        //}

        public ActionResult Create()
        {
            PermisosAccesoDeniedCreate("ProcesoEncuesta");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();

            return View();
        }

        [HttpPost]
        public ActionResult Create(obj_proceso universo)
        {
            if (ModelState.IsValid)
            {
                ProcesoEncuestaEntity objeto = new ProcesoEncuestaEntity();
                objeto.NombreProceso = universo.NombreProceso;
                objeto.TipSer = universo.TipSer;
                objeto.TipoBusqueda = universo.TipoBusqueda;
                objeto.TipoFecha = universo.tipo_fecha_nombre;
                objeto.FechaInicio = universo.fecha_creacion;
                objeto.Encuesta = universo.encuesta_nombre;
                objeto.StatusEncuesta = "Pendiente";
                objeto.IdEncuesta = universo.encuesta;
                UsuarioEntity user = proxyusuario.GetUsuario(universo.usuario);
                objeto.Usuario = user.Nombre;
                objeto.BaseRemoteIp = RemoteIp;
                objeto.BaseIdUser = LoggedUserName;
                objeto.Total = 0;
                int id_proceso = proxy.AddProcesoEncuesta(objeto);
                ConexionController c = new ConexionController();
                SqlCommand comandoSql;
                SqlConnection conexionSQL = new SqlConnection(c.DameConexion(universo.plaza));
                try
                {
                    conexionSQL.Open();
                }
                catch
                { }
                try
                {
                    id_tipoServicio tipo = new id_tipoServicio();
                    comandoSql = new SqlCommand("SELECT * from TipServ WHERE Concepto='"+universo.TipSer+"'");
                    comandoSql.Connection = conexionSQL;
                    SqlDataReader reader = comandoSql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tipo.id_tipoServ = Int32.Parse(reader[0].ToString());
                        }
                    }
                    reader.Close();
                    comandoSql = new SqlCommand("exec GetUniversoEncuesta " + universo.plaza + ", '" + universo.ciudad + "', " + tipo.id_tipoServ + ", " + universo.tipo_busqueda_id + ", '"+universo.desconectados+"', '"+universo.instalados+"', '"+universo.suspendidos+"', '"+universo.contratado+"', '"+universo.temporales+"', '"+universo.fuera_servicio+"', "+universo.tipo_fecha+", '"+universo.fecha_inicio+"', '"+universo.fecha_final+"'");
                    comandoSql.Connection = conexionSQL;
                    SqlDataReader reader2 = comandoSql.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            UniversoEncuestaEntity cliente = new UniversoEncuestaEntity();
                            cliente.IdProcesoEnc = id_proceso;
                            cliente.Contrato = Int32.Parse(reader2[0].ToString());
                            cliente.Nombre = reader2[1].ToString();
                            cliente.Tel = reader2[2].ToString();
                            cliente.Cel = reader2[3].ToString();
                            cliente.Aplicada = false;
                            cliente.IdPlaza = universo.plaza;
                            cliente.Ciudad = universo.ciudad;
                            int result = proxyuniverso.AddUniversoEncuesta(cliente);
                        }
                    }
                }
                catch { }
                int total_registros = proxyuniverso.GetUniversoEncuestaList().Where(o => o.IdProcesoEnc == id_proceso).Count();
                ProcesoEncuestaEntity aux = proxy.GetDeepProcesoEncuesta(id_proceso);
                aux.Total = total_registros;
                var editar = proxy.UpdateProcesoEncuesta(aux);

            }
            return Json(1,JsonRequestBehavior.AllowGet);
        }

        public class insertarUniverso
        {
            public int idProceso { get; set; }
            public int contrato { get; set; }
            public string nombre { get; set; }
            public string telefono { get; set; }
            public string celular { get; set; }
            public int aplicada { get; set; }
            public int plaza { get; set; }
            public string ciudad { get; set; }
        }

        public class id_tipoServicio
        {
            public int id_tipoServ { get; set; }
        }
        public class obj_proceso
        {
            public int plaza { get; set; }
            public string NombreProceso { get; set; }
            public string ciudad { get; set; }
            public int tipo_servicio { get; set; }
            public string TipSer { get; set; }
            public string TipoBusqueda { get; set; }
            public int encuesta { get; set; }
            public string contratado { get; set; }
            public string suspendidos { get; set; }
            public string temporales { get; set; }
            public string instalados { get; set; }
            public string desconectados { get; set; }
            public string fuera_servicio { get; set; }
            public string fecha_inicio { get; set; }
            public string fecha_final { get; set; }
            public int tipo_fecha { get; set; }
            public string tipo_fecha_nombre { get; set; }
            public int usuario { get; set; }
            public int tipo_busqueda_id { get; set; }
            public string fecha_creacion { get; set; }
            public string encuesta_nombre { get; set; }

        }

        public ActionResult Edit(int id = 0)
        {
            PermisosAccesoDeniedEdit("ProcesoEncuesta");
            ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
            ProcesoEncuestaEntity objProcesoEncuesta = proxy.GetProcesoEncuesta(id);

            if (objProcesoEncuesta == null)
            {
                return HttpNotFound();
            }
            return View(objProcesoEncuesta);
        }

        //
        // POST: /ProcesoEncuesta/Edit/5
        [HttpPost]
        public ActionResult Edit(ProcesoEncuestaEntity objProcesoEncuesta)
        {
            if (ModelState.IsValid)
            {
                objProcesoEncuesta.BaseRemoteIp = RemoteIp;
                objProcesoEncuesta.BaseIdUser = LoggedUserName;
                int result = proxy.UpdateProcesoEncuesta(objProcesoEncuesta);
                if (result == -1)
                {
                    ProcesoEncuestaEntity objProcesoEncuestaOld = proxy.GetProcesoEncuesta(objProcesoEncuesta.IdProcesoEnc);

                    AssingMessageScript("El ProcesoEncuesta ya existe en el sistema, .", "error", "Error", true);
                    CheckNotify();
                    return View(objProcesoEncuesta);
                }
                if (result > 0)
                {
                    AssingMessageScript("El ProcesoEncuesta se modifico en el sistema.", "success", "Éxito", true);
                    CheckNotify();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View(objProcesoEncuesta);
        }

        public ActionResult QuickIndex(int? page, int? pageSize, String NombreProceso, String TipSer, String TipoBusqueda, String StatusTipBusq, String TipoFecha, DateTime? FechaInicio, DateTime? FechaFin, String Encuesta, String StatusEncuesta, String Usuario)
        {
            int pageNumber = (page ?? 1);
            int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
            SoftvList<ProcesoEncuestaEntity> listResult = null;
            List<ProcesoEncuestaEntity> listProcesoEncuesta = new List<ProcesoEncuestaEntity>();
            ProcesoEncuestaEntity objProcesoEncuesta = new ProcesoEncuestaEntity();
            ProcesoEncuestaEntity objGetProcesoEncuesta = new ProcesoEncuestaEntity();


            if ((NombreProceso != null && NombreProceso.ToString() != ""))
            {
                objProcesoEncuesta.NombreProceso = NombreProceso;
            }

            if ((TipSer != null && TipSer.ToString() != ""))
            {
                objProcesoEncuesta.TipSer = TipSer;
            }

            if ((TipoBusqueda != null && TipoBusqueda.ToString() != ""))
            {
                objProcesoEncuesta.TipoBusqueda = TipoBusqueda;
            }

            if ((StatusTipBusq != null && StatusTipBusq.ToString() != ""))
            {
                objProcesoEncuesta.StatusTipBusq = StatusTipBusq;
            }

            if ((TipoFecha != null && TipoFecha.ToString() != ""))
            {
                objProcesoEncuesta.TipoFecha = TipoFecha;
            }

            if ((FechaInicio != null))
            {
                objProcesoEncuesta.FechaInicio = FechaInicio.ToString();;
            }

            if ((FechaFin != null))
            {
                objProcesoEncuesta.FechaFin = FechaFin.ToString();
            }

            if ((Encuesta != null && Encuesta.ToString() != ""))
            {
                objProcesoEncuesta.Encuesta = Encuesta;
            }

            if ((StatusEncuesta != null && StatusEncuesta.ToString() != ""))
            {
                objProcesoEncuesta.StatusEncuesta = StatusEncuesta;
            }

            if ((Usuario != null && Usuario.ToString() != ""))
            {
                objProcesoEncuesta.Usuario = Usuario;
            }

            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            listResult = proxy.GetProcesoEncuestaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objProcesoEncuesta));
            if (listResult.Count == 0)
            {
                int tempPageNumber = (int)(listResult.totalCount / pSize);
                pageNumber = (int)(listResult.totalCount / pSize) == 0 ? 1 : tempPageNumber;
                listResult = proxy.GetProcesoEncuestaPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objProcesoEncuesta));
            }
            listResult.ToList().ForEach(x => listProcesoEncuesta.Add(x));

            var ProcesoEncuestaAsIPagedList = new StaticPagedList<ProcesoEncuestaEntity>(listProcesoEncuesta, pageNumber, pSize, listResult.totalCount);
            if (ProcesoEncuestaAsIPagedList.Count > 0)
            {
                return PartialView(ProcesoEncuestaAsIPagedList);
            }
            else
            {
                var result = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "No se encontraron registros con los criterios de búsqueda ingresados." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(int id = 0)
        {
            int result = proxy.DeleteProcesoEncuesta(RemoteIp, LoggedUserName, id);
            if (result > 0)
            {
                var resultOk = new { tipomsj = "success", titulomsj = "Aviso", Success = "True", Message = "Registro de ProcesoEncuesta Eliminado." };
                return Json(resultOk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultNg = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "El Registro de ProcesoEncuesta No puede ser Eliminado validar dependencias." };
                return Json(resultNg, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Details(int id)
        {
            Detalle_encuesta Encuesta = new Detalle_encuesta();
            List<Detalle_pregunta> Lista_de_preguntas = new List<Detalle_pregunta>();
            ProcesoEncuestaEntity proceso = proxy.GetDeepProcesoEncuesta(id);



            EncuestaEntity objEncuesta = proxyencuesta.GetEncuesta(proceso.IdEncuesta);
            Encuesta.Encuesta = objEncuesta;
            List<RelPreguntaEncuestasEntity> lista_de_relaciones = rel_preg_encuesta.GetRelPreguntaEncuestasList().Where(x => x.IdEncuesta == objEncuesta.IdEncuesta).ToList();

            // foreach( var a in lista_de_relaciones.)

            //  List<PreguntaEntity> preguntas = preguntasService.GetPreguntaList().Where(o => o.RelPreguntaEncuestas.IdEncuesta == objEncuesta.IdEncuesta).ToList();
            // preguntas.Where(o=> o.RelPreguntaEncuestas.IdEncuesta==objEncuesta.IdEncuesta);
            foreach (var a in lista_de_relaciones)
            {

                Detalle_pregunta pregunta = new Detalle_pregunta();
                List<ResOpcMultsEntity> r = new List<ResOpcMultsEntity>();
                pregunta.Pregunta = a.Pregunta;

                List<RelPreguntaOpcMultsEntity> relaciones = relpregunta_resp.GetRelPreguntaOpcMultsList().Where(x => x.IdPregunta == a.IdPregunta).ToList();

                foreach (var resp in relaciones)
                {

                    ResOpcMultsEntity respuestas = Respuestas.GetResOpcMultsList().Where(o => o.Id_ResOpcMult == resp.Id_ResOpcMult).Select(o => o).First();

                    r.Add(respuestas);
                }


                pregunta.Respuestas = r;




                Lista_de_preguntas.Add(pregunta);



            }

            if (objEncuesta == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdEncuesta = Encuesta.Encuesta.IdEncuesta;
            ViewBag.NombreEncuesta = Encuesta.Encuesta.TituloEncuesta;
            ViewBag.Descripcion = Encuesta.Encuesta.Descripcion;
            ViewBag.FechaCreacion = Encuesta.Encuesta.FechaCreacion;
            ViewData["preguntas"] = Lista_de_preguntas;
            ViewData["id"] = id;
            ViewData["proceso"] = proceso;
            return View("PreView");
        }
        public class Detalle_encuesta
        {
            public EncuestaEntity Encuesta { get; set; }
            public List<Detalle_pregunta> Preguntas { get; set; }
        }

        public class Detalle_pregunta
        {
            public PreguntaEntity Pregunta { get; set; }
            public List<ResOpcMultsEntity> Respuestas { get; set; }

        }
        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<ProcesoEncuestaEntity> data { get; set; }
        }

        public ActionResult GetList(string cadena, int draw, int start, int length, int ? filtro)
        {
            List<ProcesoEncuestaEntity> lista = new List<ProcesoEncuestaEntity>();
            int total = 0;
            if(filtro == 1){
                lista = proxy.GetProcesoEncuestaList().Where(o => o.StatusEncuesta == "Pendiente").Skip(start).Take(length).OrderByDescending(o => o.IdProcesoEnc).ToList();
                total = proxy.GetProcesoEncuestaList().Where(o => o.StatusEncuesta == "Pendiente").Count();
            }else if(filtro ==2){
                lista = proxy.GetProcesoEncuestaList().Where(o => o.StatusEncuesta == "Terminada").Skip(start).Take(length).OrderByDescending(o => o.IdProcesoEnc).ToList();
                total = proxy.GetProcesoEncuestaList().Where(o => o.StatusEncuesta == "Terminada").Count();
            }
            else
            {
                if (cadena != "" && cadena != null)
                {

                    lista = proxy.GetProcesoEncuestaList().Where(o => o.NombreProceso.ToLower().Contains(cadena.ToLower()) || o.Encuesta.ToLower().Contains(cadena.ToLower())).Skip(start).Take(length).OrderByDescending(o => o.IdProcesoEnc).ToList();
                    total = proxy.GetProcesoEncuestaList().Where(o => o.NombreProceso.ToLower().Contains(cadena.ToLower()) || o.Encuesta.ToLower().Contains(cadena.ToLower())).Count();

                }
                else
                {
                    lista = proxy.GetProcesoEncuestaList().Skip(start).Take(length).OrderByDescending(o => o.IdProcesoEnc).ToList();
                    total = proxy.GetProcesoEncuestaList().Count();
                }
            }
            
            int recordFiltered = total;
            DataTableData dataTableData = new DataTableData();
            dataTableData.draw = draw;
            dataTableData.recordsTotal = 0;
            dataTableData.data = lista;
            dataTableData.recordsFiltered = recordFiltered;

            return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDataClient(int contrato, int plaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            List<CLIENTEEntity2> lista = new List<CLIENTEEntity2>();
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(plaza));
            try
            {
                conexionSQL.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("Select * from [dbo].[View_BusquedaIndividual] where Contrato=" + contrato);
                comandoSql.Connection = conexionSQL;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CLIENTEEntity2 cliente = new CLIENTEEntity2();
                        cliente.CONTRATO = Int32.Parse(reader[0].ToString());
                        cliente.NOMBRE = reader[1].ToString();
                        cliente.TELEFONO = reader[2].ToString();
                        cliente.CELULAR = reader[3].ToString();
                        cliente.Calle = reader[4].ToString();
                        cliente.Colonia = reader[6].ToString();
                        cliente.NUMERO = reader[5].ToString();
                        cliente.Ciudad = reader[7].ToString();
                        lista.Add(cliente);
                    }
                }
            }
            catch { }
            return Json(lista,JsonRequestBehavior.AllowGet);
        }
        public class CLIENTEEntity2
        {

            public long? CONTRATO { get; set; }

            public String NOMBRE { get; set; }

            public int? Clv_Calle { get; set; }

            public String NUMERO { get; set; }

            public String ENTRECALLES { get; set; }

            public int? Clv_Colonia { get; set; }

            public String CodigoPostal { get; set; }


            public String TELEFONO { get; set; }

            public String CELULAR { get; set; }

            public bool? DESGLOSA_Iva { get; set; }

            public bool? SoloInternet { get; set; }

            public bool? eshotel { get; set; }

            public int? Clv_Ciudad { get; set; }

            public String Email { get; set; }

            public int? clv_sector { get; set; }

            public int? Clv_Periodo { get; set; }

            public int? Clv_Tap { get; set; }

            public bool? Zona2 { get; set; }

            public int conexion { get; set; }

            public string Calle { get; set; }

            public string Colonia { get; set; }

            public string Ciudad { get; set; }

        }
        public ActionResult todosProcesos(int id_encuesta)
        {
            var lista = proxy.GetProcesoEncuestaList().Where(o => o.IdEncuesta == id_encuesta).ToList();
            return Json(lista,JsonRequestBehavior.AllowGet);
        }
    }

}

