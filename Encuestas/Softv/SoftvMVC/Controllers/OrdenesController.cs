using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace SoftvMVC.Controllers
{
    public class OrdenesController :  BaseController, IDisposable
    {
        // GET: Ordenes
        public ActionResult Index()
        {
            return View();
        }
        public class DataTableData
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<DatosOrden> data { get; set; }
        }

        public ActionResult getListOrders(int idPlaza, int draw, int start, int length, int ? conOrd, string nombre, string paterno, string materno, string status, string calle, string numero)
        {
            DataTableData dataTableData = new DataTableData();
            dataTableData.draw = draw;
            dataTableData.recordsTotal = 0;
            int recordFiltered = 0;

            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<DatosOrden> lista = new List<DatosOrden>();
            string sql = "";
            int bandera = 0;
            int tipo = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                if (conOrd != null) {
                    comandoSql = new SqlCommand("exec uspBuscaOrdSer 0, "+conOrd+", 0, '', '', '', 330, 0, 0");
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader = comandoSql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            DatosOrden datos = new DatosOrden();
                            datos.Clv_Orden = Convert.ToInt32(reader[0]);
                            datos.STATUS = reader[1].ToString();
                            datos.Contrato = Convert.ToInt32(reader[2]);
                            datos.Nombre = reader[3].ToString();
                            datos.CALLE = reader[4].ToString();
                            datos.NUMERO = reader[5].ToString();
                            datos.Clv_TipSer = Convert.ToInt32(reader[6]);
                            lista.Add(datos);
                        }
                    }
                    reader.Close();
                    comandoSql = new SqlCommand("exec uspBuscaOrdSer 0, 0, "+conOrd+", '', '', '', 30, 0, 0");
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader2 = comandoSql.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            DatosOrden datos = new DatosOrden();
                            datos.Clv_Orden = Convert.ToInt32(reader2[0]);
                            datos.STATUS = reader2[1].ToString();
                            datos.Contrato = Convert.ToInt32(reader2[2]);
                            datos.Nombre = reader2[3].ToString();
                            datos.CALLE = reader2[4].ToString();
                            datos.NUMERO = reader2[5].ToString();
                            datos.Clv_TipSer = Convert.ToInt32(reader2[6]);
                            lista.Add(datos);
                        }
                    }
                    reader.Close();
                }
                else
                {
                    if (nombre != null && nombre != "") {
                        sql = "exec BuscaOrdSerSeparado2 0, 0, 0, '"+nombre+"', '"+paterno+"', '"+materno+"', '', '', 31, 0, 0";
                        tipo = 1;
                    }
                    else if (status != null && status != "")
                    {
                        sql = "exec uspBuscaOrdSer 0, 0, 0, '"+status+"', '', '', 399, 0, 0";
                        tipo = 2;
                    }else if(calle != null && calle != "" && numero != null && numero != ""){
                        sql = "exec BuscaOrdSerSeparado2 0, 0, 0, '', '', '', '"+calle+"', '"+numero+"', 32, 0, 0";
                        tipo = 1;
                    }
                    else
                    {
                        sql = "exec BuscaOrdSerSeparado2 0,0,0,'','','','','',49,0,0";
                        bandera = 0;
                        tipo = 1;
                    }

                    comandoSql = new SqlCommand(sql);
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader = comandoSql.ExecuteReader();
                    if(bandera == 1){
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                DatosOrden datos = new DatosOrden();
                                datos.Clv_Orden = Convert.ToInt32(reader[0]);
                                datos.STATUS = reader[1].ToString();
                                datos.Contrato = Convert.ToInt32(reader[2]);
                                datos.Nombre = reader[3].ToString();
                                datos.CALLE = reader[4].ToString();
                                datos.NUMERO = reader[5].ToString();
                                datos.Clv_TipSer = Convert.ToInt32(reader[6]);
                                lista.Add(datos);
                            }
                        }
                    }
                    else
                    {
                        if(tipo == 1){
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DatosOrden datos = new DatosOrden();
                                    datos.Clv_Orden = Convert.ToInt32(reader[0]);
                                    datos.STATUS = reader[1].ToString();
                                    datos.Contrato = Convert.ToInt32(reader[2]);
                                    datos.Nombre = reader[3].ToString() + " " + reader[4].ToString() + " " + reader[5].ToString();
                                    datos.CALLE = reader[6].ToString();
                                    datos.NUMERO = reader[7].ToString();
                                    datos.Clv_TipSer = Convert.ToInt32(reader[8]);
                                    lista.Add(datos);
                                }
                            }
                        }
                        else
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    DatosOrden datos = new DatosOrden();
                                    datos.Clv_Orden = Convert.ToInt32(reader[0]);
                                    datos.STATUS = reader[1].ToString();
                                    datos.Contrato = Convert.ToInt32(reader[2]);
                                    datos.Nombre = reader[3].ToString();
                                    datos.CALLE = reader[4].ToString();
                                    datos.NUMERO = reader[5].ToString();
                                    datos.Clv_TipSer = Convert.ToInt32(reader[6]);
                                    lista.Add(datos);
                                }
                            }
                        }
                        
                    }

                    
                    reader.Close();
                }
                

               
            }
            catch
            { }

                dataTableData.data = lista.Skip(start).Take(length).ToList();
                recordFiltered = lista.Count;
            
            
                dataTableData.recordsFiltered = recordFiltered;
                return Json(dataTableData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCounters(int idPlaza)
        {
             ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objCounter data = new objCounter();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec uspChecaCuantasOrdenesQuejas 2");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        data.pendientes = Convert.ToInt32(reader2[0]);
                        data.proceso = Convert.ToInt32(reader2[2]);
                    }
                }
            }
            catch
            {

            }
            return Json(data,JsonRequestBehavior.AllowGet);
        }
        public class objCounter{
            public int pendientes { get; set; }
            public int proceso { get; set; }
        }
        public class DatosOrden
        {
            public int Clv_Orden { get; set; }
            public string STATUS { get; set; }
            public int Contrato { get; set; }
            public string Nombre { get; set; }
            public string CALLE { get; set; }
            public string NUMERO { get; set; }
            public int Clv_TipSer { get; set; }
        }

        public ActionResult getDetailOrder(int idPlaza, int Contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objDetail datos = new objDetail();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec uspBuscaOrdSer 1, "+Contrato+", 0, '', '', '', 330, 0, 0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        datos.Clv_Orden = Convert.ToInt32(reader[0]);
                        datos.Status = reader[1].ToString();
                        datos.Contrato = Convert.ToInt32(reader[2]);
                        datos.Nombre = reader[3].ToString();
                        datos.Calle = reader[4].ToString();
                        datos.Numero = reader[5].ToString();
                    }
                }
                reader.Close();
                comandoSql = new SqlCommand("exec Dame_DetOrdSer " + Contrato );
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                List<detalleDescripcion> aux = new List<detalleDescripcion>();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        detalleDescripcion des = new detalleDescripcion();
                        des.Descripcion = reader2[0].ToString();
                        aux.Add(des);
                    }
                }
                datos.listDescripcion = aux;
            }
            catch
            { }
            return Json(datos,JsonRequestBehavior.AllowGet);
        }

        public class objDetail
        {
            public int Contrato { get; set; }
            public int Clv_Orden { get; set; }
            public string Status {get;set;}
            public string Nombre { get; set; }
            public string Calle { get; set; }
            public string Numero { get; set; }
            public List<detalleDescripcion> listDescripcion { get; set; }
        }

        public class detalleDescripcion
        {
            public string Descripcion { get; set; }
        }

        public ActionResult getDatosClientes(int idPlaza, int contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objCliente datos = new objCliente();
            List<objServicios> servicios = new List<objServicios>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec BUSCLIPORCONTRATO "+contrato+", '', '','','', 0,0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        datos.Contrato = Convert.ToInt32(reader[0]);
                        datos.Nombre = reader[1].ToString();
                        datos.Calle = reader[2].ToString();
                        datos.Colonia = reader[3].ToString();
                        datos.Numero = reader[4].ToString();
                        datos.Ciudad = reader[5].ToString();
                        datos.Internet = Convert.ToInt32(reader[6]);
                        datos.Hotel = Convert.ToInt32(reader[7]);
                    }
                }
                reader.Close();
                comandoSql = new SqlCommand("exec dameSerDELCli " + contrato);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        objServicios servicio = new objServicios();
                        servicio.servicio = reader2[0].ToString();
                        servicios.Add(servicio);
                    }
                }
                reader.Close();
            }
            catch
            { }
            var result = new { datos = datos, servicios = servicios };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public class objCliente
        {
            public int Contrato { get; set; }
            public string Nombre { get; set; }
            public string Calle { get; set; }
            public string Numero { get; set; }
            public string Colonia { get; set; }
            public string Ciudad { get; set; }
            public int Hotel { get; set; }
            public int Internet { get; set; }
        }
        public class objServicios
        {
            public string servicio { get; set; }
        }

        public ActionResult getDataTecnicos(int idPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objTecnico> tecnicos = new List<objTecnico>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec Muestra_TecnicosByFamili 1, 1");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objTecnico tecnico = new objTecnico();
                        tecnico.clvTecnico = Convert.ToInt32(reader[0]);
                        tecnico.Nombre = reader[1].ToString();
                        tecnicos.Add(tecnico);

                    }
                }
            }
            catch { }
            return Json(tecnicos,JsonRequestBehavior.AllowGet);
        }

        public class objTecnico
        {
            public int clvTecnico { get; set; }
            public string Nombre { get; set; }
        }

        public ActionResult getServiciosContratados(int idPlaza, int contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objServCliente> servicios = new List<objServCliente>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec Dime_Que_servicio_Tiene_cliente "+contrato);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objServCliente servicio = new objServCliente();
                        servicio.clv_servicio = Convert.ToInt32(reader[0]);
                        servicio.servicio = reader[1].ToString();
                        servicios.Add(servicio);

                    }
                }
            }
            catch { }
            return Json(servicios, JsonRequestBehavior.AllowGet);
        }
        public class objServCliente
        {
            public int clv_servicio { get; set; }
            public string servicio { get; set; }
        }

        public ActionResult getTrabajos(int idPlaza, int Servicio)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objTrabajo> trabajos = new List<objTrabajo>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec MUESTRATRABAJOS " + Servicio);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objTrabajo trabajo = new objTrabajo();
                        trabajo.clv_trabajo = Convert.ToInt32(reader[0]);
                        trabajo.descripcion = reader[1].ToString();
                        trabajos.Add(trabajo);

                    }
                }
            }
            catch { }
            return Json(trabajos, JsonRequestBehavior.AllowGet);
        }
        public class objTrabajo
        {
            public int clv_trabajo { get; set; }
            public string descripcion { get; set; }
        }
        public ActionResult getServicioActivo(int idPlaza, int Servicio, int Contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int activo = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("declare @a int, @b int, @c int exec uspContratoServ "+Contrato+", "+Servicio+", @a OUTPUT select @a");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        activo = Convert.ToInt32(reader[0]);

                    }
                }
            }
            catch { }
            return Json(activo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult preGuardado(int idPlaza, int Contrato, string Observaciones)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int clv_orden = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("declare @OS bigint exec NUEORDSER 0, "+Contrato+",'', '', '', '', '', 0, 0, 0, '"+Observaciones+"', '', @OS OUTPUT select @OS");
                comandoSql.Connection = conexionSQL2;
                clv_orden = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(clv_orden, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllOrders(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objAllOrders> ordenes = new List<objAllOrders>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec BUSCADetOrdSer " + Orden);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objAllOrders orden = new objAllOrders();
                        orden.clave = Convert.ToInt32(reader[0]);
                        orden.clv_orden = Convert.ToInt32(reader[1]);
                        orden.clv_trabajo = Convert.ToInt32(reader[2]);
                        orden.descripcion = reader[3].ToString();
                        orden.accion = reader[4].ToString();
                        orden.observaciones = reader[5].ToString();
                        orden.realiza = reader[6].ToString();
                        ordenes.Add(orden);
                    }
                }
            }
            catch { }
            return Json(ordenes, JsonRequestBehavior.AllowGet);
        }
        public class objAllOrders{
            public int clave { get; set; }
            public int clv_orden { get; set; }
            public int clv_trabajo { get; set; }
            public string descripcion { get; set; }
            public string accion { get; set; }
            public string observaciones { get; set; }
            public string realiza { get; set; }
        }


        public ActionResult guardarDetalleOrden(int idPlaza, int Orden, int Trabajo,string Observaciones, int Realiza, int Servicio)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("declare @Id INT exec AgregaDetalleOS " + Orden + ", " + Trabajo + ",'" + Observaciones + "', " + Realiza + ", " + Servicio + ",  @Id OUTPUT select @Id");
                comandoSql.Connection = conexionSQL2;
                result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult deleteDetailOrder(int idPlaza, int Clave)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec BORDetOrdSer " +Clave);
                comandoSql.Connection = conexionSQL2;
                result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveOrder(int idPlaza, string Obse, int Orden, int Usuario)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec GuardaOS  '"+Obse+"', "+Orden+", "+Usuario+",'P'");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteReader();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult cancelOrder(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec Eliminar_Orden "+Orden);
                comandoSql.Connection = conexionSQL2;
                result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult saveCambioDomicilio(int idPlaza, int Clave, int Orden, int Contrato, int Ciudad, int Colonia, int Calle, string Numero, int ? Telefono, string entreCalles, string NumInt){
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                if (Telefono == null)
                {
                    Telefono = 0;
                }

                comandoSql = new SqlCommand("exec NUECAMDONoInt " + Clave+","+Orden+","+Contrato+","+Calle+",'"+Numero+"','"+NumInt+"','"+entreCalles+"',"+Colonia+","+Telefono+",0,"+Ciudad);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteReader();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getExtensiones(int idPlaza, int Contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec DameExteciones_Cli " + Contrato);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelExtensiones(int idPlaza, int Clave, int Orden, int Contrato, int Extensiones)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec NUECANEX " + Clave+","+Orden+","+Contrato+","+Extensiones);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteReader();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult saveExtensiones(int idPlaza, int Clave, int Orden, int Contrato, int Extensiones)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("delete from CONEX where Clv_Orden = "+Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();

                comandoSql = new SqlCommand("exec NUECONEX " + Clave + "," + Orden + "," + Contrato + "," + Extensiones);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getCablemodem(int idPlaza, int Contrato, int Orden, int Clave)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objCablemodem> cablemondems = new List<objCablemodem>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec MUESTRACABLEMODEMSDELCLI_porOpcion " + Contrato+",'P',14");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objCablemodem cablemodem = new objCablemodem();
                        cablemodem.ContratoNet = Convert.ToInt32(reader[0]);
                        cablemodem.Mac = reader[1].ToString();
                        cablemondems.Add(cablemodem);
                    }
                }
                reader.Close();

                foreach (var item in cablemondems)
                {
                    comandoSql = new SqlCommand("exec MUESTRACONTNET_PorOpcion " + item.ContratoNet + ", '', 10, " + Orden + ", " + Clave + "");
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader2 = comandoSql.ExecuteReader();
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            item.clv_unicanet = Convert.ToInt32(reader2[0]);
                            item.Detalle = reader2[1].ToString() + ": " + reader2[2].ToString();

                        }
                    }
                } 

            }
            catch { }
            return Json(cablemondems, JsonRequestBehavior.AllowGet);
        }

        public class objCablemodem
        {
            public int ContratoNet { get; set; }
            public string Mac { get; set; }
            public int clv_unicanet { get; set; }
            public string Detalle { get; set; }
        }

        public ActionResult bajaPaquete(objCancelar Objeto)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(Objeto.idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                foreach (var item in Objeto.Macs)
                {
                    comandoSql = new SqlCommand("exec NUEIPAQU_SOL "+Objeto.Clave+", "+Objeto.Orden+", "+item.ContratoNet+", 0, 3, '"+Objeto.Status+"'");
                    comandoSql.Connection = conexionSQL2;
                    comandoSql.ExecuteNonQuery();
                    if(Objeto.Status == "B"){
                        comandoSql = new SqlCommand("exec GuardaMotivoCanServ " + Objeto.Orden + ", 2, " + item.ContratoNet + ", 0, 0");
                        comandoSql.Connection = conexionSQL2;
                        comandoSql.ExecuteNonQuery();
                    }
                    
                }
                
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public class objCancelar{
            public List<objCablemodem> Macs { get; set; }
            public int idPlaza { get; set; }
            public int Clave { get; set; }
            public int Orden { get; set; }
            public string Status { get; set; }
        }

        public ActionResult consultarDetalleOrden(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objDetalleOrden orden = new objDetalleOrden();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec CONORDSER "+Orden+", 0 ");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orden.clv_orden = Convert.ToInt32(reader[0]);
                        orden.contrato = Convert.ToInt32(reader[2]);
                        orden.solicitud = reader[3].ToString();
                        orden.ejecucion = reader[4].ToString();
                        orden.visita1 = reader[5].ToString();
                        orden.visita2 = reader[6].ToString();
                        orden.status = reader[7].ToString();
                        orden.observaciones = reader[11].ToString();
                    }
                }
                reader.Close();

                comandoSql = new SqlCommand("exec BUSCLIPORCONTRATO "+orden.contrato+", '', '', '', '', 0, 0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader0 = comandoSql.ExecuteReader();
                if (reader0.HasRows)
                {
                    while (reader0.Read())
                    {
                        orden.nombre = reader0[1].ToString();
                        orden.calle = reader0[2].ToString();
                        orden.colonia = reader0[3].ToString();
                        orden.numero = reader0[4].ToString();
                        orden.ciudad = reader0[5].ToString();
                    }
                }
                reader0.Close();

                comandoSql = new SqlCommand("exec dameSerDELCli " + orden.contrato + "");
                comandoSql.Connection = conexionSQL2;
                List<objServicios> servicios = new List<objServicios>();
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        objServicios servicio = new objServicios();
                        servicio.servicio = reader2[0].ToString();
                        servicios.Add(servicio);
                    }
                }
                reader2.Close();
                orden.servicios = servicios;


                comandoSql = new SqlCommand("exec MuestraRelOrdenesTecnicos " + Orden+ "");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader3 = comandoSql.ExecuteReader();
                objTecnico tecnico = new objTecnico();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        
                        tecnico.clvTecnico = Convert.ToInt32(reader3[0]);
                        tecnico.Nombre = reader3[1].ToString();
                    }
                }
                reader3.Close();
                orden.tecnico = tecnico;


                comandoSql = new SqlCommand("declare @a int, @b varchar(50) exec uspDamePlacaTapCliente "+orden.contrato+", @a OUTPUT, @b OUTPUT  select @b");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader4 = comandoSql.ExecuteReader();
                if (reader4.HasRows)
                {
                    while (reader4.Read())
                    {
                        try
                        {
                            orden.placa = Convert.ToInt32(reader4[0]);
                        }
                        catch
                        {
                            orden.placa = 0;
                        }
                    }
                }
                reader4.Close();

                comandoSql = new SqlCommand("exec uspConsultaTap "+orden.contrato+", 0, 0 ");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader5 = comandoSql.ExecuteReader();
                objTap tap = new objTap();
                if (reader5.HasRows)
                {
                    while (reader5.Read())
                    {

                        tap.idTap = Convert.ToInt32(reader5[0]);
                        tap.clave = reader5[1].ToString();
                    }
                }
                reader5.Close();
                orden.tap = tap;

                comandoSql = new SqlCommand("exec Consulta_RelOrdenUsuario "+Orden+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader6 = comandoSql.ExecuteReader();
                if (reader6.HasRows)
                {
                    while (reader6.Read())
                    {

                        orden.genero = reader6[0].ToString(); ;
                        orden.ejecuto = reader6[1].ToString();
                    }
                }
                reader6.Close();

                comandoSql = new SqlCommand("exec BUSCADetOrdSer "+Orden+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader7 = comandoSql.ExecuteReader();
                List<objAllOrders> detallesOrdenes = new List<objAllOrders>();
                if (reader7.HasRows)
                {
                    while (reader7.Read())
                    {
                        objAllOrders detalleOrden = new objAllOrders();
                        detalleOrden.clave = Convert.ToInt32(reader7[0]);
                        detalleOrden.clv_orden = Convert.ToInt32(reader7[1]);
                        detalleOrden.clv_trabajo = Convert.ToInt32(reader7[2]);
                        detalleOrden.descripcion = reader7[3].ToString();
                        detalleOrden.accion = reader7[4].ToString();
                        detalleOrden.observaciones = reader7[5].ToString();
                        detalleOrden.realiza = reader7[6].ToString();
                        detallesOrdenes.Add(detalleOrden);
                    }
                }
                reader7.Close();
                orden.detallesOrdenes = detallesOrdenes;


                comandoSql = new SqlCommand("exec DimeSiTieneunaBitacora " + Orden + "");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader8 = comandoSql.ExecuteReader();
                if (reader8.HasRows)
                {
                    while (reader8.Read())
                    {

                        orden.folio = reader8[0].ToString();
                    }
                }
                reader8.Close();

            }
            catch { }
            return Json(orden, JsonRequestBehavior.AllowGet);
        }

        public class objDetalleOrden
        {
            public int clv_orden { get; set; }
            public int contrato { get; set; }
            public string folio { get; set; }
            public string nombre { get; set; }
            public string ciudad { get; set; }
            public string colonia { get; set; }
            public string calle { get; set; }
            public string numero { get; set; }
            public string solicitud { get; set; }
            public string ejecucion { get; set; }
            public string visita1 { get; set; }
            public string visita2 { get; set; }
            public string status { get; set; }
            public string observaciones { get; set; }
            public List<objServicios> servicios { get; set; }
            public objTecnico tecnico { get; set; }
            public int placa { get; set; }
            public objTap tap { get; set; }
            public string ejecuto { get; set; }
            public string genero { get; set; }
            public List<objAllOrders> detallesOrdenes { get; set; }
        }
        public class objTap
        {
            public int idTap { get; set; }
            public string clave { get; set; }
        }

        public ActionResult detalleCamdo(int idPlaza, int Clave, int Contrato, int Orden)
        {
             ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            onjCamdo cambio = new onjCamdo();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec CONCAMDO "+Clave+", "+Orden+", "+Contrato+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cambio.clv_calle = Convert.ToInt32(reader[3]);
                        cambio.clv_ciudad = Convert.ToInt32(reader[9]);
                        cambio.clv_colonia = Convert.ToInt32(reader[6]);
                        cambio.numero = reader[4].ToString();
                        cambio.entreCalles = reader[5].ToString();
                        cambio.telefono = reader[7].ToString();
                    }
                }
                reader.Close();

                comandoSql = new SqlCommand("SELECT * FROM CIUDADES WHERE Clv_Ciudad="+cambio.clv_ciudad);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        cambio.ciudad = reader2[1].ToString();
                    }
                }
                reader2.Close();

                comandoSql = new SqlCommand("select *  from COLONIAS WHERE clv_colonia=" + cambio.clv_colonia);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader3 = comandoSql.ExecuteReader();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        cambio.colonia = reader3[5].ToString();
                    }
                }
                reader3.Close();

                comandoSql = new SqlCommand("select *  from CALLES WHERE Clv_Calle=" + cambio.clv_calle);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader4 = comandoSql.ExecuteReader();
                if (reader4.HasRows)
                {
                    while (reader4.Read())
                    {
                        cambio.calle = reader4[1].ToString();
                    }
                }
                reader4.Close();
            }
            catch { }
            return Json(cambio,JsonRequestBehavior.AllowGet); ;
        }
        public class onjCamdo
        {
            public int clv_calle { get; set; }
            public string numero { get; set; }
            public int clv_colonia { get; set; }
            public string telefono { get; set; }
            public int clv_ciudad { get; set; }
            public string entreCalles { get; set; }
            public string ciudad { get; set; }
            public string colonia { get; set; }
            public string calle { get; set; }
        }

        public ActionResult detalleConet(int idPlaza, int Clave,int Orden, int Contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objDetalleConet extra = new objDetalleConet();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec CONCONEX " + Clave + ", " + Orden + ", " + Contrato + "");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        extra.extra = Convert.ToInt32(reader[3]);
                    }
                }
                reader.Close();
            }
            catch { }
            return Json(extra, JsonRequestBehavior.AllowGet); ;
        }
        public class objDetalleConet
        {
            public int extra { get; set; }
        }

        public ActionResult tieneCanexConex(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int conex = 0;
            int canex = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec Dimesihay_Conex "+Orden+", 0");
                comandoSql.Connection = conexionSQL2;
                conex = Int32.Parse(comandoSql.ExecuteScalar().ToString());

                comandoSql = new SqlCommand("exec Dimesihay_Conex " + Orden + ", 1");
                comandoSql.Connection = conexionSQL2;
                
                canex = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            var result = new { conex = conex, canex = canex };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult motivosCancelacion(int idPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objMotivos> motivos = new List<objMotivos>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec ConMotCan");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objMotivos motivo = new objMotivos();
                        motivo.clv_motivo = Convert.ToInt32(reader[0]);
                        motivo.motivo = reader[1].ToString();
                        motivos.Add(motivo);
                    }
                }
                reader.Close();
            }
            catch { }
            return Json(motivos, JsonRequestBehavior.AllowGet);
        }

        public class objMotivos
        {
           public int clv_motivo { get; set; }
           public string motivo { get; set; }
        }

        public ActionResult guardarMotivo(int idPlaza, int Orden, int Motivo)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec InsertMotCanServ "+Orden+","+Motivo+"");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            var result = 1;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getBitacoraDescarga(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objDescarga objeto = new objDescarga();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec DimeSiTieneunaBitacora "+Orden+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            objeto.bitacora = Convert.ToInt32(reader[0]);
                        }
                        catch { objeto.bitacora = 0; }
                        
                    }
                }
                reader.Close();

                comandoSql = new SqlCommand("select * from AlmacenWebDB.dbo.tbl_AlmacenEmpresa where Activo=1");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader2 = comandoSql.ExecuteReader();
                List<objAlamcen> almacenes = new List<objAlamcen>();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        objAlamcen almacen = new objAlamcen();
                        almacen.id = Convert.ToInt32(reader2[0]);
                        almacen.descripcion = reader2[1].ToString();
                        almacenes.Add(almacen);

                    }
                }
                reader2.Close();
                objeto.almacenes = almacenes;

                comandoSql = new SqlCommand("exec Muestra_Detalle_Bitacora 0 ");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader3 = comandoSql.ExecuteReader();
                List<objClasificacionMaterial> clasificaciones = new List<objClasificacionMaterial>();
                if (reader3.HasRows)
                {
                    while (reader3.Read())
                    {
                        objClasificacionMaterial clasificacion = new objClasificacionMaterial();
                        clasificacion.clv_tipo = Convert.ToInt32(reader3[2]);
                        clasificacion.clv_categoria = Convert.ToInt32(reader3[1]);
                        clasificacion.concepto = reader3[3].ToString();
                        clasificaciones.Add(clasificacion);

                    }
                }
                reader3.Close();
                objeto.clasificaciones = clasificaciones;
            }
            catch { }

            return Json(objeto, JsonRequestBehavior.AllowGet);
        }
 

        public class objDescarga{
            public int bitacora { get; set; }
            public List<objAlamcen> almacenes { get; set; }
            public List<objClasificacionMaterial> clasificaciones { get; set; }
        }
        public class objAlamcen
        {
            public int id { get; set; }
            public string descripcion { get; set; }
        }
        public class objClasificacionMaterial
        {
            public int clv_tipo { get; set; }
            public int clv_categoria { get; set; }
            public string concepto { get; set; }
        }

        public ActionResult getArticulosDescarga(int idPlaza,int Tecnico, int Clasificacion)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objArticuloDescarga> articulos = new List<objArticuloDescarga>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Muestra_Descripcion_Articulo "+Tecnico+", "+Clasificacion+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objArticuloDescarga articulo = new objArticuloDescarga();
                        articulo.id = Convert.ToInt32(reader[0]);
                        articulo.clave = Convert.ToInt32(reader[1]);
                        articulo.noArticulo = reader[2].ToString();
                        articulo.articulo = reader[3].ToString();
                        articulos.Add(articulo);
                    }
                }
                reader.Close();
            }
            catch { }
            return Json(articulos, JsonRequestBehavior.AllowGet); ;
        }

        public class objArticuloDescarga
        {
            public int clave { get; set; }
            public string noArticulo { get; set; }
            public string articulo { get; set; }
            public int id { get; set; }
        }

        public ActionResult guardarMaterial(int idPlaza, int Orden, string Almacen, string ClaveArticulo, string Articulo, string Tecnico, int cantidad, int Mii, int Mfi, int Mei, int Mef, int Session)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec AddTempDetalleMaterial " + Orden + ", '" + Almacen + "', '" +ClaveArticulo+ "','"+Articulo+"','"+Tecnico+"',"+cantidad+","+Mii+","+Mfi+","+Mei+","+Mef+","+Session);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }
        public ActionResult consultarExistencia(int idPlaza, int Tecnico, int Articulo, int Cantidad)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec ValidaExistenciasTecnicos " + Tecnico + ", " + Articulo + ", " + Cantidad);
                comandoSql.Connection = conexionSQL2;
                result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult getSession(int idPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("declare @a bigint exec DameClv_Session_Tecnicos @a OUTPUT select @a");
                comandoSql.Connection = conexionSQL2;
                result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }
 
        public ActionResult detalleArticulosTabla(int idPlaza, int Orden, int Session)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objDetalleArticulo> articulos = new List<objDetalleArticulo>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Consult_TempDetalleMaterial " + Orden+","+Session);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDetalleArticulo articulo = new objDetalleArticulo();
                        articulo.id = Convert.ToInt32(reader[0]);
                        articulo.orden = Convert.ToInt32(reader[1]);
                        articulo.almacen = reader[2].ToString();
                        articulo.clave = reader[3].ToString();
                        articulo.descripcion = reader[4].ToString();
                        articulo.tecnico = reader[5].ToString();
                        try
                        {
                            articulo.cantidad = Convert.ToInt32(reader[6]);
                        }
                        catch { articulo.cantidad = 0; }
                        
                        try
                        {
                            articulo.mii = Convert.ToInt32(reader[7]);
                        }
                        catch { articulo.mii = 0; }
                        try
                        {
                            articulo.mfi = Convert.ToInt32(reader[8]);
                        }
                        catch { articulo.mfi = 0; }
                        try
                        {
                            articulo.mie = Convert.ToInt32(reader[9]);
                        }
                        catch { articulo.mie = 0; }
                        try
                        {
                            articulo.mfe = Convert.ToInt32(reader[10]);
                        }
                        catch { articulo.mfe = 0; }
                        
                        articulos.Add(articulo);
                    }
                }
                reader.Close();
            }
            catch { }
            return Json(articulos, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult consultarArticulosTabla(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objConsultarMaterial objeto = new objConsultarMaterial();
            List<objDetalleArticulo> articulos = new List<objDetalleArticulo>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec ConsultaDescargaMaterialGeneral " + Orden);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objeto.almacen = reader[0].ToString();
                        objeto.categoria = reader[1].ToString();
                        objeto.articulo = reader[3].ToString();
                        objeto.bitacora = reader[4].ToString();
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            objDetalleArticulo articulo = new objDetalleArticulo();
                            articulo.orden = Convert.ToInt32(reader[0]);
                            articulo.clave = reader[1].ToString();
                            articulo.descripcion = reader[2].ToString();
                            articulo.tecnico = reader[4].ToString();
                            try
                            {
                                articulo.cantidad = Convert.ToInt32(reader[3]);
                            }
                            catch { articulo.cantidad = 0; }

                            try
                            {
                                articulo.mii = Convert.ToInt32(reader[5]);
                            }
                            catch { articulo.mii = 0; }
                            try
                            {
                                articulo.mfi = Convert.ToInt32(reader[6]);
                            }
                            catch { articulo.mfi = 0; }
                            try
                            {
                                articulo.mie = Convert.ToInt32(reader[7]);
                            }
                            catch { articulo.mie = 0; }
                            try
                            {
                                articulo.mfe = Convert.ToInt32(reader[8]);
                            }
                            catch { articulo.mfe = 0; }

                            articulos.Add(articulo);
                        }
                    }
                    
                }
                reader.Close();
                objeto.articulos = articulos;
            }
            catch { }
            return Json(objeto, JsonRequestBehavior.AllowGet); ;
        }

        public class objConsultarMaterial
        {
            public List<objDetalleArticulo> articulos { get; set; }
            public string almacen { get; set; }
            public string categoria { get; set; }
            public string articulo { get; set; }
            public string bitacora { get; set; }
        }

        public class objDetalleArticulo
        {
            public int id { get; set; }
            public string clave { get; set; }
            public int orden { get; set; }
            public string almacen { get; set; }
            public string descripcion { get; set; }
            public int cantidad { get; set; }
            public string tecnico { get; set; }
            public int mii { get; set; }
            public int mfi { get; set; }
            public int mie { get; set; }
            public int mfe{get;set;}
        }

        public ActionResult eliminarMaterial(int idPlaza, int ID)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Delete_TempDetalleMaterial " + ID );
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult eliminarTodosArticulos(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {                 
                comandoSql = new SqlCommand("exec softv_BorraDescarga  " + Orden+",'O'");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();

                comandoSql = new SqlCommand("exec Eliminar_DetalleDescarga  " + Orden );
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();


                //comandoSql = new SqlCommand("declare @i int exec SP_CancelarBitacora "+Orden+", 1, @i OUTPUT select @i");
                //comandoSql.Connection = conexionSQL2;
                //comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult eliminarArticulosTabla(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {


                comandoSql = new SqlCommand("exec Eliminar_DetalleDescarga  " + Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();

            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult guardarDescargaMaterial(objguardarDescarga Descarga)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(Descarga.idPlaza));
            int resultado = 0;
            int existencia = 0;
            int folio = 0;
            int bitacora = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                foreach (var item in Descarga.Articulos)
                {
                    comandoSql = new SqlCommand("select * from AlmacenWebDB.dbo.tbl_articulos where Descripcion='"+item.descripcion+"'");
                    comandoSql.Connection = conexionSQL2;
                    SqlDataReader reader2 = comandoSql.ExecuteReader();
                    int id = 0;
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            id = Convert.ToInt32(reader2[0]);

                        }
                    }
                    reader2.Close();
                    comandoSql = new SqlCommand("exec ValidaExistenciasTecnicos " + Descarga.idTecnico + ", " + id + ", " + item.cantidad);
                    comandoSql.Connection = conexionSQL2;
                    int result = Int32.Parse(comandoSql.ExecuteScalar().ToString());
                    if(result != 4){
                        existencia = existencia + 1;
                    }
                }
                if (existencia == 0)
                {
                    comandoSql = new SqlCommand("declare @clv_folio int exec Dame_Folio " + Descarga.Orden + ", " + Descarga.clvCategoria + ", @clv_folio OUTPUT select @clv_folio");
                    comandoSql.Connection = conexionSQL2;
                    folio = Int32.Parse(comandoSql.ExecuteScalar().ToString());

                    foreach (var x in Descarga.Articulos)
	                {
                        comandoSql = new SqlCommand("select * from AlmacenWebDB.dbo.tbl_articulos where Descripcion ='" + x.descripcion + "'");
                        comandoSql.Connection = conexionSQL2;
                        var idReal = 0;
                        SqlDataReader reader2 = comandoSql.ExecuteReader();
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                idReal = Convert.ToInt32(reader2[0]);

                            }
                        }
                        reader2.Close();

                        comandoSql = new SqlCommand("exec Add_Rel_Session_Tecnicos " + Descarga.Session + "," + idReal + "," + x.cantidad + "," + Descarga.idTecnico);
                        comandoSql.Connection = conexionSQL2;
                        comandoSql.ExecuteNonQuery();
	                }
                   

                    comandoSql = new SqlCommand(" declare @x int exec Inserta_Bitacora_tec " + Descarga.Session + ", " + Descarga.Orden + ", " + folio + ", 1, " + Descarga.idTecnico + ", '', 'P', '',@x OUTPUT select @x");
                    comandoSql.Connection = conexionSQL2;
                    bitacora = Int32.Parse(comandoSql.ExecuteScalar().ToString());

                    comandoSql = new SqlCommand("exec Inserta_Rel_Bitacora_Orden " + bitacora + "," + Descarga.Orden);
                    comandoSql.Connection = conexionSQL2;
                    comandoSql.ExecuteNonQuery();

                    foreach (var item in Descarga.Articulos)
                    {
                        comandoSql = new SqlCommand("select * from AlmacenWebDB.dbo.tbl_articulos where Descripcion='"+item.descripcion+"'");
                        comandoSql.Connection = conexionSQL2;
                        SqlDataReader reader2 = comandoSql.ExecuteReader();
                        int id = 0;
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                id = Convert.ToInt32(reader2[0]);

                            }
                        }
                        reader2.Close();
                        comandoSql = new SqlCommand("select * from AlmacenWebDB.dbo.tbl_AlmacenEmpresa where Descripcion='"+item.almacen+"'");
                        comandoSql.Connection = conexionSQL2;
                        SqlDataReader reader3 = comandoSql.ExecuteReader();
                        int idAlmacen = 0;
                        if (reader3.HasRows)
                        {
                            while (reader3.Read())
                            {
                                idAlmacen = Convert.ToInt32(reader3[0]);

                            }
                        }
                        reader3.Close();
                        int cable = 0;
                        if(item.mfi > 0 || item.mfe > 0 || item.mie > 0 || item.mii > 0){
                            cable = 1;
                        }
                        comandoSql = new SqlCommand("exec Add_DescargaMaterial " + Descarga.Contrato + "," + Descarga.Orden+","+id+","+item.cantidad+","+bitacora+","+Descarga.idTecnico+","+idAlmacen+","+item.mii+","+item.mfi+","+cable+","+item.mie+","+item.mfe);
                        comandoSql.Connection = conexionSQL2;
                        comandoSql.ExecuteNonQuery();
                        resultado = 1;


                        comandoSql = new SqlCommand("exec Borra_Inserta_Tempo " + Descarga.Session);
                        comandoSql.Connection = conexionSQL2;
                        comandoSql.ExecuteNonQuery();
                        
                    }

                }
                
            }
            catch { }
            return Json(resultado, JsonRequestBehavior.AllowGet); ;
        }

        public class objguardarDescarga
        {
            public int idPlaza { get; set; }
            public int Session { get; set; }
            public int idTecnico { get; set; }
            public int clvCategoria { get; set; }
            public int Orden { get; set; }
            public int Contrato { get; set; }
            public List<objDetalleArticulo> Articulos { get; set; }
        }

        public ActionResult addArticuloExtensiones(int idPlaza, int Orden, int Articulo, int Tecnico, int Almacen, int Mii, int Mfi, int Mie, int Mfe, int Cantidad, int Extension)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {


                comandoSql = new SqlCommand("exec Add_DescargaMaterialExtenciones  " + Orden+","+Articulo+","+Tecnico+","+Almacen+","+Mii+","+Mfi+",'O',"+Mie+","+Mfe+","+Cantidad+","+Extension+",0");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();

            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult consultarArticulosTablaExtensiones(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objConsultarMaterial objeto = new objConsultarMaterial();
            List<objDetalleArticulo> articulos = new List<objDetalleArticulo>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec ConsultaDescargaMaterialExtenciones " + Orden);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objDetalleArticulo articulo = new objDetalleArticulo();
                        articulo.id = Convert.ToInt32(reader[9]);
                        articulo.orden = Convert.ToInt32(reader[10]);
                        articulo.clave = reader[1].ToString();
                        articulo.descripcion = reader[2].ToString();
                        articulo.tecnico = reader[4].ToString();
                        try
                        {
                            articulo.cantidad = Convert.ToInt32(reader[3]);
                        }
                        catch { articulo.cantidad = 0; }

                        try
                        {
                            articulo.mii = Convert.ToInt32(reader[5]);
                        }
                        catch { articulo.mii = 0; }
                        try
                        {
                            articulo.mfi = Convert.ToInt32(reader[6]);
                        }
                        catch { articulo.mfi = 0; }
                        try
                        {
                            articulo.mie = Convert.ToInt32(reader[7]);
                        }
                        catch { articulo.mie = 0; }
                        try
                        {
                            articulo.mfe = Convert.ToInt32(reader[8]);
                        }
                        catch { articulo.mfe = 0; }

                        articulos.Add(articulo);
                    }
                }
                reader.Close();
                objeto.articulos = articulos;
            }
            catch { }
            return Json(objeto, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult eliminarMaterialExtensiones(int idPlaza, int ID)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec UspCancela_DescargaMaterialExtensiones " + ID);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult eliminarTodoMaterialExtensiones(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec UspCancelaDescargaExtensiones " + Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult consultarExtencionesArticulosDetalle(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objConsultarMaterial objeto = new objConsultarMaterial();
            List<objDetalleArticulo> articulos = new List<objDetalleArticulo>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Consulta_DescargaMaterialExtenciones " + Orden);
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objeto.almacen = reader[0].ToString();
                        objeto.categoria = reader[1].ToString();
                        objeto.articulo = reader[3].ToString();
                        objeto.bitacora = reader[4].ToString();
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            objDetalleArticulo articulo = new objDetalleArticulo();
                            articulo.orden = Convert.ToInt32(reader[0]);
                            articulo.clave = reader[1].ToString();
                            articulo.descripcion = reader[2].ToString();
                            articulo.tecnico = reader[4].ToString();
                            try
                            {
                                articulo.cantidad = Convert.ToInt32(reader[3]);
                            }
                            catch { articulo.cantidad = 0; }

                            try
                            {
                                articulo.mii = Convert.ToInt32(reader[5]);
                            }
                            catch { articulo.mii = 0; }
                            try
                            {
                                articulo.mfi = Convert.ToInt32(reader[6]);
                            }
                            catch { articulo.mfi = 0; }
                            try
                            {
                                articulo.mie = Convert.ToInt32(reader[7]);
                            }
                            catch { articulo.mie = 0; }
                            try
                            {
                                articulo.mfe = Convert.ToInt32(reader[8]);
                            }
                            catch { articulo.mfe = 0; }

                            articulos.Add(articulo);
                        }
                    }

                }
                reader.Close();
                objeto.articulos = articulos;
            }
            catch { }
            return Json(objeto, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult generarBPAQU(int idPlaza, int Contrato)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec CreateProcesoAuto_BPAQU 2," + Contrato + ",'P',0,''");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult EliminarOrden(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Eliminar_Orden " + Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult ejecutarOrden(int idPlaza, string Fecha, string Visita1, string Visita2, string Observaciones, int Usuario, int Orden, int Op, int Op2)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec  EjecutarOS '"+Fecha+"','"+Visita1+"','"+Visita2+"','"+Observaciones+"',"+Usuario+","+Orden+","+Op+","+Op2);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }


        public ActionResult consultarBPAQU(int idPlaza,int Clave, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objconsultaBPAQU> cables = new List<objconsultaBPAQU>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec MUESTRAIPAQU_porSOL "+Clave+", "+Orden+", 0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objconsultaBPAQU cable = new objconsultaBPAQU(); 
                        cable.contratoNet = Convert.ToInt32(reader[0]);
                        cable.Mac = reader[1].ToString();
                        cables.Add(cable);
                    }

                }
                reader.Close();
            }
            catch { }
            return Json(cables, JsonRequestBehavior.AllowGet); ;
        }

        public class objconsultaBPAQU
        {
            public int contratoNet { get; set; }
            public string Mac { get; set; }
        }

        public ActionResult eliminarTodoBPAQU(int idPlaza, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int restult = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec DeleteAll_BPAQU " + Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(restult, JsonRequestBehavior.AllowGet); ;
        }


        public ActionResult guardarDetalleAsignacion(objAsignacion Objeto)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(Objeto.idPlaza));
            int restult = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {
                comandoSql = new SqlCommand("exec  DeleteAll_ICABM "+Objeto.Orden);
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();

                foreach (var item in Objeto.contratosNet)
                {
                    comandoSql = new SqlCommand("exec  NUEICABM_SOL " + Objeto.Clave + ", " + Objeto.Orden + ", " + item.ContratoNet + ", 0 ");
                    comandoSql.Connection = conexionSQL2;
                    comandoSql.ExecuteNonQuery();
                }
                
            }
            catch { }
            return Json(restult, JsonRequestBehavior.AllowGet); ;
        }


        public class objAsignacion
        {
            public int idPlaza { get; set; }
            public int Clave { get; set; }
            public int Orden { get; set; }

            public List<ContratosNet> contratosNet { get; set; }
        }
        public class ContratosNet
        {
            public int ContratoNet { get; set; }
        }

        public ActionResult consultarICAM(int idPlaza, int Clave, int Orden)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objconsultaBPAQU> cables = new List<objconsultaBPAQU>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec MUESTRAICAM_porSOL " + Orden + ", " + Clave + ", 0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objconsultaBPAQU cable = new objconsultaBPAQU();
                        cable.contratoNet = Convert.ToInt32(reader[0]);
                        cable.Mac = reader[1].ToString();
                        cables.Add(cable);
                    }

                }
                reader.Close();
            }
            catch { }
            return Json(cables, JsonRequestBehavior.AllowGet); ;
        }


        public ActionResult getCablemodemsDisponibles(int idPlaza)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            List<objCablemodems> macs = new List<objCablemodems>();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec MUESTRACABLEMODEMS_disponibles 0");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        objCablemodems mac = new objCablemodems();
                        mac.clave = Convert.ToInt32(reader[0]);
                        mac.mac = reader[1].ToString();
                        macs.Add(mac);
                    }

                }
                reader.Close();
            }
            catch { }
            return Json(macs, JsonRequestBehavior.AllowGet); ;
        }
        public class objCablemodems
        {
            public int clave { get; set; }
            public string mac { get; set; }
        }

        public ActionResult actualizarCablemodem(int idPlaza, int Clave, int Orden, int ContratoNet, int Clv_Mac)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            int result = 0;
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec Update_ICABM  "+Clave+", "+Orden+", "+ContratoNet+", "+Clv_Mac+", 1");
                comandoSql.Connection = conexionSQL2;
                comandoSql.ExecuteNonQuery();
            }
            catch { }
            return Json(result, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult ConsultarDatosCablemodem(int idPlaza, int Clave, int Orden, int ContratoNet)
        {
            ConexionController c = new ConexionController();
            SqlCommand comandoSql;
            SqlConnection conexionSQL2 = new SqlConnection(c.DameConexion(idPlaza));
            objConsultaCablemodem mac = new objConsultaCablemodem();
            try
            {
                conexionSQL2.Open();
            }
            catch
            { }

            try
            {

                comandoSql = new SqlCommand("exec CONICABM "+Clave+", "+Orden+", "+ContratoNet+"");
                comandoSql.Connection = conexionSQL2;
                SqlDataReader reader = comandoSql.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        mac.clave = Convert.ToInt32(reader[3]);
                        mac.mac = reader[4].ToString();
                    }

                }
                reader.Close();
            }
            catch { }
            return Json(mac, JsonRequestBehavior.AllowGet); ;
        }
        public class objConsultaCablemodem
        {
            public int clave { get; set; }
            public string mac { get; set; }
        }
    }
}