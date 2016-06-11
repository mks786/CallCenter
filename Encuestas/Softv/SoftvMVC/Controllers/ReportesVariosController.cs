

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

namespace SoftvMVC.Controllers
{
    public class ReportesVariosController : BaseController
    {


        public ActionResult Index()
        {
            return View();
        }

        //Reportes_varios_Fechas_2
        public ActionResult Reportes_varios_Fechas_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:8px; font-family:Arial;"">
                            
                            <table  border='0' cellpadding=2 cellspacing=0>
                                <tr>
                                    <td><h5 style=""font-size:14px;""><b>@Empresa</b></h5></td>
                                </tr>
                                <tr>
                                    <td><h5 style=""font-size:12px;""><b>@Titulo</b></h5></td>
                                </tr>
                                <tr>
                                    <td> <h5 style=""font-size:12px;"">@SubTitulo</h5></td>
                                </tr>
                                <tr>
                                    <td><h5 style=""font-size:10px;"" align=""right"">PrintDate</h5></td>
                                </tr>
                            </table>                            
                            <br>
                            <table style=""font-size:10px; font-weight:bold; text-decoration: underline;"">
                                <tr>
                                    <td><h5>Contrato</h5></td>
                                    <td><h5>Nombre</h5></td>
                                    <td><h5>Telefono</h5></td>
                                    <td><h5>Celular</h5></td>
                                    <td><h5>Periodo</h5></td>
                                </tr>
                            </table>
                                <br>
                            <div>                                  
                                <table class='red' border='3' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                <h5 style=""font-size:14px;""><b>Nombre</b></h5>
                                <table class='red' border='3' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>                              
                                <table>
                                    <tr>
                                       <td><h5 style=""font-size:8px;"">Contrato</h5></td>
                                       <td align=""left"" ><h5 style=""font-size:8px;"">Nombre</h5></td>
                                       <td><h5 style=""font-size:8px;"">TELEFONO</h5></td>
                                       <td><h5 style=""font-size:8px;"">CELULAR</h5></td>
                                       <td><h5 style=""font-size:8px;"">Periodo</h5></td>
                                       </tr>                               
                                </table>
                            </div>
                                <br>
                                <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                    <tr><td>                     
                                            <table border='0' cellpadding=2 cellspacing=0>  
                                                <tr>
                                                    <td colspan='2' align='center'>
                                                        <h5>RESUMEN DE LA CIUDAD {1Nombre}</h5>
                                                    </td>
                                                </tr>                                              
                                                <tr>
                                                    <td align='right'>
                                                        <h5>Total De Clientes: </h5></td>
                                                    <td align='left'>
                                                        <h5>#RTotal0</h5></td>
                                                </tr>
                                            </table>
                                    </td></tr>
                                </table>     
 
                                <br>                        
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr><td>
                                        <table border='0' style='font-size:10px;' cellpadding=5 cellspacing=0 >
                                                <tr>
                                                    <td colspan='1' align='center'><h5>Gran Total de Clientes: </h5></td>
                                                    <td colspan='2' align='left'><h5>Count of Reporte_TiposCliente_nuevo1;1.Contrato </h5></td>
                                                    <td colspan='2'></td>
                                                </tr>
                                        </table>
                                   </td></tr>
                                </table>

                                <div align=""right"">
                                    <h5>Page N of M</h5>
                                </div>                                
                        </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "Reportes_varios_Fechas_2.pdf");
        }



        //REportePorPagarTv
        public ActionResult REportePorPagarTv(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 50, 50, 50, 50);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">
                            <table  border='0' cellpadding=2 cellspacing=0>
                                <tr>
                                    <td><h5 style=""font-size:14px;""><b>@Empresa</b></h5></td>
                                </tr>
                                <tr>
                                    <td><h5 style=""font-size:14px;""><b>@Titulo</b></h5></td>
                                </tr>
                                <tr>
                                    <td><h5 style=""font-size:14px;"">@SubTitulo</h5></td>
                                </tr>
                                <tr>
                                    <td><h5 style=""font-size:10px;"" align=""right"">PrintDate</h5> </td>
                                </tr>
                            </table>                            
                            <br> 
                            <div>                              
                                <table style=""font-weight:bold;"">
                                    <tr>
                                        <td><h5>Contrato</h5></td>
                                        <td colspan='2' ><h5>Nombre</h5></td>
                                        <td><h5>Último Año</h5></td>
                                        <td><h5>Servicio</h5></td>
                                        <td><h5>Teléfono</h5></td>
                                        <td><h5>Celular</h5></td>
                                    </tr>
                                </table>
                            </div>
                            <br>                                 
                            <table border='3' bordercolor='black' cellpadding=2 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                            <h5 style=""font-size:14px;"">Nombre</h5>
                            <table class='red' border='3' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                
                            <table style='font-size:10px; font-weight:bold;' border='0' cellpadding=2 cellspacing=2>
                                <tr><td>
                                    <table border='0' cellpadding=2 cellspacing=0>                                               
                                        <tr>
                                            <td colspan=""2"" align=""right"">
                                                        <h5>Nombre de la Colonia:  </h5></td>
                                            <td colspan=""4"" align=""left"">
                                                        <h5>Nombre</h5></td>
                                        </tr>
                                    </table>
                                </td></tr>
                            </table>
                            <br>
                            <table>
                                        <tr>
                                            <td><h5 style=""font-size:8px;"">Contrato</h5></td>
                                            <td colspan='2' ><h5 style=""font-size:8px;"">Nombre</h5></td>
                                            <td><h5 style=""font-size:8px;"">Ultimo_Mes_Letra</h5></td>
                                            <td><h5 style=""font-size:8px;"">Ultino_anio</h5></td>
                                            <td><h5 style=""font-size:8px;"">Servicio</h5></td>
                                            <td><h5 style=""font-size:8px;"">@Telefono</h5></td>
                                            <td><h5 style=""font-size:8px;"">@Celular</h5></td>
                                        </tr>
                                </table>
                                
                                <br>     
                                <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2>
                                    <tr><td>                     
                                            <table border='0' cellpadding=2 cellspacing=0>                                               
                                                <tr>
                                                    <td align='center'>
                                                        <h5>Total de la Colonia:  </h5></td>
                                                    <td colspan='4' align=""left"">
                                                        <h5>1.Contrato</h5></td>
                                                </tr>
                                            </table>
                                    </td></tr>
                                </table>
                                <br>
                                <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                    <tr><td>                     
                                            <table border='0' cellpadding=2 cellspacing=0>  
                                                <tr>
                                                    <td colspan='2' align='center'>
                                                        <h5>RESUMEN DE LA CIUDAD {Nombre}</h5>
                                                    </td>
                                                </tr>                                              
                                                <tr>
                                                    <td align='right'>
                                                        <h5>Total De Clientes: </h5></td>
                                                    <td align='left'>
                                                        <h5> #RTotal0</h5></td>
                                                </tr>
                                            </table>
                                    </td></tr>
                                </table>  
                                <br>                        
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>
                                        <td>
                                            <table border='0' style='font-size:10px;' cellpadding=5 cellspacing=0 >
                                                <tr>
                                                    <td colspan='1' align='center'><h5>Gran Total de Clientes: </h5></td>
                                                    <td colspan='2' align='left'><h5>Count of Reporte_TiposCliente_telefono;1.Contrato </h5></td>
                                                    <td colspan='2'></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>                                
                        </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "REportePorPagarTv.pdf");
        }




        //ReportePorPagarInternet_2
        public ActionResult ReportePorPagarInternet_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 15, 15);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              

                            <div align=""center"">                              
                                <table cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td colspan='3'> <h5 style=""font-size:14px;"">@Empresa</h5></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan='3'><h5 style=""font-size:14px;"">@Titulo</h5></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan='3'><h5 style=""font-size:14px;"">@Subtitulo</h5></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan='3' align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan='4'> <h5 style=""font-size:14px;"">@Empresa</h5></td>                                        
                                    </tr>
                                    <tr>
                                        <td colspan='4'><h5 style=""font-size:14px;"">@Titulo</h5></td>
                                    </tr>
                                    <tr>
                                        <td colspan='4'><h5 style=""font-size:14px;"">@Subtitulo</h5></td>
                                    </tr>
                                    <tr>
                                        <td colspan='4' align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                    </tr>
                                </table>
                            </div>
                            <div align=""center"">  
                                <table border='0' style='font-size:10px;' cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td colspan='3'>
                                            <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                                <tr>
                                                    <td>
                                                        <table table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                            <tr>
                                                                <td><h5>Contrato</h5></td>
                                                                <td colspan='2'><h5>Nombre</h5></td>
                                                                <td><h5>Ultimo Mes</h5></td>
                                                                <td><h5>Ultimo Año</h5></td>
                                                                <td><h5>Servicio</h5></td>
                                                                <td><h5>Telefono</h5></td>
                                                                <td><h5>Celular</h5></td>
                                                                <td><h5>Periodo</h5></td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan='1'>
                                        </td>
                                    </tr>
                                </table>   
                                <br>
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>
                                        <td colspan='3'>
                                            <table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td><h5>Contrato</h5></td>
                                                    <td colspan='2'><h5>Nombre</h5></td>
                                                    <td><h5>Ultimo Mes</h5></td>
                                                    <td><h5>Ultimo Año</h5></td>
                                                    <td><h5>Servicio</h5></td>
                                                    <td><h5>Telefono</h5></td>
                                                    <td><h5>Celular</h5></td>
                                                    <td><h5>Periodo</h5></td>      
                                                    <td colspan='2'><h5>Direccion</h5></td>  
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td colspan='3'>
                                            <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                            <h5 style=""font-size:14px;"">Nombre</h5>
                                            <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table> 
                                <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                <h5 style=""font-size:14px;"">Nombre</h5>
                                <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
 
                                <table border='0' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td colspan='3'>
                                            <table table border='0' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td><h5 style='font-size:8px;'>Contrato</h5></td>
                                                    <td colspan='2'><h5 style='font-size:8px;'>Nombre</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Ultimo_mes_letra</h5></td>
                                                    <td><h5 style='font-size:8px;'>Ultimo_anio</h5></td>
                                                    <td><h5 style='font-size:8px;'>Servicio</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Telefono</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Celular</h5></td>
                                                    <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                                </tr>
                                            </table>
                                         </td>
                                         <td></td>
                                     </tr>
                                </table>   
                                <br>
                                <table border='0' style='border:solid #000;' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td colspan='3'>
                                            <table border='0' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td><h5 style='font-size:8px;'>Contrato</h5></td>
                                                    <td colspan='2'><h5 style='font-size:8px;'>Nombre</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Ultimo_mes_letra</h5></td>
                                                    <td><h5 style='font-size:8px;'>Ultimo_anio</h5></td>
                                                    <td><h5 style='font-size:8px;'>Servicio</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Telefono</h5></td>
                                                    <td><h5 style='font-size:8px;'>@Celular</h5></td>
                                                    <td><h5 style='font-size:8px;'>Periodo</h5></td>     
                                                    <td colspan='2'><h5 style='font-size:8px;'>Direccion</h5></td>  
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table style=""font-weight:bold;"" cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td>
                                             <h5>RESUMEN DE LA CIUDAD {Nombre}</h5>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td></td>
                                                    <td align='right'><h5>Total de Clientes: </h5></td>
                                                    <td align='left'><h5> #RTotal0</h5></td>
                                                    <td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td colspan='2'></td>
                                    </tr>
                                </table>

                                <table>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan='3' align='right'><h5>Gran Total de Contratos: </h5></td>
                                                    <td colspan='1' align='left'><h5>Count o</h5></td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                        <td colspan='2'></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan='3' align='right'><h5>Gran Total de Contratos: </h5></td>
                                                    <td colspan='1' align='left'><h5>Count o</h5></td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                        <td colspan='2'></td>
                                    </tr>                                                                       
                                </table>
                                <table>
                                    <tr>
                                        <td colspan='3' align='right'>                                          
                                            <h5>Page N of M</h5>                                          
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan='4' align='right'>                                          
                                            <h5>Page N of M</h5>                                    
                                        </td>
                                    </tr>
                                </table> 
                                
                            </div>
                        </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "ReportePorPagarInternet_2.pdf");
        }




        //        //ReportePIInt_2
        public ActionResult ReportePIInt_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 15, 15, 15, 15);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              

                            <div align=""center"">                              
                                <table cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td><b><h5 style=""font-size:14px;"">@Empresa</h5></b></td>
                                    </tr>
                                    <tr>
                                        <td><b><h5 style=""font-size:12px;"">@Titulo</h5></b></td>
                                    </tr>
                                    <tr>
                                        <td><h5 style=""font-size:12px;"">@Subtitulo</h5></td>
                                    </tr>
                                    <tr>
                                        <td align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                    </tr>                                    
                                </table>
                                <br>
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>                                      
                                        <td>
                                            <table table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td><h5 style='font-size:9px;'>Contrato</h5></td>
                                                    <td colspan='3'><h5 style='font-size:9px;'>Nombre del Cliente</h5></td>
                                                    <td colspan='2'><h5 style='font-size:9px;'>Servicio</h5></td>
                                                    <td><h5 style='font-size:9px;'>telefono</h5></td>
                                                    <td><h5 style='font-size:9px;'>celular</h5></td>
                                                    <td><h5 style='font-size:9px;'>Periodo</h5></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>   
                                <br>
                                <table table border='0' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td>               
                                            <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                            <h5 style=""font-size:14px;"">Nombre</h5>
                                            <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                        </td>
                                    </tr>
                                </table>
                                <table border='0' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td><h5 style='font-size:8px;'>Contrato</h5></td>
                                        <td colspan='3' align='left'><h5 style='font-size:8px;'>Nombre</h5></td>
                                        <td colspan='2'><h5 style='font-size:8px;'>Servicio</h5></td>
                                        <td><h5 style='font-size:8px;'>telefono</h5></td>
                                        <td><h5 style='font-size:8px;'>celular</h5></td>
                                        <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                    </tr>
                                 </table> 
                                <br>
                                <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                    <tr><td>                     
                                            <table border='0' cellpadding=2 cellspacing=0>  
                                                <tr>
                                                    <td colspan='2'><h5>RESUMEN DE LA CIUDAD {Nombre}</h5>                                                </td>
                                                </tr>                                              
                                                <tr>
                                                    <td align='right'><h5>Total De Clientes:</h5></td>
                                                    <td align='left'><h5>#RTotal0</h5></td>
                                                </tr>
                                            </table>
                                    </td></tr>
                                </table>  
                                <br>   
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>
                                        <td colspan='3' align='left'>
                                            <table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td>
                                                        <table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0 >
                                                            <tr>
                                                                <td align='center'><h5>Gran Total de Clientes: </h5></td>
                                                                <td colspan='2' align='left'><h5>Count of Reporte_TiposCliente_nuevo2;1.Contrato </h5></td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                   </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                 <h5 align='right'>Page N of M</h5>  
                            </div>
                        </div>");

            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "ReportePIInt_2.pdf");
        }




        //ReporteNuevoCortesiaTv_2
        public ActionResult ReporteNuevoCortesiaTv_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 15, 15, 15, 15);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              

                            <div align=""center"">                              
                                <table cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td><h5 style=""font-size:14px;"">@Empresa</h5></td>
                                    </tr>
                                    <tr>
                                        <td><h5 style=""font-size:14px;"">@Titulo</h5></td>
                                    </tr>
                                    <tr>
                                        <td><h5 style=""font-size:14px;"">@Subtitulo</h5></td>
                                    </tr>
                                    <tr>
                                        <td align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                    </tr>                                    
                                </table>
                                <br>
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>                                      
                                        <td>
                                            <table table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td><h5 style='font-size:9px;'>Contrato</h5></td>
                                                    <td colspan='3'><h5 style='font-size:9px;'>Nombre del Cliente</h5></td>
                                                    <td colspan='2'><h5 style='font-size:9px;'>Servicio</h5></td>
                                                    <td><h5 style='font-size:9px;'>Teléfono</h5></td>
                                                    <td><h5 style='font-size:9px;'>celular</h5></td>
                                                    <td><h5 style='font-size:9px;'>Status</h5></td>
                                                    <td><h5 style='font-size:9px;'>Fecha De La Cortesia</h5></td>
                                                    <td><h5 style='font-size:9px;'>Activa</h5></td>
                                                    <td><h5 style='font-size:9px;'>Tv sin pago</h5></td>
                                                    <td><h5 style='font-size:9px;'>Tv pago</h5></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>   
                                <br>
                                <table table border='0' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td>               
                                            <table border='1' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                            <h5 style=""font-size:14px;"">Nombre</h5>
                                            <table border='1' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                        </td>
                                    </tr>
                                </table>
                                <table border='0' cellpadding=2 cellspacing=0>
                                    <tr>
                                        <td><h5 style='font-size:9px;'>contrato</h5></td>
                                        <td colspan='3'><h5 style='font-size:9px;'>nombre</h5></td>
                                        <td colspan='2'><h5 style='font-size:9px;'>servicio</h5></td>
                                        <td><h5 style='font-size:9px;'>telefono</h5></td>
                                        <td><h5 style='font-size:9px;'>celular</h5></td>
                                        <td><h5 style='font-size:9px;'>status</h5></td>
                                        <td><h5 style='font-size:9px;'>fecha</h5></td>
                                        <td><h5 style='font-size:9px;'>activa</h5></td>
                                        <td><h5 style='font-size:9px;'>ndecossin</h5></td>
                                        <td><h5 style='font-size:9px;'>ndecoscon</h5></td>                              
                                    </tr>
                                 </table> 
                                <br>
                                <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                    <tr><td>                     
                                            <table border='0' cellpadding=2 cellspacing=0>  
                                                <tr>
                                                    <td colspan='2' align='center'>
                                                        <h5>RESUMEN DE LA CIUDAD {Nombre}</h5>
                                                    </td>
                                                </tr>                                              
                                                <tr>
                                                    <td align='right'><h5>Total De Clientes:</h5></td>
                                                    <td align='left'><h5>#RTotal1</h5></td>
                                                </tr>
                                            </table>
                                    </td></tr>
                                </table>  
                                <br>   
                                <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000; font-weight:bold;'>
                                    <tr>
                                        <td colspan='3' align='left'>
                                            <table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0>
                                                <tr>
                                                    <td>
                                                        <table border='0' style='font-size:10px;' cellpadding=2 cellspacing=0 >
                                                            <tr>
                                                                <td colspan='1' align='center'><h5>Gran Total de Contratos: </h5></td>
                                                                <td colspan='2' align='left'><h5>#RTotal2 </h5></td>
                                                                <td colspan='2'></td>
                                                            </tr>
                                                        </table>
                                                   </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                 <h5 align='right'>Page N of M</h5>  
                            </div>
                        </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "ReporteNuevoCortesiaTv_2.pdf");
        }




        //ReporteNuevoCortesiaIntDig_2
        public ActionResult ReporteNuevoCortesiaIntDig_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              
        
                                    <div align=""center"">                              
                                        <table  border='0' cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td><h5 style=""font-size:14px;"">@Empresa</h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:14px;"">@Titulo</h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:14px;"">@Subtitulo</h5></td>
                                            </tr>
                                            <tr>
                                                <td align='right'><b><h5 style=""font-size:10px;"">PrintDate</h5></b></td>
                                            </tr>                                    
                                        </table>
                                        <br>
                                        <table border='0' cellpadding=2 cellspacing=0>
                                            <tr>
                                                <td>               
                                                    <table border='1' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                    <h5 style=""font-size:14px;"">Nombre</h5>
                                                    <table border='1' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>

                                        <table style='font-size:10px;' border='1' cellpadding=2 cellspacing=2>
                                            <tr>
                                                <td>
                                                    <table border='0' cellpadding=2 cellspacing=0>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5 style='font-size:9px;'><b>Contrato</b></h5></td>
                                                                        <td align='left'><h5 style='font-size:9px;'>contrato</h5></td>
                                                                        <td align='right'><h5 style='font-size:9px;'><b>Periodo</b></h5></td>
                                                                        <td align='left'><h5 style='font-size:9px;'>Periodo</h5></td>
                                                                        <td align='right' colspan='2'><h5 style='font-size:9px;'><b>@Titulocolumna</b></h5></td>
                                                                        <td align='left' colspan='2'><h5 style='font-size:9px;'>ndecoscon</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5 style='font-size:9px;'><b>Nombre</b></h5></td>
                                                                        <td colspan='3' align='left'><h5 style='font-size:9px;'>nombre</h5></td>>
                                                                        <td align='right' colspan='2'><h5 style='font-size:9px;'><b>@Titulocolumna1</b></h5></td>
                                                                        <td align='left' colspan='2'><h5 style='font-size:9px;'>ndecossin</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5 style='font-size:9px;'><b>Dirección</b></h5></td>
                                                                        <td colspan='3' align='left'><h5 style='font-size:9px;'>@Direccion</h5></td>>
                                                                        <td align='right' colspan='2'><h5 style='font-size:9px;'><b>@TotalDecos</b></h5></td>
                                                                        <td align='left' colspan='2'><h5 style='font-size:9px;'>totaldecos</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td colspan='4'></td>
                                                                        <td align='right' colspan='2'><h5 style='font-size:9px;'><b>Teléfono</b></h5></td>
                                                                        <td align='left' colspan='2'><h5 style='font-size:9px;'>telefono</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table> 

                                                </td>
                                            </tr>
                                        </table>

                                        <b><h5 align='left' style=""font-size:10px;"">@Titulo4</h5></b>
                                        
                                        <table border='0' style='font-size:10px;' cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td colspan='2'></td>                               
                                                <td colspan='4'>
                                                    <table border='1' cellpadding=1 cellspacing=1 style='border:solid #000;' bgcolor='silver'>
                                                        <tr>
                                                            <td>
                                                                <table border='0' style='font-size:10px; font-weight:bold;' cellpadding=0 cellspacing=0>
                                                                    <tr>
                                                                        <td><h5>Servicio</h5></td>
                                                                        <td><h5>Status</h5></td>
                                                                        <td><h5>Fecha de Cortesía</h5></td>
                                                                        <td><h5>Activa</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>                                                  
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td colspan='2'></td>                               
                                                <td colspan='4'>
                                                    <table border='0' cellpadding=1 cellspacing=1 style='border:solid #000;'>
                                                        <tr>
                                                            <td><h5 style='font-size:8px;'>servicios</h5></td>
                                                            <td><h5 style='font-size:8px;'>status</h5></td>
                                                            <td><h5 style='font-size:8px;'>fecha</h5></td>
                                                            <td><h5 style='font-size:8px;'>activa</h5></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                        <br>
                                        <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                            <tr><td>                     
                                                    <table border='0' cellpadding=2 cellspacing=0>  
                                                        <tr>
                                                            <td colspan='5' align='center'>
                                                                <h5>RESUMEN DE LA CIUDAD {Nombre}</h5>
                                                            </td>
                                                        </tr>                                              
                                                        <tr>
                                                            <td colspan='1' align='center'><h5>Total De Clientes Por Ciudad: </h5></td>                                       
                                                            <td colspan='4' align='left'><h5>#RTotal1</h5></td>                                                           
                                                        </tr>
                                                    </table>
                                            </td></tr>
                                        </table>
                                        <br>
                                        <table style='font-size:10px; font-weight:bold;' border='0' cellpadding=2 cellspacing=2 >
                                            <tr>
                                                <td>                     
                                                    <table border='0' cellpadding=0 cellspacing=0>  
                                                        <tr>
                                                            <td colspan='4' align='center'>
                                                                <h5>RESUMEN TOTAL DE TODAS LAS CIUDADES {Nombre}</h5>
                                                            </td>
                                                        </tr>                                              
                                                        <tr>
                                                            <td colspan='1' align='center'><h5>Gran Total de Clientes con Cortesia:</h5></td>
                                                            <td colspan='3' align='left'><h5>#RTotal2</h5></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table> 
                                        <h5 align='right'>Page N of M</h5> 
                                    </div>
                                </div>");
            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "ReporteNuevoCortesiaIntDig_2.pdf");
        }






        //ReporteCiudad_2
        public ActionResult ReporteCiudad_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              
        
                                    <div align=""center"">                              
                                        <table cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td><h5 style=""font-size:14px;""><b>@Empresa</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:13px;""><b>@Titulo</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:12px;"">@Subtitulo</h5></td>
                                            </tr>
                                            <tr>
                                                <td align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                            </tr>                                    
                                        </table>
                                        <br>
                                        <table border='0' cellpadding=2 cellspacing=0 style='font-weight:bold; text-decoration: underline;'>
                                            <tr> style='
                                                <td><h5 style='font-size:9px;'><b>contrato</b></h5></td>
                                                <td colspan='2' ><h5 style='font-size:9px;'>Nombre</h5></td>
                                                <td colspan='3'><h5 style='font-size:9px;'><b>Dirección</b></h5></td>
                                                <td><h5 style='font-size:9px;'>Status</h5></td>
                                                <td><h5 style='font-size:9px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:9px;'>Placa</h5></td>
                                            </tr>
                                             <tr>
                                                <td><h5 style='font-size:9px;'><b>contrato</b></h5></td>
                                                <td colspan='2' ><h5 style='font-size:9px;'>Nombre</h5></td>
                                                <td colspan='3'><h5 style='font-size:9px;'><b>Dirección</b></h5></td>
                                                <td><h5 style='font-size:9px;'>Status</h5></td>
                                                <td><h5 style='font-size:9px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:9px;'>Placa</h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style='font-size:9px;'><b>contrato</b></h5></td>
                                                <td colspan='2'><h5 style='font-size:9px;'>Nombre</h5></td>
                                                <td colspan='2'><h5 style='font-size:9px;'><b>Dirección</b></h5></td>
                                                <td><h5 style='font-size:9px;'>Telefono</h5></td>
                                                <td><h5 style='font-size:9px;'>Telefono</h5></td>
                                                <td><h5 style='font-size:9px;'>Status</h5></td>
                                                <td><h5 style='font-size:9px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:9px;'>@Titulofecha</h5></td>
                                            </tr>                                            
                                        </table>
                                        <br>
                                        <table border='0' cellpadding=2 cellspacing=0>
                                            <tr>
                                                <td>               
                                                    <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                    <h5 style=""font-size:14px;"">Nombre</h5>
                                                    <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                </td>
                                            </tr>
                                        </table>

                                         <table border='0' cellpadding=2 cellspacing=0 >
                                            <tr>
                                                <td><h5 style='font-size:8px;'>contrato</h5></td>
                                                <td colspan='2'><h5 style='font-size:8px;'>Nombre</h5></td>
                                                <td colspan='4'><h5 style='font-size:8px;'>@Direccion</h5></td>
                                                <td><h5 style='font-size:8px;'>status</h5></td>
                                                <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:8px;'>Placa</h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style='font-size:8px;'>contrato</h5></td>
                                                <td colspan='2'><h5 style='font-size:8px;'>Nombre</h5></td>
                                                <td colspan='2'><h5 style='font-size:8px;'>@Direccion</h5></td>
                                                <td><h5 style='font-size:8px;'>telefono</h5></td>
                                                <td><h5 style='font-size:8px;'>celular</h5></td>
                                                <td><h5 style='font-size:8px;'>status</h5></td>
                                                <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:8px;'>@Fecha</h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style='font-size:8px;'>contrato</h5></td>
                                                <td colspan='2'><h5 style='font-size:8px;'>Nombre</h5></td>
                                                <td colspan='4'><h5 style='font-size:8px;'>@Direccion</h5></td>
                                                <td><h5 style='font-size:8px;'>status</h5></td>
                                                <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                                <td><h5 style='font-size:8px;'>Placa</h5></td>
                                            </tr>
                                         </table>
                                        <br>
                                        <h5 align='center' style='font-size:11px;'><b>RESUMEN DE CLIENTES DE LA CIUDAD {Nombre}</b></h5> 
                             
                                        <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                            <tr>
                                                <td>
                                                    <table border='0' cellpadding=2 cellspacing=0>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Instalados:</h5></td>
                                                                        <td align='left'><h5>#RTotal0</h5></td>
                                                                        <td align='right'><h5>Contratados:</h5></td>
                                                                        <td align='left'><h5>#RTotal5</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Desconectados:</h5></td>
                                                                        <td align='left'><h5>#RTotal1</h5></td>
                                                                        <td align='right'><h5>Suspendido:</h5></td>
                                                                        <td align='left'><h5>#RTotal3</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Bajas:</h5></td>
                                                                        <td align='left'><h5>#RTotal2</h5></td>
                                                                        <td align='right'><h5>Fuera de Area:</h5></td>
                                                                        <td align='left'><h5>#RTotal4</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Suspensiones Temporales:</h5></td>
                                                                        <td align='left'><h5>#RTotal6</h5></td>
                                                                        <td colspan='2'></td>                                                                        
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td align='right' colspan='2'><h5>Total Clientes Por Ciudad:</h5></td>
                                                                        <td align='left' colspan='2'><h5><u>DistinctCount of Reporte_TiposCliente_Ciudad;1.contrato</u></h5></td>                                                                                                    
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                        <h5 align='center' style='font-size:11px;'><b>RESUMEN DE CLIENTES POR TODAS LAS CIUDADES</b></h5> 
                                        <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2>
                                            <tr>
                                                <td>
                                                    <table border='0' cellpadding=2 cellspacing=0>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Instalados:</h5></td>
                                                                        <td align='left'><h5>#RTotal7</h5></td>
                                                                        <td align='right'><h5>Contratados:</h5></td>
                                                                        <td align='left'><h5>#RTotal11</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Desconectados:</h5></td>
                                                                        <td align='left'><h5>#RTotal8</h5></td>
                                                                        <td align='right'><h5>Suspendido:</h5></td>
                                                                        <td align='left'><h5>#RTotal13</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Bajas:</h5></td>
                                                                        <td align='left'><h5>#RTotal9</h5></td>
                                                                        <td align='right'><h5>Fuera de Area:</h5></td>
                                                                        <td align='left'><h5>#RTotal12</h5></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td align='right'><h5>Suspensiones Temporales:</h5></td>
                                                                        <td align='left'><h5>#RTotal10</h5></td>
                                                                        <td colspan='2'></td>                                                                        
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>                                      
                                                            <td>
                                                                <table border='0' cellpadding=2 cellspacing=0>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td align='right' colspan='2'><h5>Gran Total de Clientes:</h5></td>
                                                                        <td align='left' colspan='2'><h5><u>DistinctCount of Reporte_TiposCliente_Ciudad;1.contrato</u></h5></td>                                                                                                                                            
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table> 
                                                </td>
                                            </tr>
                                        </table>
                                        <h5 align='right'>Page N of M</h5> 
                                    </div>
                                </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "ReporteCiudad_2.pdf");
        }




        //Reporte_Resumen_Por_Colonia
        public ActionResult Reporte_Resumen_Por_Colonia(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 10, 10, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              
        
                                    <div align=""center"">                              
                                        <table cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td><h5 style=""font-size:14px;""><b>@Empresa</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:13px;""><b>@Titulo</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:12px;"">@Subtitulo</h5></td>
                                            </tr>
                                            <tr>
                                                <td align='right'><h5 style=""font-size:10px;""><b>PrintDate</b></h5></td>
                                            </tr>                                    
                                        </table>
                                        <br>

                                        <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2>
                                            <tr><td> 
                                                <table border='0' cellpadding=2 cellspacing=0  style='text-decoration: underline;'>
                                                    <tr>
                                                        <td colspan='2'><h5>Nombre de la Colonia</h5></td>
                                                        <td><h5>Instalados</h5></td>
                                                        <td><h5><b>Desconectados</b></h5></td>
                                                        <td><h5>Contratados</h5></td>
                                                        <td><h5>Bajas</h5></td>
                                                        <td><h5>Fuera de Area</h5></td>
                                                        <td><h5>Suspendidos</h5></td>
                                                        <td><h5>Susp. Temp</h5></td>
                                                        <td><h5>Total</h5></td>
                                                    </tr>
                                                </table>
                                            </td></tr>
                                        </table>
                                        <br><br>
                                        <table border='0' cellpadding=2 cellspacing=2>
                                            <tr>
                                                <td>
                                                    <table border='0' cellpadding=2 cellspacing=0 >
                                                        <tr>
                                                            <td colspan='2'><h5 style='font-size:8px;'><b>nom_calle</b></h5></td>
                                                            <td><h5 style='font-size:8px;'>#Instalados</h5></td>
                                                            <td><h5 style='font-size:8px;'>#Desconectados</b></h5></td>
                                                            <td><h5 style='font-size:8px;'>#Contratados</h5></td>
                                                            <td><h5 style='font-size:8px;'>#Bajas</h5></td>
                                                            <td><h5 style='font-size:8px;'>#Fuera_de_Area</h5></td>
                                                            <td><h5 style='font-size:8px;'>#Suspendidos</h5></td>
                                                            <td><h5 style='font-size:8px;'>#Temporales</h5></td>
                                                            <td><h5 style='font-size:8px;'><b>#TotalColonia</b></h5></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style='font-weight:bold;' border='0' cellpadding=2 cellspacing=0 >
                                                        <tr>
                                                            <td colspan='2'><h5>TOTAL:</h5></td>
                                                            <td><h5>#Total_Instalados</h5></td>
                                                            <td><h5>#Total_Desconectados</b></h5></td>
                                                            <td><h5>#Total_Contratados</h5></td>
                                                            <td><h5>#Total_Bajas</h5></td>
                                                            <td><h5>#Total_Fueras_deArea</h5></td>
                                                            <td><h5>#Total_Suspendidos</h5></td>
                                                            <td><h5>#Total_Temporales</h5></td>
                                                            <td><h5><b>#Total_Total</b></h5></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>
                                        <h5 align='right'>Page N of M</h5> 
                                    </div>
                                </div>");
            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "Reporte_Resumen_Por_Colonia.pdf");
        }



        //Reporte_Rango_Fechas_Tv_2
        public ActionResult Reporte_Rango_Fechas_Tv_2(string a)
        {
            Guid g = Guid.NewGuid();

            string rutaarchivo = Server.MapPath("/Reportes/") + g.ToString() + "pdfejemplo.pdf";
            iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4.Rotate(), 25, 25, 20, 20);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(rutaarchivo, FileMode.Create));
            document.Open();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document, null, null);

            StringBuilder sb = new StringBuilder();

            sb.Append(@"<div align=""center"" style=""font-size:10px; font-family:Arial;"">                              
        
                                    <div align=""center"">                              
                                        <table cellpadding=0 cellspacing=0>
                                            <tr>
                                                <td><h5 style=""font-size:14px;""><b>@Empresa</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:12px;""><b>@Titulo</b></h5></td>
                                            </tr>
                                            <tr>
                                                <td><h5 style=""font-size:12px;"">@Subtitulo</h5></td>
                                            </tr>
                                            <tr>
                                                <td align='right'><h5 style=""font-size:10px;"">PrintDate</h5></td>
                                            </tr>                                    
                                        </table>
                                        <br>

                                        <table style='font-size:10px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2>
                                            <tr><td>
                                                <table border='0' cellpadding=2 cellspacing=0 style='text-decoration: underline;'>
                                                    <tr>
                                                        <td><h5>Contrato</h5></td>
                                                        <td><h5>Nombre</h5></td>
                                                        <td><h5>Servicio(s)</h5></td>
                                                        <td><h5>telefono</h5></td>
                                                        <td><h5>celular</h5></td>
                                                        <td><h5>Periodo</h5></td>
                                                    </tr>
                                                </table>
                                            </td></tr>
                                        </table>
                                        <br>
                                        <table border='0' cellpadding=2 cellspacing=0>
                                            <tr>
                                                <td>               
                                                    <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                    <h5 style=""font-size:14px;"">Nombre</h5>
                                                    <table border='2' bordercolor='black' cellpadding=0 cellspacing=0><tr><td>&nbsp;</td></tr></table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style='font-size:8px;' border='1' cellpadding=2 cellspacing=2>
                                            <tr><td>
                                                <table border='0' cellpadding=2 cellspacing=0>
                                                    <tr>
                                                        <td><h5 style='font-size:8px;'>Contrato</h5></td>
                                                        <td><h5 style='font-size:8px;'>Nombre</h5></td>
                                                        <td><h5 style='font-size:8px;'>Servicio</h5></td>
                                                        <td><h5 style='font-size:8px;'>telefono</h5></td>
                                                        <td><h5 style='font-size:8px;'>celular</h5></td>
                                                        <td><h5 style='font-size:8px;'>Periodo</h5></td>
                                                    </tr>
                                                </table>
                                            </td></tr>
                                        </table>
                                        <br
>
                                        <table style='font-weight:bold;' border='1' cellpadding=2 cellspacing=2 bgcolor='silver'>
                                            <tr>
                                                <td>                     
                                                    <table border='0' cellpadding=2 cellspacing=0>  
                                                        <tr>
                                                            <td colspan='2' align='center'>
                                                                <h5>RESUMEN DE LA CIUDAD {Nombre}</h5>
                                                            </td>
                                                        </tr>                                              
                                                        <tr>                                                            
                                                            <td align='right'><h5>Total de Clientes:</h5></td>
                                                            <td align='left'><h5>#RTotal0</h5></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br>

                                        <table style='font-size:8px; font-weight:bold;' border='1' cellpadding=2 cellspacing=2>
                                            <tr>
                                                <td>
                                                    <table border='0' cellpadding=2 cellspacing=0>
                                                        <tr>
                                                            <td align='center'><h5>Gran Total de Clientes:</h5></td>
                                                            <td align='left' colspan='2'><h5>Count of Reporte_TiposClienteTv_nuevo;1.Contrato</h5></td>
                                                            <td colspan='2'></td>                                                                                                                                            
                                                        </tr>
                                                    </table>
                                                </td>
                                           </tr>
                                        </table>
                                        <h5 align='right'>Page N of M</h5> 
                                    </div>
                                </div>");


            hw.Parse(new StringReader(sb.ToString()));
            document.Close();
            return File(rutaarchivo, "application/pdf", "Reporte_Rango_Fechas_Tv_2.pdf");
        }


    }
}
