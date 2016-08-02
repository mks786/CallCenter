﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftvConfiguration
{
    public class SoftvSection : ConfigurationSection
    {
        /// <summary>
        /// Gets default connection String. If it doesn't exist then
        /// returns global connection string
        /// </summary>
        [ConfigurationProperty("DefaultConnectionString")]
        public String ConnectionString
        {
            get
            {
                string connectionString = (string)base["DefaultConnectionString"];
                connectionString = String.IsNullOrEmpty(connectionString) ?
                    Globals.DataAccess.GlobalConectionString :
                    (string)base["DefaultConnectionString"];
                return connectionString;
            }
        }

        /// <summary>
        /// Gets default assembly name for TestSimulator data clases
        /// </summary>
        [ConfigurationProperty("DefaultAssembly", DefaultValue = "Softv.SQL")]
        public String Assembly
        {
            get { return (string)base["DefaultAssembly"]; }
        }
        /// <summary>
        /// Gets Usuario configuration data
        /// </summary>
        [ConfigurationProperty("Usuario")]
        public UsuarioElement Usuario
        {
            get { return (UsuarioElement)base["Usuario"]; }
        }
        /// <summary>
        /// Gets Role configuration data
        /// </summary>
        [ConfigurationProperty("Role")]
        public RoleElement Role
        {
            get { return (RoleElement)base["Role"]; }
        }

        /// <summary>
        /// Gets Permiso configuration data
        /// </summary>
        [ConfigurationProperty("Permiso")]
        public PermisoElement Permiso
        {
            get { return (PermisoElement)base["Permiso"]; }
        }
        /// <summary>
        /// Gets Module configuration data
        /// </summary>
        [ConfigurationProperty("Module")]
        public ModuleElement Module
        {
            get { return (ModuleElement)base["Module"]; }
        }



        /// <summary>
        /// Gets Encuesta configuration data
        /// </summary>
        [ConfigurationProperty("Encuesta")]
        public EncuestaElement Encuesta
        {
            get { return (EncuestaElement)base["Encuesta"]; }
        }

        /// <summary>
        /// Gets Pregunta configuration data
        /// </summary>
        [ConfigurationProperty("Pregunta")]
        public PreguntaElement Pregunta
        {
            get { return (PreguntaElement)base["Pregunta"]; }
        }


        /// <summary>
        /// Gets TipoPreguntas configuration data
        /// </summary>
        [ConfigurationProperty("TipoPreguntas")]
        public TipoPreguntasElement TipoPreguntas
        {
            get { return (TipoPreguntasElement)base["TipoPreguntas"]; }
        }

        /// <summary>
        /// Gets RelEncuestaClientes configuration data
        /// </summary>
        [ConfigurationProperty("RelEncuestaClientes")]
        public RelEncuestaClientesElement RelEncuestaClientes
        {
            get { return (RelEncuestaClientesElement)base["RelEncuestaClientes"]; }
        }

        /// <summary>
        /// Gets ResOpcMults configuration data
        /// </summary>
        [ConfigurationProperty("ResOpcMults")]
        public ResOpcMultsElement ResOpcMults
        {
            get { return (ResOpcMultsElement)base["ResOpcMults"]; }
        }



        /// <summary>
        /// Gets RelEnProcesos configuration data
        /// </summary>
        [ConfigurationProperty("RelEnProcesos")]
        public RelEnProcesosElement RelEnProcesos
        {
            get { return (RelEnProcesosElement)base["RelEnProcesos"]; }
        }


        /// <summary>
        /// Gets RelPreguntaEncuestas configuration data
        /// </summary>
        [ConfigurationProperty("RelPreguntaEncuestas")]
        public RelPreguntaEncuestasElement RelPreguntaEncuestas
        {
            get { return (RelPreguntaEncuestasElement)base["RelPreguntaEncuestas"]; }
        }

        /// <summary>
        /// Gets RelPreguntaOpcMults configuration data
        /// </summary>
        [ConfigurationProperty("RelPreguntaOpcMults")]
        public RelPreguntaOpcMultsElement RelPreguntaOpcMults
        {
            get { return (RelPreguntaOpcMultsElement)base["RelPreguntaOpcMults"]; }
        }


        /// <summary>
        /// Gets Conexion configuration data
        /// </summary>
        [ConfigurationProperty("Conexion")]
        public ConexionElement Conexion
        {
            get { return (ConexionElement)base["Conexion"]; }
        }

        /// <summary>
        /// Gets CLIENTE configuration data
        /// </summary>
        [ConfigurationProperty("CLIENTE")]
        public CLIENTEElement CLIENTE
        {
            get { return (CLIENTEElement)base["CLIENTE"]; }
        }




        /// <summary>
        /// Gets TipServ configuration data
        /// </summary>
        [ConfigurationProperty("TipServ")]
        public TipServElement TipServ
        {
            get { return (TipServElement)base["TipServ"]; }
        }


        /// <summary>
        /// Gets Turno configuration data
        /// </summary>
        [ConfigurationProperty("Turno")]
        public TurnoElement Turno
        {
            get { return (TurnoElement)base["Turno"]; }
        }

        /// <summary>
        /// Gets Llamada configuration data
        /// </summary>
        [ConfigurationProperty("Llamada")]
        public LlamadaElement Llamada
        {
            get { return (LlamadaElement)base["Llamada"]; }
        }

        /// <summary>
        /// Gets Rel_Clientes_TiposClientes configuration data
        /// </summary>
        [ConfigurationProperty("Rel_Clientes_TiposClientes")]
        public Rel_Clientes_TiposClientesElement Rel_Clientes_TiposClientes
        {
            get { return (Rel_Clientes_TiposClientesElement)base["Rel_Clientes_TiposClientes"]; }
        }

        /// <summary>
        /// Gets TipoCliente configuration data
        /// </summary>
        [ConfigurationProperty("TipoCliente")]
        public TipoClienteElement TipoCliente
        {
            get { return (TipoClienteElement)base["TipoCliente"]; }
        }

        /// <summary>
        /// Gets CatalogoPeriodosCorte configuration data
        /// </summary>
        [ConfigurationProperty("CatalogoPeriodosCorte")]
        public CatalogoPeriodosCorteElement CatalogoPeriodosCorte
        {
            get { return (CatalogoPeriodosCorteElement)base["CatalogoPeriodosCorte"]; }
        }

        /// <summary>
        /// Gets Cliente_Apellido configuration data
        /// </summary>
        [ConfigurationProperty("Cliente_Apellido")]
        public Cliente_ApellidoElement Cliente_Apellido
        {
            get { return (Cliente_ApellidoElement)base["Cliente_Apellido"]; }
        }

        /// <summary>
        /// Gets Tap configuration data
        /// </summary>
        [ConfigurationProperty("Tap")]
        public TapElement Tap
        {
            get { return (TapElement)base["Tap"]; }
        }

        /// <summary>
        /// Gets DatoFiscal configuration data
        /// </summary>
        [ConfigurationProperty("DatoFiscal")]
        public DatoFiscalElement DatoFiscal
        {
            get { return (DatoFiscalElement)base["DatoFiscal"]; }
        }


        /// <summary>
        /// Gets Trabajo configuration data
        /// </summary>
        [ConfigurationProperty("Trabajo")]
        public TrabajoElement Trabajo
        {
            get { return (TrabajoElement)base["Trabajo"]; }
        }

        /// <summary>
        /// Gets MotivoCancelacion configuration data
        /// </summary>
        [ConfigurationProperty("MotivoCancelacion")]
        public MotivoCancelacionElement MotivoCancelacion
        {
            get { return (MotivoCancelacionElement)base["MotivoCancelacion"]; }
        }


        /// <summary>
        /// Gets RelEncuestaPreguntaRes configuration data
        /// </summary>
        [ConfigurationProperty("RelEncuestaPreguntaRes")]
        public RelEncuestaPreguntaResElement RelEncuestaPreguntaRes
        {
            get { return (RelEncuestaPreguntaResElement)base["RelEncuestaPreguntaRes"]; }
        }


        /// <summary>
        /// Gets Queja configuration data
        /// </summary>
        [ConfigurationProperty("Queja")]
        public QuejaElement Queja
        {
            get { return (QuejaElement)base["Queja"]; }
        }


        /// <summary>
        /// Gets CIUDAD configuration data
        /// </summary>
        [ConfigurationProperty("CIUDAD")]
        public CIUDADElement CIUDAD
        {
            get { return (CIUDADElement)base["CIUDAD"]; }
        }

        /// <summary>
        /// Gets CVECOLCIU configuration data
        /// </summary>
        [ConfigurationProperty("CVECOLCIU")]
        public CVECOLCIUElement CVECOLCIU
        {
            get { return (CVECOLCIUElement)base["CVECOLCIU"]; }
        }

        /// <summary>
        /// Gets COLONIA configuration data
        /// </summary>
        [ConfigurationProperty("COLONIA")]
        public COLONIAElement COLONIA
        {
            get { return (COLONIAElement)base["COLONIA"]; }
        }

        /// <summary>
        /// Gets CVECAROL configuration data
        /// </summary>
        [ConfigurationProperty("CVECAROL")]
        public CVECAROLElement CVECAROL
        {
            get { return (CVECAROLElement)base["CVECAROL"]; }
        }


        /// <summary>
        /// Gets CALLE configuration data
        /// </summary>
        [ConfigurationProperty("CALLE")]
        public CALLEElement CALLE
        {
            get { return (CALLEElement)base["CALLE"]; }
        }


        /// <summary>
        /// Gets BusquedaIndividual configuration data
        /// </summary>
        [ConfigurationProperty("BusquedaIndividual")]
        public BusquedaIndividualElement BusquedaIndividual
        {
            get { return (BusquedaIndividualElement)base["BusquedaIndividual"]; }
        }

        /// <summary>
        /// Gets tblClasificacionProblema configuration data
        /// </summary>
        [ConfigurationProperty("tblClasificacionProblema")]
        public tblClasificacionProblemaElement tblClasificacionProblema
        {
            get { return (tblClasificacionProblemaElement)base["tblClasificacionProblema"]; }
        }

        /// <summary>
        /// Gets tblPrioridadQueja configuration data
        /// </summary>
        [ConfigurationProperty("tblPrioridadQueja")]
        public tblPrioridadQuejaElement tblPrioridadQueja
        {
            get { return (tblPrioridadQuejaElement)base["tblPrioridadQueja"]; }
        }


        /// <summary>
        /// Gets NoCliente configuration data
        /// </summary>
        [ConfigurationProperty("NoCliente")]
        public NoClienteElement NoCliente
        {
            get { return (NoClienteElement)base["NoCliente"]; }
        }


        /// <summary>
        /// Gets DatosLlamada configuration data
        /// </summary>
        [ConfigurationProperty("DatosLlamada")]
        public DatosLlamadaElement DatosLlamada
        {
            get { return (DatosLlamadaElement)base["DatosLlamada"]; }
        }

        /// <summary>
        /// Gets Estadistica configuration data
        /// </summary>
        [ConfigurationProperty("Estadistica")]
        public EstadisticaElement Estadistica
        {
            get { return (EstadisticaElement)base["Estadistica"]; }
        }


        /// <summary>
        /// Gets Servicio configuration data
        /// </summary>
        [ConfigurationProperty("Servicio")]
        public ServicioElement Servicio
        {
            get { return (ServicioElement)base["Servicio"]; }
        }

        /// <summary>
        /// Gets MotAtenTel configuration data
        /// </summary>
        [ConfigurationProperty("MotAtenTel")]
        public MotAtenTelElement MotAtenTel
        {
            get { return (MotAtenTelElement)base["MotAtenTel"]; }
        }
        /// <summary>
        /// Gets ClasificacionProblema configuration data
        /// </summary>
        [ConfigurationProperty("ClasificacionProblema")]
        public ClasificacionProblemaElement ClasificacionProblema
        {
            get { return (ClasificacionProblemaElement)base["ClasificacionProblema"]; }
        }
  
    }
}
