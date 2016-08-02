
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Text;

using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Data.SqlClient;
using Softv.Entities;
using System.Configuration;

//using System.Data.OleDb;
//using System.Data.Odbc;
//using System.Data.Common;
//using System.Data.SqlTypes;


namespace SoftvMVC.Controllers
{

    public class ReportesVariosController : BaseController
    {
       

        //******************
        ConexionController c = new ConexionController();
        SqlCommand comandoSql;
        //*****        

        //String conexion = "Data Source=BLANCA-PC;Initial Catalog=MasCiudades;User ID=saB;Password=41984; ";

        //Pie de página
        public partial class Footer : PdfPageEventHelper
        {

            public override void OnEndPage(PdfWriter writer, Document doc)
            {
                Paragraph footer = new Paragraph("Este es el pie de página", FontFactory.GetFont(FontFactory.TIMES, 10, iTextSharp.text.Font.NORMAL));

                footer.Alignment = Element.ALIGN_RIGHT;

                PdfPTable footerTbl = new PdfPTable(1);

                footerTbl.TotalWidth = 300;

                footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell cell = new PdfPCell(footer);

                cell.Border = 0;

                cell.PaddingLeft = 10;

                footerTbl.AddCell(cell);

                footerTbl.WriteSelectedRows(0, -1, 415, 30, writer.DirectContent);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

    

        public class objPrincipal
        {
            public int op { get; set; }
            public int Orden { get; set; }
            public int soloInternet { get; set; }
            public int clv_reporte { get; set; }

        }

        public class objTipoCliente
        {
            public List<string> tipoCliente { get; set; }

        }
        public class objServicio
        {
            public List<string> servicio { get; set; }
        }
        public class objCiudades
        {
            public List<string> ciudades { get; set; }
        }
        public class objColonias
        {
            public List<string> colonias { get; set; }
        }
        public class objTelefono
        {
            public Boolean telefono { get; set; }
            public int todos { get; set; }

        }
        public class objPeriodo
        {
            //  public string periodo { get; set; }
            public List<string> periodo { get; set; }
            public int ultimo_mes { get; set; }
            public int ultimo_anio { get; set; }
        }
        public class objEstatusOrden
        {
            public int OrdenEjecutada { get; set; }
        }
        public class objRangoFechas
        {
            public string fechaInicial { get; set; }
            public string fechaFinal { get; set; }
            public int motcan { get; set; }
            public int motivo { get; set; }//si hay o no motivo de cancelacion
        }
        public class objCalles
        {
            public List<string> calles { get; set; }
        }

        

        public class objEstatusCliente
        {
            public List<string> estatusCliente { get; set; }
            public int buscarPor { get; set; }
            public int mes { get; set; }
            public int anio { get; set; }
            public int motivo { get; set; }
            public int motivoCancelacion { get; set; }
            public int buscaOno { get; set; }
        }
        public class objParametros
        {
            public int op_rep { get; set; }
            public int habilita { get; set; }
            public int periodo1 { get; set; }
            public int periodo2 { get; set; }
            //     public int ultimo_mes { get; set; }
            //    public int ultimo_anio { get; set; } //bigint


            public int conectado { get; set; }
            public int baja { get; set; }
            public int Insta { get; set; }
            public int Desconect { get; set; }
            public int Susp { get; set; }
            public int Fuera { get; set; }
            public int DescTmp { get; set; }


        }
        
        public class objReporte
        {
            public int num_reporte { get; set; }

        }




        public ActionResult Create(objPrincipal objPrincipal, objTipoCliente objTipoCliente, objServicio objServicio, objCiudades objCiudades,
            objCalles objCalles, objColonias objColonias, objTelefono objTelefono, objPeriodo objPeriodo, objEstatusOrden objEstatusOrden,
            objEstatusCliente objEstatusCliente, objParametros objParametros, objRangoFechas objRangoFechas, objReporte objReporte)
        {
            string xmlComoString;
            if (objReporte.num_reporte == 0 || objReporte.num_reporte == 1 || objReporte.num_reporte == 5)
            {   //Desconectados, suspendidos, por pagar
                xmlComoString = Filtro_DSP(objPrincipal, objParametros, objReporte, objTipoCliente, objServicio, objCiudades,
          objColonias, objTelefono, objPeriodo, objEstatusOrden);

            }
            else if (objReporte.num_reporte == 2 || objReporte.num_reporte == 3 || objReporte.num_reporte == 8) //suspendidos
            {   //Al corriente, adelantados, por instalar
                xmlComoString = Filtro_APA(objPrincipal, objParametros, objReporte, objTipoCliente, objServicio, objCiudades,
               objColonias, objPeriodo);

            }
            else if (objReporte.num_reporte == 4 || objReporte.num_reporte == 6 || objReporte.num_reporte == 7 || objReporte.num_reporte == 9) //suspendidos
            {   //Contrataciones, Instalación, Cancelación, Fueras de Area
                xmlComoString = Filtro_ICCF(objPrincipal, objParametros, objReporte, objTipoCliente, objServicio, objCiudades,
               objColonias, objRangoFechas, objPeriodo);
            }
            else if (objReporte.num_reporte == 10) //paquetes de c
            {
                xmlComoString = Filtro_Paquetes(objPrincipal, objParametros, objReporte, objServicio, objCiudades,
               objColonias, objRangoFechas, objPeriodo);
            }
            else //(objReporte.num_reporte == 11 || objReporte.num_reporte == 12)
            //Ciudad, Resumen
            {
                xmlComoString = Filtro_CR(objPrincipal, objParametros, objReporte,
                objTipoCliente, objServicio, objCiudades, objColonias, objCalles,
                objEstatusCliente, objPeriodo, objTelefono, objEstatusOrden, objRangoFechas);
            }

            return Json(xmlComoString, JsonRequestBehavior.AllowGet);

            // return null;
        }

        //Desconectados, Suspendidos y por pagar
        public string Filtro_DSP(objPrincipal objPrincipal, objParametros objParametros, objReporte objReporte,
            objTipoCliente objTipoCliente, objServicio objServicio, objCiudades objCiudades, objColonias objColonias,
            objTelefono objTelefono, objPeriodo objPeriodo, objEstatusOrden objEstatusOrden)
        {
            XElement principal = XElement.Parse(Globals.SerializeTool.Serialize<objPrincipal>(objPrincipal));
            XElement parametros = XElement.Parse(Globals.SerializeTool.Serialize<objParametros>(objParametros));
            XElement tipoCliente = new XElement("TipoCliente", objTipoCliente.tipoCliente.Select(i => new XElement("tipoCliente", new XAttribute("Clv_TipoCliente", i))));
            XElement servicio = new XElement("Servicio", objServicio.servicio.Select(i => new XElement("servicio", new XAttribute("Clv_TipSer", i))));
            XElement ciudades = new XElement("Ciudades", objCiudades.ciudades.Select(i => new XElement("ciudad", new XAttribute("Clv_Ciudad", i))));
            XElement colonias = new XElement("Colonias", objColonias.colonias.Select(i => new XElement("colonia", new XAttribute("Clv_Colonia", i))));
            XElement telefono = XElement.Parse(Globals.SerializeTool.Serialize<objTelefono>(objTelefono));
            XElement periodo = new XElement("Periodo", objPeriodo.periodo.Select(i =>
                                new XElement("periodo", new XAttribute("Clv_Periodo", i))),
                                                        new XAttribute("ultimo_mes", objPeriodo.ultimo_mes),
                                                        new XAttribute("ultimo_anio", objPeriodo.ultimo_anio));
            XElement OrdenEjecutada = XElement.Parse(Globals.SerializeTool.Serialize<objEstatusOrden>(objEstatusOrden));

            principal.Add(parametros, tipoCliente, servicio, ciudades, colonias, telefono, periodo, OrdenEjecutada);//habilita, periodo1, periodo2, ultimo_mes, ultimo_anio);


            string xmlComoString = principal.ToString(); //convierte el xml a String
            //return Content(xmlComoString);

            return xmlComoString;

        }

        //Al corriente, adelantados, por instalar
        public string Filtro_APA(objPrincipal objPrincipal, objParametros objParametros, objReporte objReporte,
         objTipoCliente objTipoCliente, objServicio objServicio, objCiudades objCiudades, objColonias objColonias,
         objPeriodo objPeriodo)
        {
            XElement principal = XElement.Parse(Globals.SerializeTool.Serialize<objPrincipal>(objPrincipal));
            XElement parametros = XElement.Parse(Globals.SerializeTool.Serialize<objParametros>(objParametros));
            XElement tipoCliente = new XElement("TipoCliente", objTipoCliente.tipoCliente.Select(i => new XElement("tipoCliente", new XAttribute("Clv_TipoCliente", i))));
            XElement servicio = new XElement("Servicio", objServicio.servicio.Select(i => new XElement("servicio", new XAttribute("Clv_TipSer", i))));
            XElement ciudades = new XElement("Ciudades", objCiudades.ciudades.Select(i => new XElement("ciudad", new XAttribute("Clv_Ciudad", i))));
            XElement colonias = new XElement("Colonias", objColonias.colonias.Select(i => new XElement("colonia", new XAttribute("Clv_Colonia", i))));
            XElement periodo = new XElement("Periodo", objPeriodo.periodo.Select(i =>
                                new XElement("periodo", new XAttribute("Clv_Periodo", i))),
                                                        new XAttribute("ultimo_mes", objPeriodo.ultimo_mes),
                                                        new XAttribute("ultimo_anio", objPeriodo.ultimo_anio));

            principal.Add(parametros, tipoCliente, servicio, ciudades, colonias, periodo);//habilita, periodo1, periodo2, ultimo_mes, ultimo_anio);

            var xmlComoString = principal.ToString();
            return xmlComoString;
        }

        //Instalacion, cancelacion, fueras de area, contrataciones
        public string Filtro_ICCF(objPrincipal objPrincipal, objParametros objParametros, objReporte objReporte,
        objTipoCliente objTipoCliente, objServicio objServicio, objCiudades objCiudades, objColonias objColonias,
        objRangoFechas objRangoFechas, objPeriodo objPeriodo)
        {
            XElement principal = XElement.Parse(Globals.SerializeTool.Serialize<objPrincipal>(objPrincipal));
            XElement parametros = XElement.Parse(Globals.SerializeTool.Serialize<objParametros>(objParametros));
            XElement tipoCliente = new XElement("TipoCliente", objTipoCliente.tipoCliente.Select(i => new XElement("tipoCliente", new XAttribute("Clv_TipoCliente", i))));
            XElement servicio = new XElement("Servicio", objServicio.servicio.Select(i => new XElement("servicio", new XAttribute("Clv_TipSer", i))));
            XElement ciudades = new XElement("Ciudades", objCiudades.ciudades.Select(i => new XElement("ciudad", new XAttribute("Clv_Ciudad", i))));
            XElement colonias = new XElement("Colonias", objColonias.colonias.Select(i => new XElement("colonia", new XAttribute("Clv_Colonia", i))));
            XElement rango = XElement.Parse(Globals.SerializeTool.Serialize<objRangoFechas>(objRangoFechas));
            XElement periodo = new XElement("Periodo", objPeriodo.periodo.Select(i =>
                                new XElement("periodo", new XAttribute("Clv_Periodo", i))),
                                                        new XAttribute("ultimo_mes", objPeriodo.ultimo_mes),
                                                        new XAttribute("ultimo_anio", objPeriodo.ultimo_anio));

            principal.Add(parametros, tipoCliente, servicio, ciudades, colonias, rango, periodo);//habilita, periodo1, periodo2, ultimo_mes, ultimo_anio);

            var xmlComoString = principal.ToString();
            return xmlComoString;
        }
        //Paquetes de C
        public string Filtro_Paquetes(objPrincipal objPrincipal, objParametros objParametros, objReporte objReporte,
        objServicio objServicio, objCiudades objCiudades, objColonias objColonias,
        objRangoFechas objRangoFechas, objPeriodo objPeriodo)
        {
            XElement principal = XElement.Parse(Globals.SerializeTool.Serialize<objPrincipal>(objPrincipal));
            XElement parametros = XElement.Parse(Globals.SerializeTool.Serialize<objParametros>(objParametros));

            XElement servicio = new XElement("Servicio", objServicio.servicio.Select(i => new XElement("servicio", new XAttribute("Clv_TipSer", i))));
            XElement ciudades = new XElement("Ciudades", objCiudades.ciudades.Select(i => new XElement("ciudad", new XAttribute("Clv_Ciudad", i))));
            XElement colonias = new XElement("Colonias", objColonias.colonias.Select(i => new XElement("colonia", new XAttribute("Clv_Colonia", i))));
            XElement rango = XElement.Parse(Globals.SerializeTool.Serialize<objRangoFechas>(objRangoFechas));
            XElement periodo = new XElement("Periodo", objPeriodo.periodo.Select(i =>
                                new XElement("periodo", new XAttribute("Clv_Periodo", i))),
                                                        new XAttribute("ultimo_mes", objPeriodo.ultimo_mes),
                                                        new XAttribute("ultimo_anio", objPeriodo.ultimo_anio));

            principal.Add(parametros, servicio, ciudades, colonias, rango, periodo);//habilita, periodo1, periodo2, ultimo_mes, ultimo_anio);

            var xmlComoString = principal.ToString();
            return xmlComoString;
        }

        //Ciudad, Resumen
        public string Filtro_CR(objPrincipal objPrincipal, objParametros objParametros, objReporte objReporte,
            objTipoCliente objTipoCliente, objServicio objServicio, objCiudades objCiudades, objColonias objColonias,
            objCalles objCalles, objEstatusCliente objEstatusCliente, objPeriodo objPeriodo,
            objTelefono objTelefono, objEstatusOrden objEstatusOrden, objRangoFechas objRangoFechas)
        {
            XElement principal = XElement.Parse(Globals.SerializeTool.Serialize<objPrincipal>(objPrincipal));

            XElement parametros = XElement.Parse(Globals.SerializeTool.Serialize<objParametros>(objParametros));
            XElement tipoCliente = new XElement("TipoCliente", objTipoCliente.tipoCliente.Select(i => new XElement("tipoCliente", new XAttribute("Clv_TipoCliente", i))));
            XElement servicio = new XElement("Servicio", objServicio.servicio.Select(i => new XElement("servicio", new XAttribute("Clv_TipSer", i))));
            XElement ciudades = new XElement("Ciudades", objCiudades.ciudades.Select(i => new XElement("ciudad", new XAttribute("Clv_Ciudad", i))));
            XElement colonias = new XElement("Colonias", objColonias.colonias.Select(i => new XElement("colonia", new XAttribute("Clv_Colonia", i))));
            XElement telefono = XElement.Parse(Globals.SerializeTool.Serialize<objTelefono>(objTelefono));

            XElement fechas = XElement.Parse(Globals.SerializeTool.Serialize<objRangoFechas>(objRangoFechas));
            XElement periodo = new XElement("Periodo", objPeriodo.periodo.Select(i =>
                                new XElement("periodo", new XAttribute("Clv_Periodo", i))),
                                                        new XAttribute("ultimo_mes", objPeriodo.ultimo_mes),
                                                        new XAttribute("ultimo_anio", objPeriodo.ultimo_anio));

            XElement calles = new XElement("Calle", objCalles.calles.Select(i => new XElement("calle", new XAttribute("Clv_Calle", i))));

            XElement estatusCliente = new XElement("estatus", objEstatusCliente.estatusCliente.Select(i =>
                                    new XElement("estatusCliente", new XAttribute("estatus", i))),
                                                new XAttribute("buscaOno", objEstatusCliente.buscaOno),
                                               new XAttribute("buscarPor", objEstatusCliente.buscarPor),
                                               new XAttribute("mes", objEstatusCliente.mes),
                                               new XAttribute("anio", objEstatusCliente.anio),
                                               new XAttribute("motivo", objEstatusCliente.motivo),
                                               new XAttribute("motivoCancelacion", objEstatusCliente.motivoCancelacion));



            XElement OrdenEjecutada = XElement.Parse(Globals.SerializeTool.Serialize<objEstatusOrden>(objEstatusOrden));
            principal.Add(parametros, tipoCliente, servicio, ciudades, colonias, calles, telefono, fechas, periodo, OrdenEjecutada, estatusCliente);//habilita, periodo1, periodo2, ultimo_mes, ultimo_anio);


            var xmlComoString = principal.ToString();

            //return null;
            return xmlComoString;
        }



        [ValidateInput(false)]
        public ActionResult GenerarPdf(string cadena) //recibir string
        {
            System.IO.FileStream fs = new FileStream(Server.MapPath("/Reportes/") + "\\" + "First PDF document.pdf", FileMode.Create);

            // string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";

            // Create an instance of the document class which represents the PDF document itself.
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            // Create an instance to the PDF file by creating an instance of the PDF 
            // Writer class using the document and the filestrem in the constructor.
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            // Open the document to enable you to write to the document
            document.Open();
            // Add a simple and wellknown phrase to the document in a flow layout manner
            document.Add(new Paragraph("Hello World!"));
            // Close the document
            document.Close();
            // Close the writer instance
            writer.Close();
            // Always close open filehandles explicity
            fs.Close();
            return null;
        }

        



        //Reportes_varios_Fechas_2
        [ValidateInput(false)]
        public ActionResult Reportes_varios_Fechas_2(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            // string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";  
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 3, 2, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clContrato);


            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clNombre);

            PdfPCell clServicios = new PdfPCell(new Phrase("Servicios", titulo8Bold));
            clServicios.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicios.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicios.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicios.UseVariableBorders = true;
            clServicios.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clServicios);

            PdfPCell clTelefono = new PdfPCell(new Phrase("Telefono", titulo8Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clTelefono);

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clCelular);

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
            clPeriodo.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColor = BaseColor.BLACK;
            tblPrueba.AddCell(clPeriodo);

            // Añadimos las celdas a la tabla
            
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_nuevo1 @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                }
                if (reader.NextResult()) //tabla 3 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);

                    }

                }
                if (reader.NextResult()) //tabla 4 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla5 Query 
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        //tblPrueba.AddCell(reader[0].ToString());
                        for (int i = 0; i <= 6; i++)
                        {

                            if (i != 3)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE; //los 4 bordes
                                tblPrueba.AddCell(celda);
                            }
                        }
                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 6 count ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //TABLA GRAN TOTAL DE CLIENTES   
            float[] columnWidths2 = { 1 };    //ancho de las columnas

            PdfPTable tblClientes = new PdfPTable(columnWidths2);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));
            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();

            return Content("Reportes/" + nombreArchivo2);
        }



        //Suspendidos Cable
        //REportePorPagarTv_2
        [ValidateInput(false)]
        public ActionResult REportePorPagarTv_2(string cadena, int idConexion)
        {
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);
            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 1, 1, 2, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo7Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColor = BaseColor.BLACK;
            //clContrato.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo7Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColor = BaseColor.BLACK;

            PdfPCell clUltMes = new PdfPCell(new Phrase("Último Mes", titulo7Bold));
            clUltMes.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clUltMes.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clUltMes.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clUltMes.UseVariableBorders = true;
            clUltMes.BorderColor = BaseColor.BLACK;

            PdfPCell clUltAnio = new PdfPCell(new Phrase("Último Año", titulo7Bold));
            clUltAnio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clUltAnio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clUltAnio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clUltAnio.UseVariableBorders = true;
            clUltAnio.BorderColor = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo7Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColor = BaseColor.BLACK;

            PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo7Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColor = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo7Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColor = BaseColor.BLACK;

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo7Bold));
            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColor = BaseColor.BLACK;


            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clUltMes);
            tblPrueba.AddCell(clUltAnio);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clPeriodo);
            doc.Add(salto);
            
            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_telefono @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //Tabla 0
                {
                    //clvREPORTE
                }

                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read()) //Tabla1 Empresa
                    {
                        nombreEmpresa = reader.GetString(0);
                        Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                        empresa.Alignment = Element.ALIGN_CENTER;
                        doc.Add(empresa);
                    }
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }

                if (reader.NextResult()) //tabla3 sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                }
                if (reader.NextResult()) //tabla4 todas las ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                        //Paragraph ciudadesP = new Paragraph(ciudades, titulo10);
                        //ciudadesP.Alignment = Element.ALIGN_CENTER;
                        //doc.Add(ciudadesP);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }

                if (reader.NextResult()) //tabla5 clientes
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 10;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 10;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        for (int i = 0; i <= 9; i++)
                        {
                            if (i != 2 && i != 7 && i != 8)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                            else if (i == 2)
                            {
                                var ultimo_mes = Convert.ToInt32(reader[2]);
                                String mes = " ";
                                if (ultimo_mes == 1)
                                {
                                    mes = "Enero";
                                }
                                else if (ultimo_mes == 2)
                                {
                                    mes = "Febrero";
                                }
                                else if (ultimo_mes == 3)
                                {
                                    mes = "Marzo";
                                }
                                else if (ultimo_mes == 4)
                                {
                                    mes = "Abril";
                                }
                                else if (ultimo_mes == 5)
                                {
                                    mes = "Mayo";
                                }
                                else if (ultimo_mes == 6)
                                {
                                    mes = "Junio";
                                }
                                else if (ultimo_mes == 7)
                                {
                                    mes = "Julio";
                                }
                                else if (ultimo_mes == 8)
                                {
                                    mes = "Agosto";
                                }
                                else if (ultimo_mes == 9)
                                {
                                    mes = "Septiembre";
                                }
                                else if (ultimo_mes == 10)
                                {
                                    mes = "Octubre";
                                }
                                else if (ultimo_mes == 11)
                                {
                                    mes = "Noviembre";
                                }
                                else if (ultimo_mes == 12)
                                {
                                    mes = "Diciembre";
                                }

                                PdfPCell celda = new PdfPCell(new Phrase(mes, titulo8));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                        }

                    }

                    doc.Add(tblPrueba);//tabla de clientes
                    doc.Add(salto);//salto
                }

                if (reader.NextResult()) //tabla5 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[1].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[3].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CONTRATOS           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Contratos: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //************* Número de página
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //Por pagar cable
        [ValidateInput(false)]
        public ActionResult ReportePorPagarInternet_2_porPagar(string cadena, int idConexion)
        {
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);
            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";

            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 1, 1, 2, 1, 1, 1, 2 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColor = BaseColor.BLACK;
            //clContrato.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColor = BaseColor.BLACK;

            PdfPCell clMes = new PdfPCell(new Phrase("Último mes", titulo8Bold));
            clMes.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clMes.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clMes.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clMes.UseVariableBorders = true;
            clMes.BorderColor = BaseColor.BLACK;

            PdfPCell clAnio = new PdfPCell(new Phrase("Último año", titulo8Bold));
            clAnio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clAnio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clAnio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clAnio.UseVariableBorders = true;
            clAnio.BorderColor = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo8Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColor = BaseColor.BLACK;

            PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColor = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColor = BaseColor.BLACK;

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColor = BaseColor.BLACK;

            PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
            clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER; ;
            clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clDireccion.UseVariableBorders = true;
            clDireccion.BorderColor = BaseColor.BLACK;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clMes);
            tblPrueba.AddCell(clAnio);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clPeriodo);
            tblPrueba.AddCell(clDireccion);


            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_telefono @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //Tabla 0
                {
                    //clvREPORTE
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read()) //Tabla1 Empresa
                    {
                        nombreEmpresa = reader.GetString(0);
                        Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                        empresa.Alignment = Element.ALIGN_CENTER;
                        doc.Add(empresa);
                    }
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }

                if (reader.NextResult()) //tabla3 sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                }
                if (reader.NextResult()) //tabla4 todas las ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                        //Paragraph ciudadesP = new Paragraph(ciudades, titulo10);
                        //ciudadesP.Alignment = Element.ALIGN_CENTER;
                        //doc.Add(ciudadesP);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }

                if (reader.NextResult()) //tabla5 query
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 9;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 9;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        for (int i = 0; i <= 10; i++)
                        {
                            if (i != 2 && i != 7 && i != 8)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));
                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                            else if (i == 2)
                            {
                                var ultimo_mes = Convert.ToInt32(reader[2]);
                                String mes = " ";
                                if (ultimo_mes == 1)
                                {
                                    mes = "Enero";
                                }
                                else if (ultimo_mes == 2)
                                {
                                    mes = "Febrero";
                                }
                                else if (ultimo_mes == 3)
                                {
                                    mes = "Marzo";
                                }
                                else if (ultimo_mes == 4)
                                {
                                    mes = "Abril";
                                }
                                else if (ultimo_mes == 5)
                                {
                                    mes = "Mayo";
                                }
                                else if (ultimo_mes == 6)
                                {
                                    mes = "Junio";
                                }
                                else if (ultimo_mes == 7)
                                {
                                    mes = "Julio";
                                }
                                else if (ultimo_mes == 8)
                                {
                                    mes = "Agosto";
                                }
                                else if (ultimo_mes == 9)
                                {
                                    mes = "Septiembre";
                                }
                                else if (ultimo_mes == 10)
                                {
                                    mes = "Octubre";
                                }
                                else if (ultimo_mes == 11)
                                {
                                    mes = "Noviembre";
                                }
                                else if (ultimo_mes == 12)
                                {
                                    mes = "Diciembre";
                                }

                                PdfPCell celda = new PdfPCell(new Phrase(mes, titulo7));
                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                        }

                    }
                    doc.Add(tblPrueba);//tabla de clientes
                    doc.Add(salto);//salto
                }

                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[1].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[3].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }

            // Escribimos el encabezamiento en el documento

            //************
            //TABLA GRAN TOTAL DE CONTRATOS      
            float[] columnWidths2 = { 1 };    //ancho de las columnas
            PdfPTable tblClientes = new PdfPTable(columnWidths2);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Contratos: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //************* Número de página
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }


        //Desconectados cable
        [ValidateInput(false)]
        public ActionResult ReportePorPagarInternet_2_descon(string cadena, int idConexion)
        {
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);
            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 1, 1, 2, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo7Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColor = BaseColor.BLACK;
            //clContrato.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo7Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColor = BaseColor.BLACK;

            PdfPCell clUltMes = new PdfPCell(new Phrase("Último Mes", titulo7Bold));
            clUltMes.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clUltMes.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clUltMes.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clUltMes.UseVariableBorders = true;
            clUltMes.BorderColor = BaseColor.BLACK;

            PdfPCell clUltAnio = new PdfPCell(new Phrase("Último Año", titulo7Bold));
            clUltAnio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clUltAnio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clUltAnio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clUltAnio.UseVariableBorders = true;
            clUltAnio.BorderColor = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo7Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColor = BaseColor.BLACK;

            PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo7Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColor = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo7Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColor = BaseColor.BLACK;

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo7Bold));
            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColor = BaseColor.BLACK;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clUltMes);
            tblPrueba.AddCell(clUltAnio);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clPeriodo);
            doc.Add(salto);

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_telefono @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //Tabla 0
                {
                    //clvREPORTE
                }

                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read()) //Tabla1 Empresa
                    {
                        nombreEmpresa = reader.GetString(0);
                        Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                        empresa.Alignment = Element.ALIGN_CENTER;
                        doc.Add(empresa);
                    }
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }

                if (reader.NextResult()) //tabla3 sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                }
                if (reader.NextResult()) //tabla4 todas las ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                        //Paragraph ciudadesP = new Paragraph(ciudades, titulo10);
                        //ciudadesP.Alignment = Element.ALIGN_CENTER;
                        //doc.Add(ciudadesP);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }

                if (reader.NextResult()) //tabla5 clientes
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 10;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 10;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        for (int i = 0; i <= 9; i++)
                        {
                            if (i != 2 && i != 7 && i != 8)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                            else if (i == 2)
                            {
                                var ultimo_mes = Convert.ToInt32(reader[2]);
                                String mes = " ";
                                if (ultimo_mes == 1)
                                {
                                    mes = "Enero";
                                }
                                else if (ultimo_mes == 2)
                                {
                                    mes = "Febrero";
                                }
                                else if (ultimo_mes == 3)
                                {
                                    mes = "Marzo";
                                }
                                else if (ultimo_mes == 4)
                                {
                                    mes = "Abril";
                                }
                                else if (ultimo_mes == 5)
                                {
                                    mes = "Mayo";
                                }
                                else if (ultimo_mes == 6)
                                {
                                    mes = "Junio";
                                }
                                else if (ultimo_mes == 7)
                                {
                                    mes = "Julio";
                                }
                                else if (ultimo_mes == 8)
                                {
                                    mes = "Agosto";
                                }
                                else if (ultimo_mes == 9)
                                {
                                    mes = "Septiembre";
                                }
                                else if (ultimo_mes == 10)
                                {
                                    mes = "Octubre";
                                }
                                else if (ultimo_mes == 11)
                                {
                                    mes = "Noviembre";
                                }
                                else if (ultimo_mes == 12)
                                {
                                    mes = "Diciembre";
                                }

                                PdfPCell celda = new PdfPCell(new Phrase(mes, titulo8));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                        }

                    }

                    doc.Add(tblPrueba);//tabla de clientes
                    doc.Add(salto);//salto
                }

                if (reader.NextResult()) //tabla5 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[1].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[3].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CONTRATOS           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Contratos: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //************* Número de página
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //Suspendidos, Desconectados, Por pagar --INTERNET 
        //Por pagar cable
        [ValidateInput(false)]
        public ActionResult REportePorPagarSDP(string cadena, int idConexion)
        {
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);
            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;


            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_telefono @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0, claveReporte = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //Tabla 0
                {
                    claveReporte = Convert.ToInt32(reader[0]); //
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read()) //Tabla1 Empresa
                    {
                        nombreEmpresa = reader.GetString(0);
                        Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                        empresa.Alignment = Element.ALIGN_CENTER;
                        doc.Add(empresa);
                    }
                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }

                if (reader.NextResult()) //tabla3 sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                }
                if (reader.NextResult()) //tabla4 todas las ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                        //Paragraph ciudadesP = new Paragraph(ciudades, titulo10);
                        //ciudadesP.Alignment = Element.ALIGN_CENTER;
                        //doc.Add(ciudadesP);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }

                if (reader.NextResult()) //tabla5 query
                {
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    if (claveReporte == 2 || claveReporte == 3)
                    {
                        clNombre.Colspan = 3;
                    }
                    else if (claveReporte == 1)
                    { clNombre.Colspan = 2; }

                    PdfPCell clMes = new PdfPCell(new Phrase("Último mes", titulo8Bold));
                    clMes.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clMes.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clMes.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clMes.UseVariableBorders = true;
                    clMes.BorderColor = BaseColor.BLACK;

                    PdfPCell clAnio = new PdfPCell(new Phrase("Último año", titulo8Bold));
                    clAnio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clAnio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clAnio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clAnio.UseVariableBorders = true;
                    clAnio.BorderColor = BaseColor.BLACK;

                    PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo8Bold));
                    clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.UseVariableBorders = true;
                    clServicio.BorderColor = BaseColor.BLACK;
                    if (claveReporte == 2 || claveReporte == 3)
                    {
                        clServicio.Colspan = 3;
                    }
                    else if (claveReporte == 1)
                    { clServicio.Colspan = 2; }

                    PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                    clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clTelefono.UseVariableBorders = true;
                    clTelefono.BorderColor = BaseColor.BLACK;

                    PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
                    clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCelular.UseVariableBorders = true;
                    clCelular.BorderColor = BaseColor.BLACK;

                    PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
                    if (claveReporte == 2 || claveReporte == 3)
                    {
                        clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    }
                    else if (claveReporte == 1)
                    { clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER; }
                    clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPeriodo.UseVariableBorders = true;
                    clPeriodo.BorderColor = BaseColor.BLACK;

                    // Añadimos las celdas a la tabla
                    tblPrueba.AddCell(clContrato);
                    tblPrueba.AddCell(clNombre);
                    tblPrueba.AddCell(clMes);
                    tblPrueba.AddCell(clAnio);
                    tblPrueba.AddCell(clServicio);
                    tblPrueba.AddCell(clTelefono);
                    tblPrueba.AddCell(clCelular);
                    tblPrueba.AddCell(clPeriodo);


                    if (claveReporte == 2 || claveReporte == 3)
                    {
                        clNombre.Colspan = 3;
                    }
                    else if (claveReporte == 1)
                    {
                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        clDireccion.Colspan = 2;
                        tblPrueba.AddCell(clDireccion);
                    }


                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 12;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 12;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        if (claveReporte == 2 || claveReporte == 3)
                        {
                            for (int i = 0; i <= 9; i++)
                            {

                                if (i != 2 && i != 7 && i != 8)
                                {
                                    PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo8));

                                    celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.UseVariableBorders = true;
                                    celda.BorderColor = BaseColor.WHITE;
                                    if (i == 1 || i == 4)
                                    {
                                        celda.Colspan = 3;
                                    }
                                    tblPrueba.AddCell(celda);
                                }
                                else if (i == 2)
                                {
                                    var ultimo_mes = Convert.ToInt32(reader[2]);
                                    String mes = " ";
                                    if (ultimo_mes == 1)
                                    {
                                        mes = "Enero";
                                    }
                                    else if (ultimo_mes == 2)
                                    {
                                        mes = "Febrero";
                                    }
                                    else if (ultimo_mes == 3)
                                    {
                                        mes = "Marzo";
                                    }
                                    else if (ultimo_mes == 4)
                                    {
                                        mes = "Abril";
                                    }
                                    else if (ultimo_mes == 5)
                                    {
                                        mes = "Mayo";
                                    }
                                    else if (ultimo_mes == 6)
                                    {
                                        mes = "Junio";
                                    }
                                    else if (ultimo_mes == 7)
                                    {
                                        mes = "Julio";
                                    }
                                    else if (ultimo_mes == 8)
                                    {
                                        mes = "Agosto";
                                    }
                                    else if (ultimo_mes == 9)
                                    {
                                        mes = "Septiembre";
                                    }
                                    else if (ultimo_mes == 10)
                                    {
                                        mes = "Octubre";
                                    }
                                    else if (ultimo_mes == 11)
                                    {
                                        mes = "Noviembre";
                                    }
                                    else if (ultimo_mes == 12)
                                    {
                                        mes = "Diciembre";
                                    }

                                    PdfPCell celda = new PdfPCell(new Phrase(mes, titulo8));

                                    celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.UseVariableBorders = true;
                                    celda.BorderColor = BaseColor.WHITE;
                                    tblPrueba.AddCell(celda);
                                }
                            }
                        }
                        else if (claveReporte == 1)
                        {
                            for (int i = 0; i <= 10; i++)
                            {

                                if (i != 2 && i != 7 && i != 8)
                                {
                                    PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo8));

                                    celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.UseVariableBorders = true;
                                    celda.BorderColor = BaseColor.WHITE;
                                    if (i == 1 || i == 4 || i == 10)
                                    {
                                        celda.Colspan = 2;
                                    }
                                    tblPrueba.AddCell(celda);
                                }
                                else if (i == 2)
                                {
                                    var ultimo_mes = Convert.ToInt32(reader[2]);
                                    String mes = " ";
                                    if (ultimo_mes == 1)
                                    {
                                        mes = "Enero";
                                    }
                                    else if (ultimo_mes == 2)
                                    {
                                        mes = "Febrero";
                                    }
                                    else if (ultimo_mes == 3)
                                    {
                                        mes = "Marzo";
                                    }
                                    else if (ultimo_mes == 4)
                                    {
                                        mes = "Abril";
                                    }
                                    else if (ultimo_mes == 5)
                                    {
                                        mes = "Mayo";
                                    }
                                    else if (ultimo_mes == 6)
                                    {
                                        mes = "Junio";
                                    }
                                    else if (ultimo_mes == 7)
                                    {
                                        mes = "Julio";
                                    }
                                    else if (ultimo_mes == 8)
                                    {
                                        mes = "Agosto";
                                    }
                                    else if (ultimo_mes == 9)
                                    {
                                        mes = "Septiembre";
                                    }
                                    else if (ultimo_mes == 10)
                                    {
                                        mes = "Octubre";
                                    }
                                    else if (ultimo_mes == 11)
                                    {
                                        mes = "Noviembre";
                                    }
                                    else if (ultimo_mes == 12)
                                    {
                                        mes = "Diciembre";
                                    }

                                    PdfPCell celda = new PdfPCell(new Phrase(mes, titulo8));

                                    celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                    celda.UseVariableBorders = true;
                                    celda.BorderColor = BaseColor.WHITE;
                                    tblPrueba.AddCell(celda);
                                }
                            }

                        }


                    }
                    doc.Add(tblPrueba);//tabla de clientes
                    doc.Add(salto);//salto
                }

                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[1].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[3].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }                
            }

            // Escribimos el encabezamiento en el documento

            //************
            //TABLA GRAN TOTAL DE CONTRATOS      
            float[] columnWidths2 = { 1 };    //ancho de las columnas
            PdfPTable tblClientes = new PdfPTable(columnWidths2);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Contratos: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //************* Número de página
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }





        //ReportePIInt_2
        [ValidateInput(false)]
        public ActionResult ReportePIInt_2(string cadena, int idConexion)
        {
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);
            // Indicamos donde vamos a guardar el documento
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 3, 2, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColor = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre del Cliente", titulo8Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColor = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo8Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColor = BaseColor.BLACK;

            PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColor = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColor = BaseColor.BLACK;

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColor = BaseColor.BLACK;

            // tblPrueba.DefaultCell.Border = Rectangle.NO_BORDER;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clPeriodo);

            //************

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_nuevo2 @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = "";
            int clvReporte = 0;
            if (reader.HasRows)
            {

                while (reader.Read()) //Tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);

                }
                if (reader.NextResult()) //tabla2 reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla3 sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla4 clvReporte
                {
                    while (reader.Read())
                    {
                        clvReporte = reader.GetInt32(0);
                    }
                }
                if (reader.NextResult()) //tabla 5 ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }

                }
                if (reader.NextResult()) //tabla 6 clientes
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;
                        //tblPrueba.AddCell(reader[0].ToString());
                        for (int i = 0; i <= 6; i++)
                        {
                            if (i != 3)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                        }
                    }
                    doc.Add(tblPrueba);//tabla de clientes
                    doc.Add(salto);//salto
                }

                if (reader.NextResult()) //tabla 7 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }


                }
                if (reader.NextResult()) //la tabla6 todas las ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }

                }
            }
            //TABLA GRAN TOTAL DE CONTRATOS           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //************* Número de página
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }





        //ReporteNuevoCortesiaTv_2
        [ValidateInput(false)]
        public ActionResult ReporteNuevoCortesiaTv_2(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 2, 1, 1, 1, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColorBottom = BaseColor.BLACK;
            clContrato.BorderColorLeft = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre del Cliente", titulo8Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo8Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
            clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clStatus.UseVariableBorders = true;
            clStatus.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clCortesia = new PdfPCell(new Phrase("Fecha de la Cortesía", titulo8Bold));
            clCortesia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCortesia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCortesia.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCortesia.UseVariableBorders = true;
            clCortesia.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clActiva = new PdfPCell(new Phrase("Activa", titulo8Bold));
            clActiva.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clActiva.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clActiva.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clActiva.UseVariableBorders = true;
            clActiva.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clTvSinPago = new PdfPCell(new Phrase("Tv sin Pago", titulo8Bold));
            clTvSinPago.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTvSinPago.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTvSinPago.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTvSinPago.UseVariableBorders = true;
            clTvSinPago.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clTvPago = new PdfPCell(new Phrase("Tv Pago", titulo8Bold));
            clTvPago.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTvPago.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTvPago.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTvPago.UseVariableBorders = true;
            clTvPago.BorderColorBottom = BaseColor.BLACK;
            clTvPago.BorderColorRight = BaseColor.BLACK;

            // tblPrueba.DefaultCell.Border = Rectangle.NO_BORDER;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clStatus);
            tblPrueba.AddCell(clCortesia);
            tblPrueba.AddCell(clActiva);
            tblPrueba.AddCell(clTvSinPago);
            tblPrueba.AddCell(clTvPago);


            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_cortesia_nuevo @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }
                }
                if (reader.NextResult()) //tabla3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 10;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 10;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                }
                if (reader.NextResult()) //tabla5 Clientes
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;

                        //tblPrueba.AddCell(reader[0].ToString());
                        for (int i = 0; i <= 11; i++)
                        {
                            if (i != 2 && i != 3 && i != 4)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo7));
                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE; //los 4 bordes
                                tblPrueba.AddCell(celda);
                            }
                            if (i == 2)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[14].ToString(), titulo7));
                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE; //los 4 bordes
                                tblPrueba.AddCell(celda);
                            }
                        }

                    }
                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {

                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes por Ciudad: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CLIENTES           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }




        //ReporteNuevoCortesiaIntDig_2
        [ValidateInput(false)]
        public ActionResult ReporteNuevoCortesiaIntDig_2(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 20, 20, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            //*************************

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_cortesia_nuevo @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);

                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);

                    }
                }
                if (reader.NextResult()) //tabla3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);

                }
                if (reader.NextResult()) //tabla4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                    // Creamos una tabla ------Ciudades        
                    PdfPTable tblCiudades = new PdfPTable(1);
                    tblCiudades.WidthPercentage = 100;

                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblCiudades.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblCiudades.AddCell(clCiudades);
                    tblCiudades.AddCell(clFilaVacia);

                    doc.Add(tblCiudades);

                }
                if (reader.NextResult()) //tabla5 Clientes 
                {

                    while (reader.Read())
                    {
                        contador = contador + 1;
                        // Creamos una tabla        
                        PdfPTable table = new PdfPTable(3);
                        table.WidthPercentage = 100;

                        //Fila 1
                        PdfPCell clContrato = new PdfPCell(new Phrase(" Contrato: \t" + reader[0].ToString(), titulo8));
                        clContrato.BorderColor = BaseColor.WHITE;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContrato.UseVariableBorders = true;
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                        clContrato.BorderColor = BaseColor.BLACK;
                        table.AddCell(clContrato);

                        PdfPCell clPeriodo = new PdfPCell(new Phrase(" Periodo: \t" + reader[11].ToString(), titulo8));
                        clPeriodo.BorderColor = BaseColor.WHITE;
                        clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clPeriodo.UseVariableBorders = true;
                        clPeriodo.Border = Rectangle.TOP_BORDER;
                        clPeriodo.BorderColor = BaseColor.BLACK;
                        table.AddCell(clPeriodo);

                        PdfPCell clPaquete = new PdfPCell(new Phrase(" Paquetes con Cortesía: \t" + reader[13].ToString(), titulo8));
                        clPaquete.BorderColor = BaseColor.WHITE;
                        clPaquete.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPaquete.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clPaquete.UseVariableBorders = true;
                        clPaquete.Border = Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPaquete.BorderColor = BaseColor.BLACK;
                        table.AddCell(clPaquete);

                        //Colspan fila 2 de 2 columnas 

                        PdfPCell clNombre = new PdfPCell(new Phrase(" Nombre: \t" + reader[1].ToString(), titulo8));
                        clNombre.Colspan = 2;
                        clNombre.BorderColor = BaseColor.WHITE;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_LEFT; //HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.LEFT_BORDER;
                        clNombre.BorderColor = BaseColor.BLACK;
                        table.AddCell(clNombre);

                        PdfPCell clSin = new PdfPCell(new Phrase(" Paquetes sin Cortesía: \t" + reader[12].ToString(), titulo8));
                        clSin.BorderColor = BaseColor.WHITE;
                        clSin.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSin.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSin.UseVariableBorders = true;
                        clSin.Border = Rectangle.RIGHT_BORDER;
                        clSin.BorderColor = BaseColor.BLACK;
                        table.AddCell(clSin);

                        //Fila 3 de 2 columnas

                        PdfPCell celDire = new PdfPCell(new Phrase(" Dirección: \t" + reader[17].ToString(), titulo8));
                        celDire.Colspan = 2;
                        celDire.BorderColor = BaseColor.WHITE;
                        celDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        celDire.HorizontalAlignment = PdfPCell.ALIGN_LEFT; //HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        celDire.UseVariableBorders = true;
                        celDire.Border = Rectangle.LEFT_BORDER;
                        celDire.BorderColor = BaseColor.BLACK;
                        table.AddCell(celDire);

                        PdfPCell clTotalCable = new PdfPCell(new Phrase(" Total de Cablemodems: \t" + reader[14].ToString(), titulo8));
                        clTotalCable.BorderColor = BaseColor.WHITE;
                        clTotalCable.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalCable.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clTotalCable.UseVariableBorders = true;
                        clTotalCable.Border = Rectangle.RIGHT_BORDER;
                        clTotalCable.BorderColor = BaseColor.BLACK;
                        table.AddCell(clTotalCable);

                        //Fila 4
                        PdfPCell cellTres = new PdfPCell(new Phrase(" "));
                        cellTres.Colspan = 2;
                        cellTres.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cellTres.UseVariableBorders = true;
                        cellTres.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        cellTres.BorderColor = BaseColor.BLACK;
                        table.AddCell(cellTres);

                        PdfPCell clTelefono = new PdfPCell(new Phrase(" Teléfono: \t" + reader[5].ToString(), titulo8));
                        clTelefono.BorderColor = BaseColor.WHITE;
                        clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefono.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clTelefono.UseVariableBorders = true;
                        clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clTelefono.BorderColor = BaseColor.BLACK;
                        table.AddCell(clTelefono);


                        //*************************
                        Paragraph macAddress = new Paragraph(" Mac Address Cablemodem: \t" + reader[10].ToString(), titulo8);
                        macAddress.Alignment = Element.ALIGN_LEFT;
                        //***********************
                        // Creamos una tabla2
                        float[] columnWidths4 = { 2, 2, 1, 2, 1, 1 };    //ancho de las columnas
                        PdfPTable table2 = new PdfPTable(columnWidths4);
                        table2.WidthPercentage = 100;

                        //Fila 1

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        table2.AddCell(clVacia);

                        PdfPCell clServ = new PdfPCell(new Phrase("Servicio", titulo8));
                        clServ.UseVariableBorders = true;
                        clServ.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clServ.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clServ.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.BorderColorBottom = BaseColor.BLACK;
                        table2.AddCell(clServ);

                        PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8));
                        clStatus.UseVariableBorders = true;
                        clStatus.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.BorderColorBottom = BaseColor.BLACK;
                        table2.AddCell(clStatus);

                        PdfPCell clFechaCortesia = new PdfPCell(new Phrase("Fecha de la Cortesía", titulo8));
                        clFechaCortesia.UseVariableBorders = true;
                        clFechaCortesia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFechaCortesia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFechaCortesia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaCortesia.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaCortesia.BorderColorBottom = BaseColor.BLACK;
                        table2.AddCell(clFechaCortesia);

                        PdfPCell clActiva = new PdfPCell(new Phrase("Activa", titulo8));
                        clActiva.UseVariableBorders = true;
                        clActiva.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clActiva.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clActiva.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clActiva.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clActiva.BorderColor = BaseColor.BLACK;
                        table2.AddCell(clActiva);
                        table2.AddCell(clVacia);

                        //Fila 2
                        table2.AddCell(clVacia);

                        PdfPCell clServ2 = new PdfPCell(new Phrase(reader[16].ToString(), titulo8));
                        clServ2.UseVariableBorders = true;
                        clServ2.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clServ2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ2.BorderColor = BaseColor.WHITE;
                        table2.AddCell(clServ2);

                        PdfPCell clStatus2 = new PdfPCell(new Phrase(reader[7].ToString(), titulo8));
                        clStatus2.UseVariableBorders = true;
                        clStatus2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus2.BorderColor = BaseColor.WHITE;
                        table2.AddCell(clStatus2);

                        PdfPCell clFechaCortesia2 = new PdfPCell(new Phrase(reader[8].ToString(), titulo8));
                        clFechaCortesia2.UseVariableBorders = true;
                        clFechaCortesia2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFechaCortesia2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaCortesia2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaCortesia2.BorderColor = BaseColor.WHITE;
                        table2.AddCell(clFechaCortesia2);

                        PdfPCell clActiva2 = new PdfPCell(new Phrase(reader[9].ToString(), titulo8));
                        clActiva2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clActiva2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clActiva2.UseVariableBorders = true;
                        clActiva2.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER;
                        clActiva2.BorderColor = BaseColor.WHITE;
                        table2.AddCell(clActiva2);
                        table2.AddCell(clVacia);

                        //Añadimos la tabla
                        doc.Add(table); //tabla 1
                        doc.Add(salto);
                        doc.Add(macAddress);
                        doc.Add(salto);
                        doc.Add(table2);
                        doc.Add(salto);

                    }
                }
                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CLIENTES           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }




        //ReporteCiudad_2_c
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_CU(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);


            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 2, 1, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla 5
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) // tabla 6
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); //
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
                    }

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    if (baja == 0)
                    {
                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clDireccion);
                    }
                    else if (baja == 1)
                    {
                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        clDireccion.Colspan = 2;
                        tblPrueba.AddCell(clDireccion);
                    }


                    if (baja == 0)
                    {
                        PdfPCell clTelefono1 = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                        clTelefono1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTelefono1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefono1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefono1.UseVariableBorders = true;
                        clTelefono1.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clTelefono1);

                        PdfPCell clTelefono2 = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                        clTelefono2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTelefono2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefono2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefono2.UseVariableBorders = true;
                        clTelefono2.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clTelefono2);
                    }

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.UseVariableBorders = true;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    if (baja == 0)
                    {
                        PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
                        clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.UseVariableBorders = true;
                        clPeriodo.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPeriodo);
                    }
                    else if (baja == 1)
                    {
                        PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de Cancelación", titulo8Bold));
                        clFecha.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        clFecha.Colspan = 2;
                        tblPrueba.AddCell(clFecha);

                        PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                        clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPlaca);
                    }


                    if (conect == 1)
                    {
                        PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de Contratación", titulo8Bold));
                        clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clFecha);
                    }

                    if (insta == 1)
                    {
                        PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de Instalación", titulo8Bold));
                        clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clFecha);
                    }
                    else if (desco == 1 || susp == 1)
                    {
                        //no hay campo de fecha
                        PdfPCell clFecha = new PdfPCell(new Phrase(" ", titulo8Bold));
                        clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clFecha);
                    }
                    else if (fuera == 1)
                    {
                        PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de Área", titulo8Bold));
                        clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clFecha);
                    }
                    else if (desctemp == 1)
                    {
                        PdfPCell clFecha = new PdfPCell(new Phrase("Fecha de Corte", titulo8Bold));
                        clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFecha.UseVariableBorders = true;
                        clFecha.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clFecha);
                    }

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 8;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 8;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);

                }
                if (reader.NextResult()) //tabla4 Clientes 
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;
                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clContrato = new PdfPCell(new Phrase(reader[2].ToString(), titulo8));
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.UseVariableBorders = true;
                        clContrato.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clContrato);

                        PdfPCell clNombre = new PdfPCell(new Phrase(reader[3].ToString(), titulo8));
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNombre);

                        if (baja == 0)
                        {
                            PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                            clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.UseVariableBorders = true;
                            clDireccion.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clDireccion);
                        }
                        else if (baja == 1)
                        {
                            PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                            clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.UseVariableBorders = true;
                            clDireccion.BorderColor = BaseColor.WHITE;
                            clDireccion.Colspan = 2;
                            tblPrueba.AddCell(clDireccion);
                        }


                        if (baja == 0)
                        {
                            PdfPCell clTelefono1 = new PdfPCell(new Phrase(reader[14].ToString(), titulo8));
                            clTelefono1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTelefono1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTelefono1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTelefono1.UseVariableBorders = true;
                            clTelefono1.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTelefono1);

                            PdfPCell clTelefono2 = new PdfPCell(new Phrase(reader[15].ToString(), titulo8));
                            clTelefono2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTelefono2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTelefono2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTelefono2.UseVariableBorders = true;
                            clTelefono2.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTelefono2);
                        }

                        PdfPCell clStatus = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clStatus);

                        if (baja == 0)
                        {
                            PdfPCell clPeriodo = new PdfPCell(new Phrase(reader[16].ToString(), titulo8));
                            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.UseVariableBorders = true;
                            clPeriodo.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPeriodo);
                        }
                        else if (baja == 1)
                        {
                            PdfPCell clFecha = new PdfPCell(new Phrase(reader[10].ToString(), titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            clFecha.Colspan = 2;
                            tblPrueba.AddCell(clFecha);

                            PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                            clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.UseVariableBorders = true;
                            clPlaca.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPlaca);
                        }


                        if (conect == 1)
                        {
                            PdfPCell clFecha = new PdfPCell(new Phrase(reader[11].ToString(), titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clFecha);
                        }

                        if (insta == 1)
                        {
                            PdfPCell clFecha = new PdfPCell(new Phrase(reader[8].ToString(), titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clFecha);
                        }
                        else if (desco == 1 || susp == 1)
                        {
                            //no hay campo de fecha
                            PdfPCell clFecha = new PdfPCell(new Phrase(" ", titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clFecha);
                        }
                        else if (fuera == 1)
                        {
                            PdfPCell clFecha = new PdfPCell(new Phrase(reader[9].ToString(), titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clFecha);
                        }
                        else if (desctemp == 1)
                        {
                            PdfPCell clFecha = new PdfPCell(new Phrase(reader[7].ToString(), titulo8));
                            clFecha.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFecha.UseVariableBorders = true;
                            clFecha.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clFecha);
                        }
                        // Añadimos las celdas a la tabla                   

                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);


                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }


        //ReporteCiudad_2_E
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_E(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);


            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 2, 1, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;



            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.CommandTimeout = 60;
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult())
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {

                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); ;//
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

                    }
                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    if (desco == 0 && baja == 0)   // if ((desco == 0) || (desco == 1 && baja == 1))
                    {
                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        clDireccion.Colspan = 3;
                        tblPrueba.AddCell(clDireccion);
                    }
                    else if (desco == 1 || baja == 1)
                    {
                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        clDireccion.Colspan = 1;
                        tblPrueba.AddCell(clDireccion);
                    }


                    if (desco == 1 && baja == 0)
                    {
                        PdfPCell clTel1 = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                        clTel1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.UseVariableBorders = true;
                        clTel1.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clTel1);

                        PdfPCell clTel2 = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                        clTel2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTel2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel2.UseVariableBorders = true;
                        clTel2.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clTel2);
                    }

                    if (desco == 0 && baja == 1)
                    {
                        PdfPCell clTel1 = new PdfPCell(new Phrase("Teléfonos", titulo8Bold));
                        clTel1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.UseVariableBorders = true;
                        clTel1.BorderColor = BaseColor.BLACK;
                        clTel1.Colspan = 2;
                        tblPrueba.AddCell(clTel1);
                    }// else if (desco == 1 && baja == 1)


                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.UseVariableBorders = true;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    if ((desco == 0 && baja == 0) || (desco == 1 && baja == 0))
                    {
                        PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
                        clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.UseVariableBorders = true;
                        clPeriodo.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPeriodo);
                    }
                    else if (desco == 0 && baja == 1)
                    {
                        PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                        clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.BLACK;
                        clPlaca.Colspan = 2;
                        tblPrueba.AddCell(clPlaca);
                    }
                    else if (desco == 1 && baja == 1)
                    {
                        PdfPCell clFechaDe = new PdfPCell(new Phrase("Fecha de Corte", titulo8Bold));
                        clFechaDe.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFechaDe.UseVariableBorders = true;
                        clFechaDe.BorderColor = BaseColor.BLACK;
                        clFechaDe.Colspan = 2;
                        tblPrueba.AddCell(clFechaDe);

                        PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                        clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.BLACK;
                        clPlaca.Colspan = 2;
                        tblPrueba.AddCell(clPlaca);
                    }

                    if ((desco == 0 && baja == 0))
                    {
                        PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                        clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPlaca);
                    }
                    else if (desco == 1 && baja == 0)
                    {
                        if (conect == 1)
                        {
                            PdfPCell clFechaDe = new PdfPCell(new Phrase("Fecha de Contratación", titulo8Bold));
                            clFechaDe.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.UseVariableBorders = true;
                            clFechaDe.BorderColor = BaseColor.BLACK;
                            //clFechaDe.Colspan = 1;
                            tblPrueba.AddCell(clFechaDe);
                        }
                        else
                        {
                            PdfPCell clFechaDe = new PdfPCell(new Phrase("Fecha de Corte", titulo8Bold));
                            clFechaDe.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.UseVariableBorders = true;
                            clFechaDe.BorderColor = BaseColor.BLACK;
                            //clFechaDe.Colspan = 2;
                            tblPrueba.AddCell(clFechaDe);
                        }
                    }

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 8;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 8;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                }
                if (reader.NextResult()) //tabla4 Clientes 
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;
                        // reader[2].ToString()
                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clContrato = new PdfPCell(new Phrase(reader[2].ToString(), titulo8));
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.UseVariableBorders = true;
                        clContrato.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clContrato);

                        PdfPCell clNombre = new PdfPCell(new Phrase(reader[3].ToString(), titulo8));
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNombre);

                        if (desco == 0 && baja == 0)   // if ((desco == 0) || (desco == 1 && baja == 1))
                        {
                            PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                            clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.UseVariableBorders = true;
                            clDireccion.BorderColor = BaseColor.WHITE;
                            clDireccion.Colspan = 3;
                            tblPrueba.AddCell(clDireccion);
                        }
                        else if (desco == 1 || baja == 1)
                        {
                            PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                            clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clDireccion.UseVariableBorders = true;
                            clDireccion.BorderColor = BaseColor.WHITE;
                            clDireccion.Colspan = 1;
                            tblPrueba.AddCell(clDireccion);
                        }


                        if (desco == 1 && baja == 0)
                        {
                            PdfPCell clTel1 = new PdfPCell(new Phrase(reader[14].ToString(), titulo8));
                            clTel1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.UseVariableBorders = true;
                            clTel1.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel1);

                            PdfPCell clTel2 = new PdfPCell(new Phrase(reader[15].ToString(), titulo8));
                            clTel2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.UseVariableBorders = true;
                            clTel2.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel2);
                        }

                        if (desco == 0 && baja == 1)
                        {
                            PdfPCell clTel1 = new PdfPCell(new Phrase(reader[14].ToString(), titulo8));
                            clTel1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.UseVariableBorders = true;
                            clTel1.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel1);

                            PdfPCell clTel2 = new PdfPCell(new Phrase(reader[15].ToString(), titulo8));
                            clTel2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.UseVariableBorders = true;
                            clTel2.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel2);
                        }// else if (desco == 1 && baja == 1)


                        PdfPCell clStatus = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clStatus);

                        if ((desco == 0 && baja == 0) || (desco == 1 && baja == 0))
                        {
                            PdfPCell clPeriodo = new PdfPCell(new Phrase(reader[16].ToString(), titulo8));
                            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.UseVariableBorders = true;
                            clPeriodo.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPeriodo);
                        }
                        else if (desco == 0 && baja == 1)
                        {
                            PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                            clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.UseVariableBorders = true;
                            clPlaca.BorderColor = BaseColor.WHITE;
                            clPlaca.Colspan = 2;
                            tblPrueba.AddCell(clPlaca);
                        }
                        else if (desco == 1 && baja == 1)
                        {
                            PdfPCell clFechaDe = new PdfPCell(new Phrase(reader[7].ToString(), titulo8));
                            clFechaDe.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clFechaDe.UseVariableBorders = true;
                            clFechaDe.BorderColor = BaseColor.WHITE;
                            clFechaDe.Colspan = 2;
                            tblPrueba.AddCell(clFechaDe);

                            PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                            clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.UseVariableBorders = true;
                            clPlaca.BorderColor = BaseColor.WHITE;
                            clPlaca.Colspan = 2;
                            tblPrueba.AddCell(clPlaca);
                        }

                        if ((desco == 0 && baja == 0))
                        {
                            PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                            clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPlaca.UseVariableBorders = true;
                            clPlaca.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPlaca);
                        }
                        else if (desco == 1 && baja == 0)
                        {
                            if (conect == 1)
                            {
                                PdfPCell clFechaDe = new PdfPCell(new Phrase(reader[11].ToString(), titulo8));
                                clFechaDe.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                                clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                clFechaDe.UseVariableBorders = true;
                                clFechaDe.BorderColor = BaseColor.WHITE;
                                //clFechaDe.Colspan = 1;
                                tblPrueba.AddCell(clFechaDe);
                            }
                            else
                            {
                                PdfPCell clFechaDe = new PdfPCell(new Phrase(reader[7].ToString(), titulo8));
                                clFechaDe.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                                clFechaDe.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                clFechaDe.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                clFechaDe.UseVariableBorders = true;
                                clFechaDe.BorderColor = BaseColor.WHITE;
                                //clFechaDe.Colspan = 2;
                                tblPrueba.AddCell(clFechaDe);
                            }
                        }

                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de todas las ciudades
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);

                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);
                    }
                }

            }
            //************
            doc.Close();
            writer.Close();
            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));


            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //ReporteCiudad_2
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_B(string cadena, int idConexion)
        { //TRES FILTROS
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 12, 12, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);


            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 2, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            comandoSql.CommandTimeout = 60;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);

                        Paragraph fecha = new Paragraph(fechaVal, titulo10);
                        fecha.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(fecha);
                        doc.Add(salto);
                    }
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult())
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]);
                    }
                    //  String fechaDe = " ";
                    //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.UseVariableBorders = true;
                    clDireccion.BorderColor = BaseColor.BLACK;

                    if (baja == 0)
                    {
                        clDireccion.Colspan = 2;
                    }

                    tblPrueba.AddCell(clDireccion);

                    if (baja == 1)
                    {

                        PdfPCell clTelefonos = new PdfPCell(new Phrase("Teléfonos", titulo8Bold));
                        clTelefonos.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTelefonos.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefonos.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefonos.UseVariableBorders = true;
                        clTelefonos.BorderColor = BaseColor.BLACK;
                        clTelefonos.Colspan = 2;
                        tblPrueba.AddCell(clTelefonos);

                        PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clStatus);
                    }
                    else if (baja == 0)
                    {
                        PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clStatus);

                        PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
                        clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.UseVariableBorders = true;
                        clPeriodo.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPeriodo);
                    }

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 7;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 7;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);
                }

                if (reader.NextResult()) //tabla4 Clientes 
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clContrato = new PdfPCell(new Phrase(reader[2].ToString(), titulo8));
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.UseVariableBorders = true;
                        clContrato.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clContrato);

                        PdfPCell clNombre = new PdfPCell(new Phrase(reader[3].ToString(), titulo8));
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNombre);

                        PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.WHITE;

                        if (baja == 0)
                        {
                            clDireccion.Colspan = 2;
                        }

                        tblPrueba.AddCell(clDireccion);

                        if (baja == 1)
                        {

                            PdfPCell clTel1 = new PdfPCell(new Phrase(reader[14].ToString(), titulo8));
                            clTel1.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel1.UseVariableBorders = true;
                            clTel1.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel1);

                            PdfPCell clTel2 = new PdfPCell(new Phrase(reader[15].ToString(), titulo8));
                            clTel2.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clTel2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clTel2.UseVariableBorders = true;
                            clTel2.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clTel2);

                            PdfPCell clStatus = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                            clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clStatus.UseVariableBorders = true;
                            clStatus.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clStatus);
                        }
                        else if (baja == 0)
                        {
                            PdfPCell clStatus = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                            clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clStatus.UseVariableBorders = true;
                            clStatus.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clStatus);

                            PdfPCell clPeriodo = new PdfPCell(new Phrase(reader[16].ToString(), titulo8));
                            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.UseVariableBorders = true;
                            clPeriodo.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPeriodo);
                        }

                        PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                        clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlaca);
                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);

                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }
            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //ReporteCiudad_2
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_A(string cadena, int idConexion)
        { //TODOS LOS FILTROS
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 13, 13, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);


            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 2, 1, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            comandoSql.CommandTimeout = 60;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);

                        Paragraph fecha = new Paragraph(fechaVal, titulo10);
                        fecha.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(fecha);
                        doc.Add(salto);
                    }
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult())
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {

                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); ;//
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.UseVariableBorders = true;
                        clContrato.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clContrato);

                        PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clNombre);

                        PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clDireccion);


                        PdfPCell clTelefonos = new PdfPCell(new Phrase("Telefonos", titulo8Bold));
                        clTelefonos.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clTelefonos.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefonos.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTelefonos.UseVariableBorders = true;
                        clTelefonos.BorderColor = BaseColor.BLACK;
                        clTelefonos.Colspan = 2;
                        tblPrueba.AddCell(clTelefonos);

                        PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clStatus);

                        PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                        clPlaca.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPlaca);
                    }

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 7;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 7;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                }
                if (reader.NextResult()) //tabla4 Clientes 
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;

                        PdfPCell clCont = new PdfPCell(new Phrase(reader[2].ToString(), titulo8));
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[3].ToString(), titulo8));
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.UseVariableBorders = true;
                        clNom.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clNom);

                        PdfPCell clDire = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                        clDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.UseVariableBorders = true;
                        clDire.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clDire);

                        PdfPCell clTel1 = new PdfPCell(new Phrase("Tel. " + reader[14].ToString(), titulo8));
                        clTel1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel1.UseVariableBorders = true;
                        clTel1.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clTel1);

                        PdfPCell clTel2 = new PdfPCell(new Phrase("Cel. " + reader[15].ToString(), titulo8));
                        clTel2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel2.UseVariableBorders = true;
                        clTel2.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clTel2);

                        PdfPCell clSta = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                        clSta.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.UseVariableBorders = true;
                        clSta.BorderColor = BaseColor.WHITE; //los 4 bordes
                        tblPrueba.AddCell(clSta);

                        PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                        clPlaca.Border = Rectangle.RIGHT_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlaca);

                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);

                        //fila 5
                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes por todas las ciudades 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //ReporteCiudad_2 INTERNET
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_Internet(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);


            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 2, 3, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult())
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) //
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); ;//
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
                    }

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.UseVariableBorders = true;
                    clDireccion.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clDireccion);

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.UseVariableBorders = true;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    if (baja == 0)
                    {
                        PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
                        clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPeriodo.UseVariableBorders = true;
                        clPeriodo.BorderColor = BaseColor.BLACK;
                        tblPrueba.AddCell(clPeriodo);
                    }

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    if (baja == 1)
                    {
                        clPlaca.Colspan = 2;
                    }
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);
                }
                if (reader.NextResult()) //tabla4 Clientes 
                {
                    while (reader.Read())
                    {
                        PdfPCell clContrato = new PdfPCell(new Phrase(reader[2].ToString(), titulo8));
                        clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clContrato.UseVariableBorders = true;
                        clContrato.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clContrato);

                        PdfPCell clNombre = new PdfPCell(new Phrase(reader[3].ToString(), titulo8));
                        clNombre.UseVariableBorders = true;
                        clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNombre.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNombre);

                        PdfPCell clDireccion = new PdfPCell(new Phrase(reader[18].ToString(), titulo8));
                        clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDireccion.UseVariableBorders = true;
                        clDireccion.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clDireccion);

                        PdfPCell clStatus = new PdfPCell(new Phrase(reader[6].ToString(), titulo8));
                        clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clStatus.UseVariableBorders = true;
                        clStatus.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clStatus);

                        if (baja == 0)
                        {
                            PdfPCell clPeriodo = new PdfPCell(new Phrase(reader[16].ToString(), titulo8));
                            clPeriodo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clPeriodo.UseVariableBorders = true;
                            clPeriodo.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clPeriodo);
                        }

                        PdfPCell clPlaca = new PdfPCell(new Phrase(reader[17].ToString(), titulo8));
                        clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlaca.UseVariableBorders = true;
                        clPlaca.BorderColor = BaseColor.WHITE;
                        if (baja == 1)
                        {
                            clPlaca.Colspan = 2;
                        }
                        tblPrueba.AddCell(clPlaca);
                    }

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        //TABLA GRAN TOTAL DE CLIENTES           
                        float[] columnWidths2 = { 2, 1, 2 };
                        PdfPTable tableResumen = new PdfPTable(columnWidths2);
                        tableResumen.WidthPercentage = 100;

                        //centroArriba
                        PdfPCell clCentroArriba1 = new PdfPCell(new Phrase(" "));
                        clCentroArriba1.BorderColor = BaseColor.BLACK;
                        clCentroArriba1.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba1.UseVariableBorders = true;
                        clCentroArriba1.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro1 = new PdfPCell(new Phrase(" "));
                        clCentro1.BorderColor = BaseColor.WHITE;
                        clCentro1.BackgroundColor = BaseColor.WHITE;
                        clCentro1.UseVariableBorders = true;
                        clCentro1.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        PdfPCell clInstalados1 = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados1.BorderColor = BaseColor.BLACK;
                        clInstalados1.BackgroundColor = BaseColor.WHITE;
                        clInstalados1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados1.UseVariableBorders = true;
                        clInstalados1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
                        tableResumen.AddCell(clInstalados1);

                        tableResumen.AddCell(clCentroArriba1);

                        PdfPCell clContratados1 = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados1.BorderColor = BaseColor.BLACK;
                        clContratados1.BackgroundColor = BaseColor.WHITE;
                        clContratados1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados1.UseVariableBorders = true;
                        clContratados1.Border = Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        tableResumen.AddCell(clContratados1);

                        //Fila2
                        PdfPCell clDesconect1 = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect1.BorderColor = BaseColor.BLACK;
                        clDesconect1.BackgroundColor = BaseColor.WHITE;
                        clDesconect1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect1.UseVariableBorders = true;
                        clDesconect1.Border = Rectangle.LEFT_BORDER;
                        tableResumen.AddCell(clDesconect1);

                        tableResumen.AddCell(clCentro1);

                        PdfPCell clSuspen1 = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen1.BorderColor = BaseColor.BLACK;
                        clSuspen1.BackgroundColor = BaseColor.WHITE;
                        clSuspen1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen1.UseVariableBorders = true;
                        clSuspen1.Border = Rectangle.RIGHT_BORDER;
                        tableResumen.AddCell(clSuspen1);

                        //Fila3
                        PdfPCell clBajas1 = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas1.BorderColor = BaseColor.BLACK;
                        clBajas1.BackgroundColor = BaseColor.WHITE;
                        clBajas1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas1.UseVariableBorders = true;
                        clBajas1.Border = Rectangle.LEFT_BORDER;
                        tableResumen.AddCell(clBajas1);

                        tableResumen.AddCell(clCentro1);

                        PdfPCell clFA1 = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA1.BorderColor = BaseColor.BLACK;
                        clFA1.BackgroundColor = BaseColor.WHITE;
                        clFA1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA1.UseVariableBorders = true;
                        clFA1.Border = Rectangle.RIGHT_BORDER;
                        tableResumen.AddCell(clFA1);

                        //Fila 4
                        PdfPCell clST1 = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST1.BorderColor = BaseColor.BLACK;
                        clST1.BackgroundColor = BaseColor.WHITE;
                        clST1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST1.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST1.UseVariableBorders = true;
                        clST1.Border = Rectangle.LEFT_BORDER;
                        tableResumen.AddCell(clST1);

                        tableResumen.AddCell(clCentro1);

                        PdfPCell clVacia1 = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia1.BorderColor = BaseColor.BLACK;
                        clVacia1.BackgroundColor = BaseColor.WHITE;
                        clVacia1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia1.UseVariableBorders = true;
                        clVacia1.Border = Rectangle.RIGHT_BORDER;
                        tableResumen.AddCell(clVacia1);

                        //fila 5
                        PdfPCell clTotalClientesPC1 = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC1.Colspan = 3;
                        clTotalClientesPC1.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC1.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC1.UseVariableBorders = true;
                        clTotalClientesPC1.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        tableResumen.AddCell(clTotalClientesPC1);

                        doc.Add(tableResumen);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //Reporte_Resumen_Por_Colonia
        [ValidateInput(false)]
        public ActionResult Reporte_Resumen_Por_Colonia(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 27, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 3, 2, 2, 2, 1, 2, 2, 2, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre de la Colonia", titulo8Bold));
            clNombre.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.UseVariableBorders = true;
            clNombre.BorderColorBottom = BaseColor.BLACK;
            clNombre.BorderColorLeft = BaseColor.BLACK;

            PdfPCell clInstalados = new PdfPCell(new Phrase("Instalados", titulo8Bold));
            clInstalados.UseVariableBorders = true;
            clInstalados.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clInstalados.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clInstalados.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clDesconectados = new PdfPCell(new Phrase("Desconectados", titulo8Bold));
            clDesconectados.UseVariableBorders = true;
            clDesconectados.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clDesconectados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clDesconectados.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clDesconectados.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clContratados = new PdfPCell(new Phrase("Contratados", titulo8Bold));
            clContratados.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContratados.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContratados.UseVariableBorders = true;
            clContratados.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clBajas = new PdfPCell(new Phrase("Bajas", titulo8Bold));
            clBajas.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clBajas.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clBajas.UseVariableBorders = true;
            clBajas.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clFuera = new PdfPCell(new Phrase("Fuera de Área", titulo8Bold));
            clFuera.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clFuera.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clFuera.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clFuera.UseVariableBorders = true;
            clFuera.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clSuspendidos = new PdfPCell(new Phrase("Suspendidos", titulo8Bold));
            clSuspendidos.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clSuspendidos.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clSuspendidos.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clSuspendidos.UseVariableBorders = true;
            clSuspendidos.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clSuspTtempo = new PdfPCell(new Phrase("Susp. Tempo", titulo8Bold));
            clSuspTtempo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clSuspTtempo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clSuspTtempo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clSuspTtempo.UseVariableBorders = true;
            clSuspTtempo.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clTotal = new PdfPCell(new Phrase("Total", titulo8Bold));
            clTotal.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTotal.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTotal.UseVariableBorders = true;
            clTotal.BorderColorBottom = BaseColor.BLACK;
            clTotal.BorderColorRight = BaseColor.BLACK;

            // tblPrueba.DefaultCell.Border = Rectangle.NO_BORDER;

            // Añadimos las celdas a la tabla            
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clInstalados);
            tblPrueba.AddCell(clDesconectados);
            tblPrueba.AddCell(clContratados);
            tblPrueba.AddCell(clBajas);
            tblPrueba.AddCell(clFuera);
            tblPrueba.AddCell(clSuspendidos);
            tblPrueba.AddCell(clSuspTtempo);
            tblPrueba.AddCell(clTotal);


            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad_Resumen @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String nombreEmpresa = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    // Escribimos el encabezamiento en el documento
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Sucursal
                {
                    Paragraph titulo = new Paragraph("Listado de Clientes por Colonia y Status", titulo10);
                    titulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(titulo);

                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }

                if (reader.NextResult()) //tabla3 query principal 
                {
                    while (reader.Read())
                    {
                        contador = contador + 1;

                        //tblPrueba.AddCell(reader[0].ToString());
                        for (int i = 1; i <= 9; i++)
                        {
                            PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo8));

                            celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            celda.UseVariableBorders = true;
                            celda.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(celda);
                        }
                    }
                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto  
                }
            }
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));


            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }




        //Reporte_Rango_Fechas_Tv_2
        [ValidateInput(false)]
        public ActionResult Reporte_Rango_Fechas_Tv_2(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            // Creamos una tabla
            float[] columnWidths = { 1, 3, 2, 1, 1, 1 };    //ancho de las columnas 
            PdfPTable tblPrueba = new PdfPTable(columnWidths);
            tblPrueba.WidthPercentage = 100;
            // tblPrueba
            tblPrueba.HeaderRows = 1;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
            clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clContrato.UseVariableBorders = true;
            clContrato.BorderColorBottom = BaseColor.BLACK;
            clContrato.BorderColorLeft = BaseColor.BLACK;

            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
            clNombre.UseVariableBorders = true;
            clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clNombre.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clServicio = new PdfPCell(new Phrase("Servicio", titulo8Bold));
            clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clServicio.UseVariableBorders = true;
            clServicio.BorderColorBottom = BaseColor.BLACK;


            PdfPCell clTelefono = new PdfPCell(new Phrase("Telefono", titulo8Bold));
            clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clTelefono.UseVariableBorders = true;
            clTelefono.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clCelular = new PdfPCell(new Phrase("Celular", titulo8Bold));
            clCelular.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clCelular.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clCelular.UseVariableBorders = true;
            clCelular.BorderColorBottom = BaseColor.BLACK;

            PdfPCell clPeriodo = new PdfPCell(new Phrase("Periodo", titulo8Bold));
            clPeriodo.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            clPeriodo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            clPeriodo.UseVariableBorders = true;
            clPeriodo.BorderColorBottom = BaseColor.BLACK;
            clPeriodo.BorderColorRight = BaseColor.BLACK;

            // tblPrueba.DefaultCell.Border = Rectangle.NO_BORDER;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clContrato);
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clServicio);
            tblPrueba.AddCell(clTelefono);
            tblPrueba.AddCell(clCelular);
            tblPrueba.AddCell(clPeriodo);

            //************

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposClienteTv_nuevo @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String subtitulo = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }

                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }
                }
                if (reader.NextResult()) //tabla3 subtitulo
                {
                    while (reader.Read())
                    {
                        subtitulo = reader.GetString(0);
                        Paragraph subtituloP = new Paragraph(subtitulo, titulo10);
                        subtituloP.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtituloP);
                    }
                }
                if (reader.NextResult()) //tabla 4 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph sucursal = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        sucursal.Alignment = Element.ALIGN_CENTER;
                        doc.Add(sucursal);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 5 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla5 query principal 
                {
                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contador = contador + 1;

                        //tblPrueba.AddCell(reader[0].ToString());
                        for (int i = 0; i <= 6; i++)
                        {
                            if (i != 3)
                            {
                                PdfPCell celda = new PdfPCell(new Phrase(reader[i].ToString(), titulo8));

                                celda.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                                celda.UseVariableBorders = true;
                                celda.BorderColor = BaseColor.WHITE;
                                tblPrueba.AddCell(celda);
                            }
                        }

                    }
                    doc.Add(tblPrueba);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);

                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CLIENTES           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contador.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);

        }



        //*********************************************** POR COLONIA Y CALLE


        [ValidateInput(false)]
        public ActionResult ReportePIInt_2_Colonias(string cadena, int idConexion)
        { //TRES FILTROS
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 12, 12, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_nuevo2 @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            comandoSql.CommandTimeout = 50;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            int contadorTotal = 0, clvColonia = 0;

            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);

                        Paragraph fecha = new Paragraph(fechaVal, titulo10);
                        fecha.Alignment = Element.ALIGN_RIGHT;
                        doc.Add(fecha);
                        doc.Add(salto);
                    }
                }
                if (reader.NextResult()) //tabla 4 clvReporte
                {
                    //while (reader.Read())
                    //{
                    //    clvReporte = reader.GetString(0);
                    //}
                }
                if (reader.NextResult()) //tabla 5 ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //Tabla 6 query
                {

                    // Creamos una tabla
                    float[] columnWidths = { 1, 3, 2, 1 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre del Cliente", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clServicio = new PdfPCell(new Phrase("Servicio(s)", titulo8Bold));
                    clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.UseVariableBorders = true;
                    clServicio.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clServicio);

                    PdfPCell clTelefono = new PdfPCell(new Phrase("Teléfono", titulo8Bold));
                    clTelefono.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTelefono.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTelefono.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clTelefono.UseVariableBorders = true;
                    clTelefono.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clTelefono);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 4;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 4;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[3]))
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 4;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[8].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 3;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[0].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[1].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNom);

                        PdfPCell clServ = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clServ.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clServ.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.UseVariableBorders = true;
                        clServ.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clServ);

                        PdfPCell clTel = new PdfPCell(new Phrase(reader[4].ToString(), titulo7));
                        clTel.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clTel.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTel.UseVariableBorders = true;
                        clTel.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clTel);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 4;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //Tabla 7 count ciudad 
                {
                    while (reader.Read())
                    {
                        //ancho de las columnas
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
                //TABLA GRAN TOTAL DE CLIENTES       

                float[] columnWidths2 = { 1 };    //ancho de las columnas
                PdfPTable tblClientes = new PdfPTable(columnWidths2);
                tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
                tblClientes.WidthPercentage = 100;

                PdfPCell clGran = new PdfPCell(new Phrase("Gran Total de Clientes: " + contadorTotal.ToString(), titulo8Bold));
                clGran.PaddingLeft = 50f;

                tblClientes.AddCell(clGran);
                doc.Add(tblClientes);
            }
            //************
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //Reporte_Rango_Fechas_Tv_2 contrataciones, instalaciones, cancelaciones, fueras de area
        [ValidateInput(false)]
        public ActionResult Reporte_Rango_Fechas_Tv_2_Colonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");


            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposClienteTv_nuevo @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String subtitulo = " ";
            String nombreSucursal = " ";
            int contadorTotal = 0, clvColonia = 0;
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }

                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }
                }
                if (reader.NextResult()) //tabla3 subtitulo
                {
                    while (reader.Read())
                    {
                        subtitulo = reader.GetString(0);
                        Paragraph subtituloP = new Paragraph(subtitulo, titulo10);
                        subtituloP.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtituloP);
                    }
                }
                if (reader.NextResult()) //tabla 4 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph sucursal = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        sucursal.Alignment = Element.ALIGN_CENTER;
                        doc.Add(sucursal);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 5 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla5 query principal 
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 3, 2 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre del Cliente", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clServicio = new PdfPCell(new Phrase("Servicio(s)", titulo8Bold));
                    clServicio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clServicio.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clServicio.UseVariableBorders = true;
                    clServicio.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clServicio);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 4;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 4;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[3]))
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total de Clientes: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 4;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[8].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 3;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[0].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[1].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNom);

                        PdfPCell clServ = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clServ.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clServ.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clServ.UseVariableBorders = true;
                        clServ.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clServ);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 4;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla6 total por ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);

                    }
                }
            }
            //************
            //TABLA GRAN TOTAL DE CLIENTES           
            PdfPTable tblClientes = new PdfPTable(1);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contadorTotal.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }



        //Orden 2 (por colonia y calle)
        [ValidateInput(false)]
        public ActionResult Reportes_varios_Fechas_2_Colonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_nuevo1 @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0, contadorTotal = 0, clvColonia = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            if (reader.HasRows)
            {

                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }
                }
                if (reader.NextResult()) //tabla 3 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);

                    }

                }
                if (reader.NextResult()) //tabla 4 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }
                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla5 Query 
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 3 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre del Cliente", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 4;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 4;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[3]))
                        {
                            clvColonia = Convert.ToInt32(reader[3]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total de Clientes: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 4;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[8].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 3;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[0].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[1].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNom.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNom);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 4;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 6 count ciudad
                {
                    while (reader.Read())
                    {
                        PdfPTable tblResumenC = new PdfPTable(2);
                        tblResumenC.HorizontalAlignment = Element.ALIGN_LEFT;
                        tblResumenC.WidthPercentage = 100;

                        PdfPCell clResumen = new PdfPCell(new Phrase("RESUMEN DE LA CIUDAD " + reader[0].ToString(), titulo8Bold));
                        clResumen.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clResumen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clResumen.UseVariableBorders = true;
                        clResumen.BorderColor = BaseColor.BLACK;
                        clResumen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clResumen.Colspan = 2;
                        tblResumenC.AddCell(clResumen);

                        PdfPCell clTotal = new PdfPCell(new Phrase("Total de Clientes: ", titulo8Bold));
                        clTotal.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        clTotal.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotal.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clTotal.UseVariableBorders = true;
                        clTotal.BorderColor = BaseColor.BLACK;
                        clTotal.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clTotal);

                        PdfPCell clNum = new PdfPCell(new Phrase(reader[2].ToString(), titulo8Bold));
                        clNum.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                        clNum.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNum.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNum.UseVariableBorders = true;
                        clNum.BorderColor = BaseColor.BLACK;
                        clNum.BackgroundColor = BaseColor.LIGHT_GRAY;
                        tblResumenC.AddCell(clNum);

                        doc.Add(tblResumenC);
                        doc.Add(salto);
                    }
                }
            }
            //TABLA GRAN TOTAL DE CLIENTES   
            float[] columnWidths2 = { 1 };    //ancho de las columnas

            PdfPTable tblClientes = new PdfPTable(columnWidths2);
            tblClientes.HorizontalAlignment = Element.ALIGN_LEFT;
            tblClientes.WidthPercentage = 100;

            PdfPCell clGranTotal = new PdfPCell(new Phrase("Gran Total de Clientes: " + contadorTotal.ToString(), titulo8Bold));
            clGranTotal.PaddingLeft = 50f;

            tblClientes.AddCell(clGranTotal);
            doc.Add(tblClientes);
            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }

        //ReporteCiudad_2  --internet todos los filtros excepto baja
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_InternetColonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0, contadorTotal = 0, clvColonia = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) // tabla 5
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult())
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) //tabla6 Query
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 3, 3, 1, 1 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.UseVariableBorders = true;
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clDireccion);

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.UseVariableBorders = true;
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 5;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 5;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[4]))
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 6;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[19].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 5;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[3].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNom);

                        PdfPCell clDire = new PdfPCell(new Phrase(reader[18].ToString(), titulo7));
                        clDire.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.UseVariableBorders = true;
                        clDire.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clDire);

                        PdfPCell clSta = new PdfPCell(new Phrase(reader[6].ToString(), titulo7));
                        clSta.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clSta.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.UseVariableBorders = true;
                        clSta.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clSta);

                        PdfPCell clPlac = new PdfPCell(new Phrase(reader[17].ToString(), titulo7));
                        clPlac.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlac.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.UseVariableBorders = true;
                        clPlac.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlac);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 6;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 7 Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }


        //ReporteCiudad_2_Cable - colonia y calle - 1 filtro
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_CU_CableColonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int contador = 0, contadorTotal = 0, clvColonia = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla 5
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) // tabla 6
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); ;//
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
                    }

                }
                /////////
                if (reader.NextResult()) //tabla6 Query
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 2, 3, 1, 2, 1 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clNombre);

                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.UseVariableBorders = true;
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clDireccion);

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.UseVariableBorders = true;
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    String fechas = " ";

                    if (conect == 1)
                    {
                        fechas = "Fecha de Contratación";
                    }
                    else if (fuera == 1)
                    {
                        fechas = "Fecha de Fuera de Área";
                    }
                    else if (susp == 1 || desco == 1)
                    {
                        fechas = " ";
                    }
                    else if (insta == 1)
                    {
                        fechas = "Fecha de Instalación";
                    }
                    else if (baja == 1)
                    {
                        fechas = "Fecha de Cancelación";
                    }
                    else if (desctemp == 1)
                    {
                        fechas = "Fecha de Corte";
                    }

                    PdfPCell clFecha = new PdfPCell(new Phrase(fechas, titulo8Bold));
                    clFecha.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.UseVariableBorders = true;
                    clFecha.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clFecha);

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 6;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 6;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[4]))
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 6;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[19].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 6;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Configuramos el título de las columnas de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[3].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clNom);

                        PdfPCell clDire = new PdfPCell(new Phrase(reader[18].ToString(), titulo7));
                        clDire.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.UseVariableBorders = true;
                        clDire.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clDire);

                        PdfPCell clSta = new PdfPCell(new Phrase(reader[6].ToString(), titulo7));
                        clSta.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clSta.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.UseVariableBorders = true;
                        clSta.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clSta);

                        String fechaReader = " ";
                        if (conect == 1)
                        {
                            fechaReader = reader[11].ToString();
                        }
                        else if (fuera == 1)
                        {
                            fechaReader = reader[9].ToString();
                        }
                        else if (susp == 1 || desco == 1)
                        {
                            fechaReader = " ";
                        }
                        else if (insta == 1)
                        {
                            fechaReader = reader[8].ToString();
                        }
                        else if (baja == 1)
                        {
                            fechaReader = reader[10].ToString();
                        }
                        else if (desctemp == 1)
                        {
                            fechaReader = reader[7].ToString();
                        }


                        PdfPCell clFech = new PdfPCell(new Phrase(fechaReader, titulo7));
                        clFech.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFech.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.UseVariableBorders = true;
                        clFech.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clFech);

                        PdfPCell clPlac = new PdfPCell(new Phrase(reader[17].ToString(), titulo7));
                        clPlac.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlac.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.UseVariableBorders = true;
                        clPlac.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlac);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 6;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 7 Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }


        //ReporteCiudad_2_Cable - colonia y calle - 2 filtros
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_E_CableColonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);

            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int sumaFiltro = 0, contador = 0, contadorTotal = 0, clvColonia = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla 5
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) // tabla 6
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); //
                        sumaFiltro = conect + baja + insta + desco + susp + fuera + desctemp;
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
                    }

                }
                /////////
                if (reader.NextResult()) //tabla6 Query
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;
                    if (desco == 0)
                    {
                        clNombre.Colspan = 3;
                    }
                    else if (desco == 1 && sumaFiltro == 2)
                    {
                        clNombre.Colspan = 2;
                    }
                    else if (desco == 1 && sumaFiltro >= 3)
                    {
                        clNombre.Colspan = 3;
                    }
                    tblPrueba.AddCell(clNombre);


                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.UseVariableBorders = true;
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.BorderColor = BaseColor.BLACK;
                    if (desco == 0)
                    {
                        clDireccion.Colspan = 4;
                    }
                    else if (desco == 1 && sumaFiltro == 2)
                    {
                        clDireccion.Colspan = 3;
                    }
                    else if (desco == 1 && sumaFiltro >= 3)
                    {
                        clDireccion.Colspan = 4;
                    }
                    tblPrueba.AddCell(clDireccion);

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.UseVariableBorders = true;
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    String fechas = " ";
                    if (conect == 1 && (fuera == 1 || susp == 1 || insta == 1 || baja == 1 || desctemp == 1))
                    {
                        fechas = " ";
                    }
                    else if (desco == 1 && (fuera == 1 || susp == 1 || insta == 1 || baja == 1 || desctemp == 1))
                    {
                        fechas = "Fecha de Corte";
                    }
                    else if (conect == 1 && desco == 1) //solo dos filtros
                    {
                        fechas = "Fecha de Contratación";
                    }

                    PdfPCell clFecha = new PdfPCell(new Phrase(fechas, titulo8Bold));
                    clFecha.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.UseVariableBorders = true;
                    clFecha.BorderColor = BaseColor.BLACK;
                    if (desco == 1 && sumaFiltro == 2)
                    {
                        clFecha.Colspan = 2;
                        tblPrueba.AddCell(clFecha);
                    }

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 10;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 10;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);

                    while (reader.Read())
                    {
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[4]))
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total Clientes por Colonia: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 10;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }
                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[19].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 10;
                            tblPrueba.AddCell(clColonia);
                        }

                        // Llenado de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[3].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNom.BorderColor = BaseColor.WHITE;
                        if (desco == 0)
                        {
                            clNom.Colspan = 3;
                        }
                        else if (desco == 1 && sumaFiltro == 2)
                        {
                            clNom.Colspan = 2;
                        }
                        else if (desco == 1 && sumaFiltro >= 3)
                        {
                            clNom.Colspan = 3;
                        }
                        tblPrueba.AddCell(clNom);

                        PdfPCell clDire = new PdfPCell(new Phrase(reader[18].ToString(), titulo7));
                        clDire.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.UseVariableBorders = true;
                        clDire.BorderColor = BaseColor.WHITE;
                        if (desco == 0)
                        {
                            clDire.Colspan = 4;
                        }
                        else if (desco == 1 && sumaFiltro == 2)
                        {
                            clDire.Colspan = 3;
                        }
                        else if (desco == 1 && sumaFiltro >= 3)
                        {
                            clDire.Colspan = 4;
                        }
                        tblPrueba.AddCell(clDire);

                        PdfPCell clSta = new PdfPCell(new Phrase(reader[6].ToString(), titulo7));
                        clSta.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clSta.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.UseVariableBorders = true;
                        clSta.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clSta);

                        String fechaReader = " ";
                        if (conect == 1 && (fuera == 1 || susp == 1 || insta == 1 || baja == 1 || desctemp == 1))
                        {
                            fechaReader = " ";
                        }
                        else if (desco == 1 && (fuera == 1 || susp == 1 || insta == 1 || baja == 1 || desctemp == 1))
                        {
                            fechaReader = reader[7].ToString();
                        }
                        else if (conect == 1 && desco == 1)
                        {
                            fechaReader = reader[11].ToString();
                        }


                        PdfPCell clFech = new PdfPCell(new Phrase(fechaReader, titulo7));
                        clFech.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFech.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.UseVariableBorders = true;
                        clFech.BorderColor = BaseColor.WHITE;
                        if (desco == 1 && sumaFiltro == 2)
                        {
                            clFech.Colspan = 2;
                            tblPrueba.AddCell(clFech);
                        }

                        PdfPCell clPlac = new PdfPCell(new Phrase(reader[17].ToString(), titulo7));
                        clPlac.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlac.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.UseVariableBorders = true;
                        clPlac.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlac);

                        contador = contador + 1;
                    }
                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total Clientes por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 10;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 7 Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }


        //ReporteCiudad_2_Cable - colonia y calle - Baja
        [ValidateInput(false)]
        public ActionResult ReporteCiudad_2_Baja_CableColonias(string cadena, int idConexion)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER, 15, 15, 30, 30);

            // Indicamos donde vamos a guardar el documento
            //var output = new FileStream(Server.MapPath("/Reportes/pdfejemplo.pdf"), FileMode.Create);
            string fileName = Server.MapPath("/Reportes/") + Guid.NewGuid().ToString() + ".pdf";
            var output = new FileStream(fileName, FileMode.Create);
            PdfWriter writer = PdfWriter.GetInstance(doc, output);

            // Abrimos el archivo
            doc.Open();

            // Creamos el tipo de Font que vamos utilizar
            iTextSharp.text.Font titulo13 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo10 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo8Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo7 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            iTextSharp.text.Font titulo7Bold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font titulo5 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            Paragraph salto = new Paragraph(" ", titulo5);

            //fecha
            DateTime now = DateTime.Now;
            String fechaVal = now.ToString("D");

            SqlConnection conexionSQL = new SqlConnection(c.DameConexion(idConexion));
            try
            {
                conexionSQL.Open();
            }
            catch
            {

            }
            SqlCommand comandoSql = new SqlCommand("EXEC Reporte_TiposCliente_Ciudad @reporteXml");
            comandoSql.Parameters.AddWithValue("@reporteXml", cadena);
            comandoSql.CommandTimeout = 60;
            comandoSql.Connection = conexionSQL;
            SqlDataReader reader = comandoSql.ExecuteReader(); //obtiene todas las tablas del query

            int sumaFiltro = 0, contador = 0, contadorTotal = 0, clvColonia = 0;
            int clvMotCan = 0, totalMotCan = 0;
            String ciudades = " ";
            String nombreEmpresa = " ";
            String nombreReporte = " ";
            String nombreSucursal = " ";
            String fechaDe = " ";
            int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;

            if (reader.HasRows)
            {
                while (reader.Read()) //tabla1 Empresa
                {
                    nombreEmpresa = reader.GetString(0);
                    Paragraph empresa = new Paragraph(nombreEmpresa, titulo13);
                    empresa.Alignment = Element.ALIGN_CENTER;
                    doc.Add(empresa);
                }
                if (reader.NextResult()) //tabla2 Reporte
                {
                    while (reader.Read())
                    {
                        nombreReporte = reader.GetString(0);
                        Paragraph titulo = new Paragraph(nombreReporte, titulo10);
                        titulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(titulo);
                    }

                }
                if (reader.NextResult()) //tabla 3 Sucursal
                {
                    while (reader.Read())
                    {
                        nombreSucursal = reader.GetString(0);
                        Paragraph subtitulo = new Paragraph("SUCURSAL: " + nombreSucursal, titulo10);
                        subtitulo.Alignment = Element.ALIGN_CENTER;
                        doc.Add(subtitulo);
                    }

                    Paragraph fecha = new Paragraph(fechaVal, titulo10);
                    fecha.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(fecha);
                    doc.Add(salto);
                }
                if (reader.NextResult()) //tabla 4 Ciudades
                {
                    while (reader.Read())
                    {
                        ciudades = reader.GetString(0);
                    }
                }
                if (reader.NextResult()) //tabla 5
                {
                    //no trae datos de esta tabla, hasta la siguiente
                }
                if (reader.NextResult()) // tabla 6
                {
                    while (reader.Read())
                    {
                        fechaDe = reader[0].ToString();
                        conect = Convert.ToInt32(reader[1]);
                        baja = Convert.ToInt32(reader[2]);
                        insta = Convert.ToInt32(reader[3]);
                        desco = Convert.ToInt32(reader[4]);
                        susp = Convert.ToInt32(reader[5]);
                        fuera = Convert.ToInt32(reader[6]);
                        desctemp = Convert.ToInt32(reader[7]); //
                        sumaFiltro = conect + baja + insta + desco + susp + fuera + desctemp;
                        //  String fechaDe = " ";
                        //int conect = 0, baja = 0, insta = 0, desco = 0, susp = 0, fuera = 0, desctemp = 0;
                    }
                }
                if (reader.NextResult()) //tabla6 Query
                {
                    // Creamos una tabla
                    float[] columnWidths = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };    //ancho de las columnas 
                    PdfPTable tblPrueba = new PdfPTable(columnWidths);
                    tblPrueba.WidthPercentage = 100;
                    // tblPrueba
                    tblPrueba.HeaderRows = 1;

                    // Configuramos el título de las columnas de la tabla
                    PdfPCell clContrato = new PdfPCell(new Phrase("Contrato", titulo8Bold));
                    clContrato.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clContrato.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clContrato.UseVariableBorders = true;
                    clContrato.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clContrato);

                    PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", titulo8Bold));
                    clNombre.UseVariableBorders = true;
                    clNombre.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clNombre.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clNombre.BorderColor = BaseColor.BLACK;

                    if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                    {
                        clNombre.Colspan = 2;
                    }
                    else //if (sumaFiltro >= 3)
                    {
                        clNombre.Colspan = 3;
                    }
                    tblPrueba.AddCell(clNombre);


                    PdfPCell clDireccion = new PdfPCell(new Phrase("Dirección", titulo8Bold));
                    clDireccion.UseVariableBorders = true;
                    clDireccion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clDireccion.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clDireccion.BorderColor = BaseColor.BLACK;
                    if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                    {
                        clDireccion.Colspan = 3;
                    }
                    else //if (sumaFiltro >= 3)
                    {
                        clDireccion.Colspan = 4;
                    }
                    tblPrueba.AddCell(clDireccion);

                    PdfPCell clStatus = new PdfPCell(new Phrase("Status", titulo8Bold));
                    clStatus.UseVariableBorders = true;
                    clStatus.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clStatus.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clStatus.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clStatus);

                    String fechas = " ";

                    if (sumaFiltro == 1)
                    {
                        fechas = "Fecha de Cancelación";
                    }
                    if (sumaFiltro == 2)
                    {
                        fechas = "Fecha de Corte";
                    }

                    PdfPCell clFecha = new PdfPCell(new Phrase(fechas, titulo8Bold));
                    clFecha.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFecha.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clFecha.UseVariableBorders = true;
                    clFecha.BorderColor = BaseColor.BLACK;

                    if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                    {
                        clFecha.Colspan = 2;
                        tblPrueba.AddCell(clFecha);
                    }
                    else // if (sumaFiltro >= 3)
                    {
                        //clDireccion.Colspan = 4;
                    }

                    PdfPCell clPlaca = new PdfPCell(new Phrase("Placa", titulo8Bold));
                    clPlaca.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clPlaca.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clPlaca.UseVariableBorders = true;
                    clPlaca.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clPlaca);

                    //-------Ciudades
                    PdfPCell clFilaVacia = new PdfPCell(new Phrase(" ", titulo5));
                    clFilaVacia.UseVariableBorders = true;
                    clFilaVacia.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clFilaVacia.BorderColor = BaseColor.WHITE;
                    clFilaVacia.FixedHeight = 4f;
                    clFilaVacia.Colspan = 10;
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clCiudades = new PdfPCell(new Phrase(ciudades, titulo10));
                    clCiudades.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                    clCiudades.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    clCiudades.UseVariableBorders = true;
                    clCiudades.BorderWidth = 1f;
                    clCiudades.Colspan = 10;
                    clCiudades.BorderColor = BaseColor.BLACK;
                    tblPrueba.AddCell(clCiudades);
                    tblPrueba.AddCell(clFilaVacia);


                    while (reader.Read())
                    { //

                        //
                        contadorTotal = contadorTotal + 1;

                        if (contador == 0)
                        {
                            clvColonia = Convert.ToInt32(reader[4]);
                        }

                        if (clvColonia != Convert.ToInt32(reader[4]))
                        {
                            PdfPCell clTotalMotivo2 = new PdfPCell(new Phrase("  Total Clientes por Motivo de Cancelación: " + totalMotCan, titulo8Bold));
                            clTotalMotivo2.Border = Rectangle.TOP_BORDER;
                            clTotalMotivo2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalMotivo2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalMotivo2.UseVariableBorders = true;
                            clTotalMotivo2.BorderColor = BaseColor.BLACK;
                            clTotalMotivo2.Colspan = 10;
                            tblPrueba.AddCell(clTotalMotivo2);
                            tblPrueba.AddCell(clFilaVacia);

                            clvColonia = Convert.ToInt32(reader[4]);
                            // Configuramos el título de las columnas de la tabla
                            PdfPCell clTotalCol = new PdfPCell(new Phrase("  Total Clientes por Colonia: " + contador, titulo8Bold));
                            //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                            clTotalCol.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalCol.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalCol.UseVariableBorders = true;
                            clTotalCol.BorderColor = BaseColor.BLACK;
                            clTotalCol.Colspan = 10;
                            tblPrueba.AddCell(clTotalCol);

                            contador = 0;
                        }

                        if (contador == 0)
                        {
                            PdfPCell clVacia = new PdfPCell(new Phrase(" "));
                            clVacia.Border = Rectangle.LEFT_BORDER;
                            clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clVacia.UseVariableBorders = true;
                            clVacia.BorderColor = BaseColor.WHITE;
                            tblPrueba.AddCell(clVacia);

                            PdfPCell clColonia = new PdfPCell(new Phrase("Nombre de la Colonia: " + reader[19].ToString(), titulo8Bold));
                            clColonia.Border = Rectangle.LEFT_BORDER;
                            clColonia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clColonia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clColonia.UseVariableBorders = true;
                            clColonia.BorderColor = BaseColor.WHITE;
                            clColonia.Colspan = 10;
                            tblPrueba.AddCell(clColonia);

                            //1 MOTIVO CANCELACION
                            clvMotCan = Convert.ToInt32(reader[13]);
                            PdfPCell clMotivo = new PdfPCell(new Phrase(reader[20].ToString(), titulo10));
                            clMotivo.Border = Rectangle.BOTTOM_BORDER;
                            clMotivo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clMotivo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clMotivo.UseVariableBorders = true;
                            clMotivo.BorderColor = BaseColor.BLACK;
                            clMotivo.Colspan = 10;
                            tblPrueba.AddCell(clMotivo);
                        }

                        if (clvMotCan != Convert.ToInt32(reader[13]))
                        {
                            //Imprimir el total del motivo de cancelacion
                            PdfPCell clTotalMotivo1 = new PdfPCell(new Phrase("  Total Clientes por Motivo de Cancelación " + totalMotCan, titulo8Bold));
                            clTotalMotivo1.Border = Rectangle.TOP_BORDER;
                            clTotalMotivo1.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clTotalMotivo1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            clTotalMotivo1.UseVariableBorders = true;
                            clTotalMotivo1.BorderColor = BaseColor.BLACK;
                            clTotalMotivo1.Colspan = 10;
                            tblPrueba.AddCell(clTotalMotivo1);
                            tblPrueba.AddCell(clFilaVacia);

                            //2 MOTIVO CANCELACION
                            PdfPCell clMotivo = new PdfPCell(new Phrase(reader[20].ToString(), titulo10));
                            clMotivo.Border = Rectangle.BOTTOM_BORDER;
                            clMotivo.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                            clMotivo.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                            clMotivo.UseVariableBorders = true;
                            clMotivo.BorderColor = BaseColor.BLACK;
                            clMotivo.Colspan = 10;
                            tblPrueba.AddCell(clMotivo);
                            clvMotCan = Convert.ToInt32(reader[13]);
                        }


                        // Llenado de la tabla
                        PdfPCell clCont = new PdfPCell(new Phrase(reader[2].ToString(), titulo7));
                        clCont.Border = Rectangle.LEFT_BORDER;
                        clCont.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clCont.UseVariableBorders = true;
                        clCont.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clCont);

                        PdfPCell clNom = new PdfPCell(new Phrase(reader[3].ToString(), titulo7));
                        clNom.UseVariableBorders = true;
                        clNom.Border = Rectangle.BOTTOM_BORDER;
                        clNom.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clNom.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clNom.BorderColor = BaseColor.WHITE;
                        if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                        {
                            clNom.Colspan = 2;
                        }
                        else //if (sumaFiltro >= 3)
                        {
                            clNom.Colspan = 3;
                        }
                        tblPrueba.AddCell(clNom);

                        PdfPCell clDire = new PdfPCell(new Phrase(reader[18].ToString(), titulo7));
                        clDire.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clDire.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clDire.UseVariableBorders = true;
                        clDire.BorderColor = BaseColor.WHITE;
                        if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                        {
                            clDire.Colspan = 3;
                        }
                        else //if (sumaFiltro >= 3)
                        {
                            clDire.Colspan = 4;
                        }
                        tblPrueba.AddCell(clDire);

                        PdfPCell clSta = new PdfPCell(new Phrase(reader[6].ToString(), titulo7));
                        clSta.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clSta.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clSta.UseVariableBorders = true;
                        clSta.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clSta);

                        String fechaReader = " ";

                        if (sumaFiltro == 1)
                        {
                            fechaReader = reader[10].ToString();
                        }
                        else if (sumaFiltro == 2 && desco == 1 && baja == 1)
                        {
                            fechaReader = reader[7].ToString();
                        }

                        PdfPCell clFech = new PdfPCell(new Phrase(fechaReader, titulo7));
                        clFech.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                        clFech.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clFech.UseVariableBorders = true;
                        clFech.BorderColor = BaseColor.WHITE;

                        if (sumaFiltro == 1 || (sumaFiltro == 2 && desco == 1 && baja == 1))
                        {
                            clFech.Colspan = 2;
                            tblPrueba.AddCell(clFech);
                        }

                        PdfPCell clPlac = new PdfPCell(new Phrase(reader[17].ToString(), titulo7));
                        clPlac.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                        clPlac.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clPlac.UseVariableBorders = true;
                        clPlac.BorderColor = BaseColor.WHITE;
                        tblPrueba.AddCell(clPlac);

                        contador = contador + 1;
                        totalMotCan = Convert.ToInt32(reader[21]);
                    }

                    PdfPCell clTotalMotivo3 = new PdfPCell(new Phrase("  Total Clientes por Motivo de Cancelación " + totalMotCan, titulo8Bold));
                    clTotalMotivo3.Border = Rectangle.TOP_BORDER;
                    clTotalMotivo3.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalMotivo3.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalMotivo3.UseVariableBorders = true;
                    clTotalMotivo3.BorderColor = BaseColor.BLACK;
                    clTotalMotivo3.Colspan = 10;
                    tblPrueba.AddCell(clTotalMotivo3);
                    tblPrueba.AddCell(clFilaVacia);

                    PdfPCell clTotalCol2 = new PdfPCell(new Phrase("  Total Clientes por Colonia: " + contador, titulo8Bold));
                    //  clTotalCol.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER;
                    clTotalCol2.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    clTotalCol2.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    clTotalCol2.UseVariableBorders = true;
                    clTotalCol2.BorderColor = BaseColor.BLACK;
                    clTotalCol2.Colspan = 10;
                    tblPrueba.AddCell(clTotalCol2);

                    doc.Add(tblPrueba);
                    doc.Add(salto);//salto
                }
                if (reader.NextResult()) //tabla 7 Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenClientesCiu = new Paragraph("RESUMEN DE CLIENTES DE LA CIUDAD: " + reader.GetString(8), titulo8Bold);
                        resumenClientesCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenClientesCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.LIGHT_GRAY;
                        clCentro.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.LIGHT_GRAY;
                        clDesconect.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.LIGHT_GRAY;
                        clSuspen.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.LIGHT_GRAY;
                        clBajas.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.LIGHT_GRAY;
                        clFA.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.LIGHT_GRAY;
                        clST.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.LIGHT_GRAY;
                        clVacia.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Total Clientes por Ciudad: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.LIGHT_GRAY;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                        doc.Add(salto);//salto                    
                    }
                }
                if (reader.NextResult()) //Resumen de clientes de la ciudad 
                {
                    while (reader.Read())
                    {
                        Paragraph resumenTodasCiu = new Paragraph("RESUMEN DE CLIENTES POR TODAS LAS CIUDADES: " + ciudades, titulo8Bold);
                        resumenTodasCiu.Alignment = Element.ALIGN_CENTER;
                        doc.Add(resumenTodasCiu);
                        doc.Add(salto);//salto

                        // Creamos una tabla       
                        float[] columnWidths3 = { 1, 3, 1, 3, 1 };    //ancho de las columnas
                        PdfPTable table = new PdfPTable(columnWidths3);
                        table.WidthPercentage = 100;

                        //Rowspan
                        PdfPCell clDer = new PdfPCell(new Phrase(" "));
                        clDer.Rowspan = 5;
                        clDer.BorderColor = BaseColor.BLACK;
                        clDer.BackgroundColor = BaseColor.WHITE;
                        clDer.UseVariableBorders = true;
                        clDer.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //Rowspan
                        PdfPCell clIzq = new PdfPCell(new Phrase(" "));
                        clIzq.Rowspan = 5;
                        clIzq.BorderColor = BaseColor.BLACK;
                        clIzq.BackgroundColor = BaseColor.WHITE;
                        clIzq.UseVariableBorders = true;
                        clIzq.Border = Rectangle.RIGHT_BORDER | Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;


                        //centroArriba
                        PdfPCell clCentroArriba = new PdfPCell(new Phrase(" "));
                        clCentroArriba.BorderColor = BaseColor.BLACK;
                        clCentroArriba.BackgroundColor = BaseColor.WHITE;
                        clCentroArriba.UseVariableBorders = true;
                        clCentroArriba.Border = Rectangle.TOP_BORDER;

                        PdfPCell clCentro = new PdfPCell(new Phrase(" "));
                        clCentro.BorderColor = BaseColor.WHITE;
                        clCentro.BackgroundColor = BaseColor.WHITE;
                        clCentro.UseVariableBorders = true;
                        clCentro.Border = Rectangle.TOP_BORDER;

                        //Fila 1
                        table.AddCell(clDer);

                        PdfPCell clInstalados = new PdfPCell(new Phrase(" Instalados: \t" + reader[0].ToString(), titulo8));
                        clInstalados.BorderColor = BaseColor.BLACK;
                        clInstalados.BackgroundColor = BaseColor.WHITE;
                        clInstalados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clInstalados.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clInstalados.UseVariableBorders = true;
                        clInstalados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clInstalados);

                        table.AddCell(clCentroArriba);

                        PdfPCell clContratados = new PdfPCell(new Phrase(" Contratados: \t" + reader[2].ToString(), titulo8));
                        clContratados.BorderColor = BaseColor.BLACK;
                        clContratados.BackgroundColor = BaseColor.WHITE;
                        clContratados.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clContratados.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clContratados.UseVariableBorders = true;
                        clContratados.Border = Rectangle.TOP_BORDER;
                        table.AddCell(clContratados);

                        table.AddCell(clIzq);

                        //Fila2

                        PdfPCell clDesconect = new PdfPCell(new Phrase(" Desconectados: \t" + reader[1].ToString(), titulo8));
                        clDesconect.BorderColor = BaseColor.WHITE;
                        clDesconect.BackgroundColor = BaseColor.WHITE;
                        clDesconect.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clDesconect.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clDesconect.UseVariableBorders = true;
                        clDesconect.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clDesconect);

                        table.AddCell(clCentro);

                        PdfPCell clSuspen = new PdfPCell(new Phrase(" Suspendidos: \t" + reader[5].ToString(), titulo8));
                        clSuspen.BorderColor = BaseColor.WHITE;
                        clSuspen.BackgroundColor = BaseColor.WHITE;
                        clSuspen.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clSuspen.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clSuspen.UseVariableBorders = true;
                        clSuspen.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clSuspen);

                        //Fila3

                        PdfPCell clBajas = new PdfPCell(new Phrase(" Bajas: \t" + reader[3].ToString(), titulo8));
                        clBajas.BorderColor = BaseColor.WHITE;
                        clBajas.BackgroundColor = BaseColor.WHITE;
                        clBajas.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clBajas.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clBajas.UseVariableBorders = true;
                        clBajas.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clBajas);

                        table.AddCell(clCentro);

                        PdfPCell clFA = new PdfPCell(new Phrase(" Fuera de Área: \t" + reader[4].ToString(), titulo8));
                        clFA.BorderColor = BaseColor.WHITE;
                        clFA.BackgroundColor = BaseColor.WHITE;
                        clFA.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clFA.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clFA.UseVariableBorders = true;
                        clFA.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clFA);

                        //Fila 4

                        PdfPCell clST = new PdfPCell(new Phrase(" Suspendidos Temporales: \t" + reader[6].ToString(), titulo8));
                        clST.BorderColor = BaseColor.WHITE;
                        clST.BackgroundColor = BaseColor.WHITE;
                        clST.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clST.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                        clST.UseVariableBorders = true;
                        clST.Border = Rectangle.LEFT_BORDER;
                        table.AddCell(clST);

                        table.AddCell(clCentro);

                        PdfPCell clVacia = new PdfPCell(new Phrase(" ", titulo8));
                        clVacia.BorderColor = BaseColor.WHITE;
                        clVacia.BackgroundColor = BaseColor.WHITE;
                        clVacia.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clVacia.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                        clVacia.UseVariableBorders = true;
                        clVacia.Border = Rectangle.RIGHT_BORDER;
                        table.AddCell(clVacia);


                        //fila 5

                        PdfPCell clTotalClientesPC = new PdfPCell(new Phrase("Gran Total de Clientes: " + reader[7].ToString(), titulo8Bold));
                        clTotalClientesPC.Colspan = 3;
                        clTotalClientesPC.BorderColor = BaseColor.BLACK;
                        clTotalClientesPC.BackgroundColor = BaseColor.WHITE;
                        clTotalClientesPC.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        clTotalClientesPC.UseVariableBorders = true;
                        clTotalClientesPC.Border = Rectangle.BOTTOM_BORDER;
                        table.AddCell(clTotalClientesPC);

                        doc.Add(table);
                    }
                }

            }
            //************

            doc.Close();
            writer.Close();

            //*************
            PdfReader rd = new PdfReader(fileName);
            String nombreArchivo2 = Guid.NewGuid().ToString() + ".pdf";
            string fileName2 = Server.MapPath("/Reportes/") + nombreArchivo2;
            PdfStamper ps = new PdfStamper(rd, new FileStream(fileName2, FileMode.Create));

            PdfImportedPage page;
            for (int i = 1; i <= rd.NumberOfPages; i++)
            {
                PdfContentByte canvas = ps.GetOverContent(i);
                page = ps.GetImportedPage(rd, i);
                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                canvas.SetColorFill(BaseColor.DARK_GRAY);
                canvas.BeginText();
                canvas.SetFontAndSize(bf, 9);

                canvas.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Página " + i.ToString() + " de " + rd.NumberOfPages, 550.7f, 20.7f, 0);

                canvas.EndText();
                canvas.AddTemplate(page, 0, 0);

            }
            ps.Close();
            return Content("Reportes/" + nombreArchivo2);
        }
    }


}