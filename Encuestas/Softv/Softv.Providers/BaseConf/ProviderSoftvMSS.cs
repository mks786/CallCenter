using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Providers
{
    public class ProviderSoftv
    {
        public static UsuarioProvider Usuario
        {
            get { return UsuarioProvider.Instance; }
        }
        public static RoleProvider Role
        {
            get { return RoleProvider.Instance; }
        }
        public static PermisoProvider Permiso
        {
            get { return PermisoProvider.Instance; }
        }
        public static ModuleProvider Module
        {
            get { return ModuleProvider.Instance; }
        }



        public static EncuestaProvider Encuesta
        {
            get { return EncuestaProvider.Instance; }
        }


        public static PreguntaProvider Pregunta
        {
            get { return PreguntaProvider.Instance; }
        }


        public static TipoPreguntasProvider TipoPreguntas
        {
            get { return TipoPreguntasProvider.Instance; }
        }

        public static RelEncuestaClientesProvider RelEncuestaClientes
        {
            get { return RelEncuestaClientesProvider.Instance; }
        }

        public static ResOpcMultsProvider ResOpcMults
        {
            get { return ResOpcMultsProvider.Instance; }
        }


        public static RelEnProcesosProvider RelEnProcesos
        {
            get { return RelEnProcesosProvider.Instance; }
        }

        public static RelPreguntaEncuestasProvider RelPreguntaEncuestas
        {
            get { return RelPreguntaEncuestasProvider.Instance; }
        }

        public static RelPreguntaOpcMultsProvider RelPreguntaOpcMults
        {
            get { return RelPreguntaOpcMultsProvider.Instance; }
        }

        public static ConexionProvider Conexion
        {
            get { return ConexionProvider.Instance; }
        }

        public static CLIENTEProvider CLIENTE
        {
            get { return CLIENTEProvider.Instance; }
        }




        public static TipServProvider TipServ
        {
            get { return TipServProvider.Instance; }
        }


        public static TurnoProvider Turno
        {
            get { return TurnoProvider.Instance; }
        }

        public static LlamadaProvider Llamada
        {
            get { return LlamadaProvider.Instance; }
        }

        public static Rel_Clientes_TiposClientesProvider Rel_Clientes_TiposClientes
        {
            get { return Rel_Clientes_TiposClientesProvider.Instance; }
        }

        public static TipoClienteProvider TipoCliente
        {
            get { return TipoClienteProvider.Instance; }
        }

        public static CatalogoPeriodosCorteProvider CatalogoPeriodosCorte
        {
            get { return CatalogoPeriodosCorteProvider.Instance; }
        }

        public static Cliente_ApellidoProvider Cliente_Apellido
        {
            get { return Cliente_ApellidoProvider.Instance; }
        }

        public static TapProvider Tap
        {
            get { return TapProvider.Instance; }
        }

        public static DatoFiscalProvider DatoFiscal
        {
            get { return DatoFiscalProvider.Instance; }
        }

        public static TrabajoProvider Trabajo
        {
            get { return TrabajoProvider.Instance; }
        }




        public static MotivoCancelacionProvider MotivoCancelacion
        {
            get { return MotivoCancelacionProvider.Instance; }
        }


        public static RelEncuestaPreguntaResProvider RelEncuestaPreguntaRes
        {
            get { return RelEncuestaPreguntaResProvider.Instance; }
        }

        public static QuejaProvider Queja
        {
            get { return QuejaProvider.Instance; }
        }

        public static CIUDADProvider CIUDAD
        {
            get { return CIUDADProvider.Instance; }
        }

        public static CVECOLCIUProvider CVECOLCIU
        {
            get { return CVECOLCIUProvider.Instance; }
        }

        public static COLONIAProvider COLONIA
        {
            get { return COLONIAProvider.Instance; }
        }

        public static CVECAROLProvider CVECAROL
        {
            get { return CVECAROLProvider.Instance; }
        }

        public static CALLEProvider CALLE
        {
            get { return CALLEProvider.Instance; }
        }



        public static BusquedaIndividualProvider BusquedaIndividual
        {
            get { return BusquedaIndividualProvider.Instance; }
        }

        public static tblClasificacionProblemaProvider tblClasificacionProblema
        {
            get { return tblClasificacionProblemaProvider.Instance; }
        }

        public static tblPrioridadQuejaProvider tblPrioridadQueja
        {
            get { return tblPrioridadQuejaProvider.Instance; }
        }


        public static NoClienteProvider NoCliente
        {
            get { return NoClienteProvider.Instance; }
        }
  
    }
}
