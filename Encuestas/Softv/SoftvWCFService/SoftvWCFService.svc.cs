using Globals;
using Softv.BAL;
using Softv.Entities;
using SoftvWCFService.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;
using System.Xml.Linq;

namespace SoftvWCFService
{
    public partial class SoftvWCFService : IUsuario, IRole, IModule, IPermiso, IEncuesta, IPregunta, ITipoPreguntas, IRelEncuestaClientes, IRelPreguntaOpcMults,
        IRelPreguntaEncuestas, IResOpcMults, IRelEnProcesos, IConexion, ICLIENTE, ITurno, ILlamada, IRel_Clientes_TiposClientes, ITipoCliente, ICatalogoPeriodosCorte,
        ICliente_Apellido, ITap, IDatoFiscal, ITrabajo, ITipServ, IMotivoCancelacion, IRelEncuestaPreguntaRes, IQueja, ICIUDAD, ICVECOLCIU, ICOLONIA, ICVECAROL, ICALLE
    {
        #region Usuario
        public UsuarioEntity GetUsuario(int? IdUsuario)
        {
            return Usuario.GetOne(IdUsuario);
        }

        public UsuarioEntity GetusuarioByUserAndPass(string Usuariox, string Pass)
        {
            return Usuario.GetusuarioByUserAndPass(Usuariox, Pass);
        }

        public UsuarioEntity GetDeepUsuario(int? IdUsuario)
        {
            return Usuario.GetOneDeep(IdUsuario);
        }

        public IEnumerable<UsuarioEntity> GetUsuarioList()
        {
            return Usuario.GetAll();
        }

        public SoftvList<UsuarioEntity> GetUsuarioPagedList(int page, int pageSize)
        {
            return Usuario.GetPagedList(page, pageSize);
        }

        public SoftvList<UsuarioEntity> GetUsuarioPagedListXml(int page, int pageSize, String xml)
        {
            return Usuario.GetPagedList(page, pageSize, xml);
        }

        public int AddUsuario(UsuarioEntity objUsuario)
        {
            return Usuario.Add(objUsuario);
        }

        public int UpdateUsuario(UsuarioEntity objUsuario)
        {
            return Usuario.Edit(objUsuario);
        }

        public int DeleteUsuario(int? IdUsuario)
        {
            return Usuario.Delete(IdUsuario);
        }

        public int ChangeStateUsuario(UsuarioEntity objUsuario, bool State)
        {
            return Usuario.ChangeState(objUsuario.IdUsuario, State);
        }

        #endregion

        #region Role
        public RoleEntity GetRole(int? IdRol)
        {
            return Role.GetOne(IdRol);
        }

        public RoleEntity GetDeepRole(int? IdRol)
        {
            return Role.GetOneDeep(IdRol);
        }

        public IEnumerable<RoleEntity> GetRoleList()
        {
            return Role.GetAll();
        }

        public SoftvList<RoleEntity> GetRolePagedList(int page, int pageSize)
        {
            return Role.GetPagedList(page, pageSize);
        }

        public SoftvList<RoleEntity> GetRolePagedListXml(int page, int pageSize, String xml)
        {
            return Role.GetPagedList(page, pageSize, xml);
        }

        public int AddRole(RoleEntity objRole)
        {
            return Role.Add(objRole);
        }

        public int UpdateRole(RoleEntity objRole)
        {
            return Role.Edit(objRole);
        }

        public int DeleteRole(int? IdRol)
        {
            return Role.Delete(IdRol);
        }

        public int ChangeStateRole(RoleEntity objRole, bool State)
        {
            return Role.ChangeState(objRole.IdRol, State);
        }

        #endregion

        #region Module
        public ModuleEntity GetModule(int? IdModule)
        {
            return Module.GetOne(IdModule);
        }

        public ModuleEntity GetDeepModule(int? IdModule)
        {
            return Module.GetOneDeep(IdModule);
        }

        public IEnumerable<ModuleEntity> GetModuleList()
        {
            return Module.GetAll();
        }

        public SoftvList<ModuleEntity> GetModulePagedList(int page, int pageSize)
        {
            return Module.GetPagedList(page, pageSize);
        }

        public SoftvList<ModuleEntity> GetModulePagedListXml(int page, int pageSize, String xml)
        {
            return Module.GetPagedList(page, pageSize, xml);
        }

        public int AddModule(ModuleEntity objModule)
        {
            return Module.Add(objModule);
        }

        public int UpdateModule(ModuleEntity objModule)
        {
            return Module.Edit(objModule);
        }

        public int DeleteModule(int? IdModule)
        {
            return Module.Delete(IdModule);
        }

        #endregion

        #region Permiso


        public SoftvList<PermisoEntity> GetXmlPermiso(String xml)
        {
            return Permiso.GetXml(xml);
        }

        public int MargePermiso(int BaseIdUser, String BaseRemoteIp, String xml)
        {
            return Permiso.MargePermiso(xml);
        }


        #endregion



        #region Encuesta
        public EncuestaEntity GetEncuesta(int? IdEncuesta)
        {
            return Encuesta.GetOne(IdEncuesta);
        }

        public EncuestaEntity GetDeepEncuesta(int? IdEncuesta)
        {
            return Encuesta.GetOneDeep(IdEncuesta);
        }

        public IEnumerable<EncuestaEntity> GetEncuestaList()
        {
            return Encuesta.GetAll();
        }

        public SoftvList<EncuestaEntity> GetEncuestaPagedList(int page, int pageSize)
        {
            return Encuesta.GetPagedList(page, pageSize);
        }

        public SoftvList<EncuestaEntity> GetEncuestaPagedListXml(int page, int pageSize, String xml)
        {
            return Encuesta.GetPagedList(page, pageSize, xml);
        }

        public int AddEncuesta(string data)
        {
            return Encuesta.Add(data);
        }

        public int UpdateEncuesta(string data)
        {
            return Encuesta.Edit(data);
        }

        public int DeleteEncuesta(int? IdEncuesta)
        {
            return Encuesta.Delete(IdEncuesta);
        }




























        public int? AddEncuestaRel(EncuestaEntity lstEncuesta, List<PreguntaEntity> PreguntaAdd)
        {

            if (WebOperationContext.Current.IncomingRequest.Method == "OPTIONS")
            {
                return null;
            }
            else
            {
                lstEncuesta.PreguntaAdd = PreguntaAdd;

                XElement xe = XElement.Parse(Globals.SerializeTool.Serialize<EncuestaEntity>(lstEncuesta));

                XElement xmll = XElement.Parse(Globals.SerializeTool.SerializeList<PreguntaEntity>(lstEncuesta.PreguntaAdd, "PreguntaAdd"));


                xe.Add(xmll);
                try
                {
                    return Encuesta.AddEncuestaRel(xe.ToString());
                }
                catch (Exception ex)
                {
                    throw new WebFaultException<string>(ex.Message + " " + xe.ToString(), HttpStatusCode.ExpectationFailed);
                }
            }
        }




        #endregion

        #region Pregunta
        public PreguntaEntity GetPregunta(int? IdPregunta)
        {
            return Pregunta.GetOne(IdPregunta);
        }

        public PreguntaEntity GetDeepPregunta(int? IdPregunta)
        {
            return Pregunta.GetOneDeep(IdPregunta);
        }

        public IEnumerable<PreguntaEntity> GetPreguntaList()
        {
            return Pregunta.GetAll();
        }

        public SoftvList<PreguntaEntity> GetPreguntaPagedList(int page, int pageSize)
        {
            return Pregunta.GetPagedList(page, pageSize);
        }

        public SoftvList<PreguntaEntity> GetPreguntaPagedListXml(int page, int pageSize, String xml)
        {
            return Pregunta.GetPagedList(page, pageSize, xml);
        }

        public int AddPregunta(PreguntaEntity objPregunta)
        {
            return Pregunta.Add(objPregunta);
        }

        public int UpdatePregunta(PreguntaEntity objPregunta)
        {
            return Pregunta.Edit(objPregunta);
        }

        public int DeletePregunta(int? IdPregunta)
        {
            return Pregunta.Delete(IdPregunta);
        }

        #endregion

        #region TipoPreguntas
        public TipoPreguntasEntity GetTipoPreguntas(int? IdTipoPregunta)
        {
            return TipoPreguntas.GetOne(IdTipoPregunta);
        }

        public TipoPreguntasEntity GetDeepTipoPreguntas(int? IdTipoPregunta)
        {
            return TipoPreguntas.GetOneDeep(IdTipoPregunta);
        }

        public IEnumerable<TipoPreguntasEntity> GetTipoPreguntasList()
        {
            return TipoPreguntas.GetAll();
        }

        public SoftvList<TipoPreguntasEntity> GetTipoPreguntasPagedList(int page, int pageSize)
        {
            return TipoPreguntas.GetPagedList(page, pageSize);
        }

        public SoftvList<TipoPreguntasEntity> GetTipoPreguntasPagedListXml(int page, int pageSize, String xml)
        {
            return TipoPreguntas.GetPagedList(page, pageSize, xml);
        }

        public int AddTipoPreguntas(TipoPreguntasEntity objTipoPreguntas)
        {
            return TipoPreguntas.Add(objTipoPreguntas);
        }

        public int UpdateTipoPreguntas(TipoPreguntasEntity objTipoPreguntas)
        {
            return TipoPreguntas.Edit(objTipoPreguntas);
        }

        public int DeleteTipoPreguntas(int? IdTipoPregunta)
        {
            return TipoPreguntas.Delete(IdTipoPregunta);
        }

        #endregion

        #region RelEncuestaClientes
        public RelEncuestaClientesEntity GetRelEncuestaClientes(int? IdProceso)
        {
            return RelEncuestaClientes.GetOne(IdProceso);
        }

        public RelEncuestaClientesEntity GetDeepRelEncuestaClientes(int? IdProceso)
        {
            return RelEncuestaClientes.GetOneDeep(IdProceso);
        }

        public IEnumerable<RelEncuestaClientesEntity> GetRelEncuestaClientesList()
        {
            return RelEncuestaClientes.GetAll();
        }

        public SoftvList<RelEncuestaClientesEntity> GetRelEncuestaClientesPagedList(int page, int pageSize)
        {
            return RelEncuestaClientes.GetPagedList(page, pageSize);
        }

        public SoftvList<RelEncuestaClientesEntity> GetRelEncuestaClientesPagedListXml(int page, int pageSize, String xml)
        {
            return RelEncuestaClientes.GetPagedList(page, pageSize, xml);
        }

        public int AddRelEncuestaClientes(RelEncuestaClientesEntity objRelEncuestaClientes)
        {
            return RelEncuestaClientes.Add(objRelEncuestaClientes);
        }

        public int UpdateRelEncuestaClientes(RelEncuestaClientesEntity objRelEncuestaClientes)
        {
            return RelEncuestaClientes.Edit(objRelEncuestaClientes);
        }

        public int DeleteRelEncuestaClientes(int? IdProceso)
        {
            return RelEncuestaClientes.Delete(IdProceso);
        }

        #endregion

        #region RelPreguntaOpcMults
        public RelPreguntaOpcMultsEntity GetRelPreguntaOpcMults(int? IdPregunta)
        {
            return RelPreguntaOpcMults.GetOne(IdPregunta);
        }

        public RelPreguntaOpcMultsEntity GetDeepRelPreguntaOpcMults(int? IdPregunta)
        {
            return RelPreguntaOpcMults.GetOneDeep(IdPregunta);
        }

        public IEnumerable<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsList()
        {
            return RelPreguntaOpcMults.GetAll();
        }

        public SoftvList<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsPagedList(int page, int pageSize)
        {
            return RelPreguntaOpcMults.GetPagedList(page, pageSize);
        }

        public SoftvList<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsPagedListXml(int page, int pageSize, String xml)
        {
            return RelPreguntaOpcMults.GetPagedList(page, pageSize, xml);
        }

        public int AddRelPreguntaOpcMults(RelPreguntaOpcMultsEntity objRelPreguntaOpcMults)
        {
            return RelPreguntaOpcMults.Add(objRelPreguntaOpcMults);
        }

        public int UpdateRelPreguntaOpcMults(RelPreguntaOpcMultsEntity objRelPreguntaOpcMults)
        {
            return RelPreguntaOpcMults.Edit(objRelPreguntaOpcMults);
        }

        public int DeleteRelPreguntaOpcMults(String BaseRemoteIp, int BaseIdUser, int? IdPregunta)
        {
            return RelPreguntaOpcMults.Delete(IdPregunta);
        }

        #endregion

        #region RelPreguntaEncuestas
        public RelPreguntaEncuestasEntity GetRelPreguntaEncuestas(int? IdPregunta)
        {
            return RelPreguntaEncuestas.GetOne(IdPregunta);
        }

        public RelPreguntaEncuestasEntity GetDeepRelPreguntaEncuestas(int? IdPregunta)
        {
            return RelPreguntaEncuestas.GetOneDeep(IdPregunta);
        }

        public IEnumerable<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasList()
        {
            return RelPreguntaEncuestas.GetAll();
        }

        public SoftvList<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasPagedList(int page, int pageSize)
        {
            return RelPreguntaEncuestas.GetPagedList(page, pageSize);
        }

        public SoftvList<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasPagedListXml(int page, int pageSize, String xml)
        {
            return RelPreguntaEncuestas.GetPagedList(page, pageSize, xml);
        }

        public int AddRelPreguntaEncuestas(RelPreguntaEncuestasEntity objRelPreguntaEncuestas)
        {
            return RelPreguntaEncuestas.Add(objRelPreguntaEncuestas);
        }

        public int UpdateRelPreguntaEncuestas(RelPreguntaEncuestasEntity objRelPreguntaEncuestas)
        {
            return RelPreguntaEncuestas.Edit(objRelPreguntaEncuestas);
        }

        public int DeleteRelPreguntaEncuestas(String BaseRemoteIp, int BaseIdUser, int? IdPregunta)
        {
            return RelPreguntaEncuestas.Delete(IdPregunta);
        }

        #endregion

        #region ResOpcMults
        public ResOpcMultsEntity GetResOpcMults(int? Id_ResOpcMult)
        {
            return ResOpcMults.GetOne(Id_ResOpcMult);
        }

        public ResOpcMultsEntity GetDeepResOpcMults(int? Id_ResOpcMult)
        {
            return ResOpcMults.GetOneDeep(Id_ResOpcMult);
        }

        public IEnumerable<ResOpcMultsEntity> GetResOpcMultsList()
        {
            return ResOpcMults.GetAll();
        }

        public SoftvList<ResOpcMultsEntity> GetResOpcMultsPagedList(int page, int pageSize)
        {
            return ResOpcMults.GetPagedList(page, pageSize);
        }

        public SoftvList<ResOpcMultsEntity> GetResOpcMultsPagedListXml(int page, int pageSize, String xml)
        {
            return ResOpcMults.GetPagedList(page, pageSize, xml);
        }

        public int AddResOpcMults(ResOpcMultsEntity objResOpcMults)
        {
            return ResOpcMults.Add(objResOpcMults);
        }

        public int UpdateResOpcMults(ResOpcMultsEntity objResOpcMults)
        {
            return ResOpcMults.Edit(objResOpcMults);
        }

        public int DeleteResOpcMults(int? Id_ResOpcMult)
        {
            return ResOpcMults.Delete(Id_ResOpcMult);
        }

        #endregion

        #region RelEnProcesos
        public RelEnProcesosEntity GetRelEnProcesos(int? IdProceso)
        {
            return RelEnProcesos.GetOne(IdProceso);
        }

        public RelEnProcesosEntity GetDeepRelEnProcesos(int? IdProceso)
        {
            return RelEnProcesos.GetOneDeep(IdProceso);
        }

        public IEnumerable<RelEnProcesosEntity> GetRelEnProcesosList()
        {
            return RelEnProcesos.GetAll();
        }

        public SoftvList<RelEnProcesosEntity> GetRelEnProcesosPagedList(int page, int pageSize)
        {
            return RelEnProcesos.GetPagedList(page, pageSize);
        }

        public SoftvList<RelEnProcesosEntity> GetRelEnProcesosPagedListXml(int page, int pageSize, String xml)
        {
            return RelEnProcesos.GetPagedList(page, pageSize, xml);
        }

        public int AddRelEnProcesos(RelEnProcesosEntity objRelEnProcesos)
        {
            return RelEnProcesos.Add(objRelEnProcesos);
        }

        public int UpdateRelEnProcesos(RelEnProcesosEntity objRelEnProcesos)
        {
            return RelEnProcesos.Edit(objRelEnProcesos);
        }

        public int DeleteRelEnProcesos(String BaseRemoteIp, int BaseIdUser, int? IdProceso)
        {
            return RelEnProcesos.Delete(IdProceso);
        }

        #endregion

        #region Conexion
        public ConexionEntity GetConexion(int? IdConexion)
        {
            return Conexion.GetOne(IdConexion);
        }

        public ConexionEntity GetDeepConexion(int? IdConexion)
        {
            return Conexion.GetOneDeep(IdConexion);
        }

        public IEnumerable<ConexionEntity> GetConexionList()
        {
            return Conexion.GetAll();
        }

        public SoftvList<ConexionEntity> GetConexionPagedList(int page, int pageSize)
        {
            return Conexion.GetPagedList(page, pageSize);
        }

        public SoftvList<ConexionEntity> GetConexionPagedListXml(int page, int pageSize, String xml)
        {
            return Conexion.GetPagedList(page, pageSize, xml);
        }

        public int AddConexion(ConexionEntity objConexion)
        {
            return Conexion.Add(objConexion);
        }

        public int UpdateConexion(ConexionEntity objConexion)
        {
            return Conexion.Edit(objConexion);
        }

        public int DeleteConexion(String BaseRemoteIp, int BaseIdUser, int? IdConexion)
        {
            return Conexion.Delete(IdConexion);
        }

        #endregion

        #region CLIENTE
        public CLIENTEEntity GetCLIENTE(long? CONTRATO)
        {
            return CLIENTE.GetOne(CONTRATO);
        }

        public CLIENTEEntity GetDeepCLIENTE(long? CONTRATO)
        {
            return CLIENTE.GetOneDeep(CONTRATO);
        }

        public IEnumerable<CLIENTEEntity> GetCLIENTEList()
        {
            return CLIENTE.GetAll();
        }

        public SoftvList<CLIENTEEntity> GetCLIENTEPagedList(int page, int pageSize)
        {
            return CLIENTE.GetPagedList(page, pageSize);
        }

        public SoftvList<CLIENTEEntity> GetCLIENTEPagedListXml(int page, int pageSize, String xml)
        {
            return CLIENTE.GetPagedList(page, pageSize, xml);
        }

        public int AddCLIENTE(CLIENTEEntity objCLIENTE)
        {
            return CLIENTE.Add(objCLIENTE);
        }

        public int UpdateCLIENTE(CLIENTEEntity objCLIENTE)
        {
            return CLIENTE.Edit(objCLIENTE);
        }

        public int DeleteCLIENTE(String BaseRemoteIp, int BaseIdUser, long? CONTRATO)
        {
            return CLIENTE.Delete(CONTRATO);
        }

        #endregion




        #region TipServ
        public TipServEntity GetTipServ(int? Clv_TipSer)
        {
            return TipServ.GetOne(Clv_TipSer);
        }

        public TipServEntity GetDeepTipServ(int? Clv_TipSer)
        {
            return TipServ.GetOneDeep(Clv_TipSer);
        }

        public IEnumerable<TipServEntity> GetTipServList()
        {
            return TipServ.GetAll();
        }

        public SoftvList<TipServEntity> GetTipServPagedList(int page, int pageSize)
        {
            return TipServ.GetPagedList(page, pageSize);
        }

        public SoftvList<TipServEntity> GetTipServPagedListXml(int page, int pageSize, String xml)
        {
            return TipServ.GetPagedList(page, pageSize, xml);
        }

        public int AddTipServ(TipServEntity objTipServ)
        {
            return TipServ.Add(objTipServ);
        }

        public int UpdateTipServ(TipServEntity objTipServ)
        {
            return TipServ.Edit(objTipServ);
        }

        public int DeleteTipServ(String BaseRemoteIp, int BaseIdUser, int? Clv_TipSer)
        {
            return TipServ.Delete(Clv_TipSer);
        }

        #endregion

        #region Turno
        public TurnoEntity GetTurno(int? IdTurno)
        {
            return Turno.GetOne(IdTurno);
        }

        public TurnoEntity GetDeepTurno(int? IdTurno)
        {
            return Turno.GetOneDeep(IdTurno);
        }

        public IEnumerable<TurnoEntity> GetTurnoList()
        {
            return Turno.GetAll();
        }

        public SoftvList<TurnoEntity> GetTurnoPagedList(int page, int pageSize)
        {
            return Turno.GetPagedList(page, pageSize);
        }

        public SoftvList<TurnoEntity> GetTurnoPagedListXml(int page, int pageSize, String xml)
        {
            return Turno.GetPagedList(page, pageSize, xml);
        }

        public int AddTurno(TurnoEntity objTurno)
        {
            return Turno.Add(objTurno);
        }

        public int UpdateTurno(TurnoEntity objTurno)
        {
            return Turno.Edit(objTurno);
        }

        public int DeleteTurno(String BaseRemoteIp, int BaseIdUser, int? IdTurno)
        {
            return Turno.Delete(IdTurno);
        }

        #endregion

        #region Rel_Clientes_TiposClientes
        public Rel_Clientes_TiposClientesEntity GetRel_Clientes_TiposClientes(long? CONTRATO)
        {
            return Rel_Clientes_TiposClientes.GetOne(CONTRATO);
        }

        public Rel_Clientes_TiposClientesEntity GetDeepRel_Clientes_TiposClientes(long? CONTRATO)
        {
            return Rel_Clientes_TiposClientes.GetOneDeep(CONTRATO);
        }

        public IEnumerable<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesList()
        {
            return Rel_Clientes_TiposClientes.GetAll();
        }

        public SoftvList<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesPagedList(int page, int pageSize)
        {
            return Rel_Clientes_TiposClientes.GetPagedList(page, pageSize);
        }

        public SoftvList<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesPagedListXml(int page, int pageSize, String xml)
        {
            return Rel_Clientes_TiposClientes.GetPagedList(page, pageSize, xml);
        }

        public int AddRel_Clientes_TiposClientes(Rel_Clientes_TiposClientesEntity objRel_Clientes_TiposClientes)
        {
            return Rel_Clientes_TiposClientes.Add(objRel_Clientes_TiposClientes);
        }

        public int UpdateRel_Clientes_TiposClientes(Rel_Clientes_TiposClientesEntity objRel_Clientes_TiposClientes)
        {
            return Rel_Clientes_TiposClientes.Edit(objRel_Clientes_TiposClientes);
        }

        public int DeleteRel_Clientes_TiposClientes(String BaseRemoteIp, int BaseIdUser, long? CONTRATO)
        {
            return Rel_Clientes_TiposClientes.Delete(CONTRATO);
        }

        #endregion

        #region TipoCliente
        public TipoClienteEntity GetTipoCliente(int? Clv_TipoCliente)
        {
            return TipoCliente.GetOne(Clv_TipoCliente);
        }

        public TipoClienteEntity GetDeepTipoCliente(int? Clv_TipoCliente)
        {
            return TipoCliente.GetOneDeep(Clv_TipoCliente);
        }

        public IEnumerable<TipoClienteEntity> GetTipoClienteList()
        {
            return TipoCliente.GetAll();
        }

        public SoftvList<TipoClienteEntity> GetTipoClientePagedList(int page, int pageSize)
        {
            return TipoCliente.GetPagedList(page, pageSize);
        }

        public SoftvList<TipoClienteEntity> GetTipoClientePagedListXml(int page, int pageSize, String xml)
        {
            return TipoCliente.GetPagedList(page, pageSize, xml);
        }

        public int AddTipoCliente(TipoClienteEntity objTipoCliente)
        {
            return TipoCliente.Add(objTipoCliente);
        }

        public int UpdateTipoCliente(TipoClienteEntity objTipoCliente)
        {
            return TipoCliente.Edit(objTipoCliente);
        }

        public int DeleteTipoCliente(String BaseRemoteIp, int BaseIdUser, int? Clv_TipoCliente)
        {
            return TipoCliente.Delete(Clv_TipoCliente);
        }

        #endregion

        #region CatalogoPeriodosCorte
        public CatalogoPeriodosCorteEntity GetCatalogoPeriodosCorte(int? Clv_Periodo)
        {
            return CatalogoPeriodosCorte.GetOne(Clv_Periodo);
        }

        public CatalogoPeriodosCorteEntity GetDeepCatalogoPeriodosCorte(int? Clv_Periodo)
        {
            return CatalogoPeriodosCorte.GetOneDeep(Clv_Periodo);
        }

        public IEnumerable<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCorteList()
        {
            return CatalogoPeriodosCorte.GetAll();
        }

        public SoftvList<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCortePagedList(int page, int pageSize)
        {
            return CatalogoPeriodosCorte.GetPagedList(page, pageSize);
        }

        public SoftvList<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCortePagedListXml(int page, int pageSize, String xml)
        {
            return CatalogoPeriodosCorte.GetPagedList(page, pageSize, xml);
        }

        public int AddCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity objCatalogoPeriodosCorte)
        {
            return CatalogoPeriodosCorte.Add(objCatalogoPeriodosCorte);
        }

        public int UpdateCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity objCatalogoPeriodosCorte)
        {
            return CatalogoPeriodosCorte.Edit(objCatalogoPeriodosCorte);
        }

        public int DeleteCatalogoPeriodosCorte(String BaseRemoteIp, int BaseIdUser, int? Clv_Periodo)
        {
            return CatalogoPeriodosCorte.Delete(Clv_Periodo);
        }

        #endregion

        #region Cliente_Apellido
        public Cliente_ApellidoEntity GetCliente_Apellido(long? Contrato)
        {
            return Cliente_Apellido.GetOne(Contrato);
        }

        public Cliente_ApellidoEntity GetDeepCliente_Apellido(long? Contrato)
        {
            return Cliente_Apellido.GetOneDeep(Contrato);
        }

        public IEnumerable<Cliente_ApellidoEntity> GetCliente_ApellidoList()
        {
            return Cliente_Apellido.GetAll();
        }

        public SoftvList<Cliente_ApellidoEntity> GetCliente_ApellidoPagedList(int page, int pageSize)
        {
            return Cliente_Apellido.GetPagedList(page, pageSize);
        }

        public SoftvList<Cliente_ApellidoEntity> GetCliente_ApellidoPagedListXml(int page, int pageSize, String xml)
        {
            return Cliente_Apellido.GetPagedList(page, pageSize, xml);
        }

        public int AddCliente_Apellido(Cliente_ApellidoEntity objCliente_Apellido)
        {
            return Cliente_Apellido.Add(objCliente_Apellido);
        }

        public int UpdateCliente_Apellido(Cliente_ApellidoEntity objCliente_Apellido)
        {
            return Cliente_Apellido.Edit(objCliente_Apellido);
        }

        public int DeleteCliente_Apellido(String BaseRemoteIp, int BaseIdUser, long? Contrato)
        {
            return Cliente_Apellido.Delete(Contrato);
        }

        #endregion

        #region Tap
        public TapEntity GetTap(int? IdTap)
        {
            return Tap.GetOne(IdTap);
        }

        public TapEntity GetDeepTap(int? IdTap)
        {
            return Tap.GetOneDeep(IdTap);
        }

        public IEnumerable<TapEntity> GetTapList()
        {
            return Tap.GetAll();
        }

        public SoftvList<TapEntity> GetTapPagedList(int page, int pageSize)
        {
            return Tap.GetPagedList(page, pageSize);
        }

        public SoftvList<TapEntity> GetTapPagedListXml(int page, int pageSize, String xml)
        {
            return Tap.GetPagedList(page, pageSize, xml);
        }

        public int AddTap(TapEntity objTap)
        {
            return Tap.Add(objTap);
        }

        public int UpdateTap(TapEntity objTap)
        {
            return Tap.Edit(objTap);
        }

        public int DeleteTap(String BaseRemoteIp, int BaseIdUser, int? IdTap)
        {
            return Tap.Delete(IdTap);
        }

        #endregion

        #region DatoFiscal
        public DatoFiscalEntity GetDatoFiscal(long? Contrato)
        {
            return DatoFiscal.GetOne(Contrato);
        }

        public DatoFiscalEntity GetDeepDatoFiscal(long? Contrato)
        {
            return DatoFiscal.GetOneDeep(Contrato);
        }

        public IEnumerable<DatoFiscalEntity> GetDatoFiscalList()
        {
            return DatoFiscal.GetAll();
        }

        public SoftvList<DatoFiscalEntity> GetDatoFiscalPagedList(int page, int pageSize)
        {
            return DatoFiscal.GetPagedList(page, pageSize);
        }

        public SoftvList<DatoFiscalEntity> GetDatoFiscalPagedListXml(int page, int pageSize, String xml)
        {
            return DatoFiscal.GetPagedList(page, pageSize, xml);
        }

        public int AddDatoFiscal(DatoFiscalEntity objDatoFiscal)
        {
            return DatoFiscal.Add(objDatoFiscal);
        }

        public int UpdateDatoFiscal(DatoFiscalEntity objDatoFiscal)
        {
            return DatoFiscal.Edit(objDatoFiscal);
        }

        public int DeleteDatoFiscal(String BaseRemoteIp, int BaseIdUser, long? Contrato)
        {
            return DatoFiscal.Delete(Contrato);
        }

        #endregion

        #region Trabajo
        public TrabajoEntity GetTrabajo(int? Clv_Trabajo)
        {
            return Trabajo.GetOne(Clv_Trabajo);
        }

        public TrabajoEntity GetDeepTrabajo(int? Clv_Trabajo)
        {
            return Trabajo.GetOneDeep(Clv_Trabajo);
        }

        public IEnumerable<TrabajoEntity> GetTrabajoList()
        {
            return Trabajo.GetAll();
        }

        public SoftvList<TrabajoEntity> GetTrabajoPagedList(int page, int pageSize)
        {
            return Trabajo.GetPagedList(page, pageSize);
        }

        public SoftvList<TrabajoEntity> GetTrabajoPagedListXml(int page, int pageSize, String xml)
        {
            return Trabajo.GetPagedList(page, pageSize, xml);
        }

        public int AddTrabajo(TrabajoEntity objTrabajo)
        {
            return Trabajo.Add(objTrabajo);
        }

        public int UpdateTrabajo(TrabajoEntity objTrabajo)
        {
            return Trabajo.Edit(objTrabajo);
        }

        public int DeleteTrabajo(String BaseRemoteIp, int BaseIdUser, int? Clv_Trabajo)
        {
            return Trabajo.Delete(Clv_Trabajo);
        }

        #endregion

        #region Llamada
        public LlamadaEntity GetLlamada(int? IdLlamada)
        {
            return Llamada.GetOne(IdLlamada);
        }

        public LlamadaEntity GetDeepLlamada(int? IdLlamada)
        {
            return Llamada.GetOneDeep(IdLlamada);
        }

        public IEnumerable<LlamadaEntity> GetLlamadaList()
        {
            return Llamada.GetAll();
        }

        public SoftvList<LlamadaEntity> GetLlamadaPagedList(int page, int pageSize)
        {
            return Llamada.GetPagedList(page, pageSize);
        }

        public SoftvList<LlamadaEntity> GetLlamadaPagedListXml(int page, int pageSize, String xml)
        {
            return Llamada.GetPagedList(page, pageSize, xml);
        }

        public int AddLlamada(LlamadaEntity objLlamada)
        {
            return Llamada.Add(objLlamada);
        }

        public int UpdateLlamada(LlamadaEntity objLlamada)
        {
            return Llamada.Edit(objLlamada);
        }

        public int DeleteLlamada(String BaseRemoteIp, int BaseIdUser, int? IdLlamada)
        {
            return Llamada.Delete(IdLlamada);
        }

        #endregion




        #region MotivoCancelacion
        public MotivoCancelacionEntity GetMotivoCancelacion(int? Clv_MOTCAN)
        {
            return MotivoCancelacion.GetOne(Clv_MOTCAN);
        }

        public MotivoCancelacionEntity GetDeepMotivoCancelacion(int? Clv_MOTCAN)
        {
            return MotivoCancelacion.GetOneDeep(Clv_MOTCAN);
        }

        public IEnumerable<MotivoCancelacionEntity> GetMotivoCancelacionList()
        {
            return MotivoCancelacion.GetAll();
        }

        public SoftvList<MotivoCancelacionEntity> GetMotivoCancelacionPagedList(int page, int pageSize)
        {
            return MotivoCancelacion.GetPagedList(page, pageSize);
        }

        public SoftvList<MotivoCancelacionEntity> GetMotivoCancelacionPagedListXml(int page, int pageSize, String xml)
        {
            return MotivoCancelacion.GetPagedList(page, pageSize, xml);
        }

        public int AddMotivoCancelacion(MotivoCancelacionEntity objMotivoCancelacion)
        {
            return MotivoCancelacion.Add(objMotivoCancelacion);
        }

        public int UpdateMotivoCancelacion(MotivoCancelacionEntity objMotivoCancelacion)
        {
            return MotivoCancelacion.Edit(objMotivoCancelacion);
        }

        public int DeleteMotivoCancelacion(String BaseRemoteIp, int BaseIdUser, int? Clv_MOTCAN)
        {
            return MotivoCancelacion.Delete(Clv_MOTCAN);
        }

        #endregion

        #region RelEncuestaPreguntaRes
        public RelEncuestaPreguntaResEntity GetRelEncuestaPreguntaRes(int? Id)
        {
            return RelEncuestaPreguntaRes.GetOne(Id);
        }

        public RelEncuestaPreguntaResEntity GetDeepRelEncuestaPreguntaRes(int? Id)
        {
            return RelEncuestaPreguntaRes.GetOneDeep(Id);
        }

        public IEnumerable<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResList()
        {
            return RelEncuestaPreguntaRes.GetAll();
        }

        public SoftvList<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResPagedList(int page, int pageSize)
        {
            return RelEncuestaPreguntaRes.GetPagedList(page, pageSize);
        }

        public SoftvList<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResPagedListXml(int page, int pageSize, String xml)
        {
            return RelEncuestaPreguntaRes.GetPagedList(page, pageSize, xml);
        }

        public int AddRelEncuestaPreguntaRes(RelEncuestaPreguntaResEntity objRelEncuestaPreguntaRes)
        {
            return RelEncuestaPreguntaRes.Add(objRelEncuestaPreguntaRes);
        }

        public int UpdateRelEncuestaPreguntaRes(RelEncuestaPreguntaResEntity objRelEncuestaPreguntaRes)
        {
            return RelEncuestaPreguntaRes.Edit(objRelEncuestaPreguntaRes);
        }

        public int DeleteRelEncuestaPreguntaRes(String BaseRemoteIp, int BaseIdUser, int? Id)
        {
            return RelEncuestaPreguntaRes.Delete(Id);
        }

        #endregion

        #region Queja
        public QuejaEntity GetQueja(long? Clv_Queja)
        {
            return Queja.GetOne(Clv_Queja);
        }

        public QuejaEntity GetDeepQueja(long? Clv_Queja)
        {
            return Queja.GetOneDeep(Clv_Queja);
        }

        public IEnumerable<QuejaEntity> GetQuejaList()
        {
            return Queja.GetAll();
        }

        public SoftvList<QuejaEntity> GetQuejaPagedList(int page, int pageSize)
        {
            return Queja.GetPagedList(page, pageSize);
        }

        public SoftvList<QuejaEntity> GetQuejaPagedListXml(int page, int pageSize, String xml)
        {
            return Queja.GetPagedList(page, pageSize, xml);
        }

        public int AddQueja(QuejaEntity objQueja)
        {
            return Queja.Add(objQueja);
        }

        public int UpdateQueja(QuejaEntity objQueja)
        {
            return Queja.Edit(objQueja);
        }

        public int DeleteQueja(String BaseRemoteIp, int BaseIdUser, long? Clv_Queja)
        {
            return Queja.Delete(Clv_Queja);
        }

        #endregion

        #region CIUDAD
        public CIUDADEntity GetCIUDAD(int? Clv_Ciudad)
        {
            return CIUDAD.GetOne(Clv_Ciudad);
        }

        public CIUDADEntity GetDeepCIUDAD(int? Clv_Ciudad)
        {
            return CIUDAD.GetOneDeep(Clv_Ciudad);
        }

        public IEnumerable<CIUDADEntity> GetCIUDADList()
        {
            return CIUDAD.GetAll();
        }

        public SoftvList<CIUDADEntity> GetCIUDADPagedList(int page, int pageSize)
        {
            return CIUDAD.GetPagedList(page, pageSize);
        }

        public SoftvList<CIUDADEntity> GetCIUDADPagedListXml(int page, int pageSize, String xml)
        {
            return CIUDAD.GetPagedList(page, pageSize, xml);
        }

        public int AddCIUDAD(CIUDADEntity objCIUDAD)
        {
            return CIUDAD.Add(objCIUDAD);
        }

        public int UpdateCIUDAD(CIUDADEntity objCIUDAD)
        {
            return CIUDAD.Edit(objCIUDAD);
        }

        public int DeleteCIUDAD(String BaseRemoteIp, int BaseIdUser, int? Clv_Ciudad)
        {
            return CIUDAD.Delete(Clv_Ciudad);
        }

        #endregion

        #region CVECOLCIU
        public CVECOLCIUEntity GetCVECOLCIU(int? Clv_Ciudad)
        {
            return CVECOLCIU.GetOne(Clv_Ciudad);
        }

        public CVECOLCIUEntity GetDeepCVECOLCIU(int? Clv_Colonia, int? Clv_Ciudad)
        {
            return CVECOLCIU.GetOneDeep(Clv_Colonia, Clv_Ciudad);
        }

        public IEnumerable<CVECOLCIUEntity> GetCVECOLCIUList()
        {
            return CVECOLCIU.GetAll();
        }

        public SoftvList<CVECOLCIUEntity> GetCVECOLCIUPagedList(int page, int pageSize)
        {
            return CVECOLCIU.GetPagedList(page, pageSize);
        }

        public SoftvList<CVECOLCIUEntity> GetCVECOLCIUPagedListXml(int page, int pageSize, String xml)
        {
            return CVECOLCIU.GetPagedList(page, pageSize, xml);
        }

        public int AddCVECOLCIU(CVECOLCIUEntity objCVECOLCIU)
        {
            return CVECOLCIU.Add(objCVECOLCIU);
        }

        public int UpdateCVECOLCIU(CVECOLCIUEntity objCVECOLCIU)
        {
            return CVECOLCIU.Edit(objCVECOLCIU);
        }

        public int DeleteCVECOLCIU(int? Clv_Ciudad)
        {
            return CVECOLCIU.Delete(Clv_Ciudad);
        }

        #endregion

        #region COLONIA
        public COLONIAEntity GetCOLONIA(int? Clv_Colonia)
        {
            return COLONIA.GetOne(Clv_Colonia);
        }

        public COLONIAEntity GetDeepCOLONIA(int? Clv_Colonia)
        {
            return COLONIA.GetOneDeep(Clv_Colonia);
        }

        public IEnumerable<COLONIAEntity> GetCOLONIAList()
        {
            return COLONIA.GetAll();
        }

        public SoftvList<COLONIAEntity> GetCOLONIAPagedList(int page, int pageSize)
        {
            return COLONIA.GetPagedList(page, pageSize);
        }

        public SoftvList<COLONIAEntity> GetCOLONIAPagedListXml(int page, int pageSize, String xml)
        {
            return COLONIA.GetPagedList(page, pageSize, xml);
        }

        public int AddCOLONIA(COLONIAEntity objCOLONIA)
        {
            return COLONIA.Add(objCOLONIA);
        }

        public int UpdateCOLONIA(COLONIAEntity objCOLONIA)
        {
            return COLONIA.Edit(objCOLONIA);
        }

        public int DeleteCOLONIA(String BaseRemoteIp, int BaseIdUser, int? Clv_Colonia)
        {
            return COLONIA.Delete(Clv_Colonia);
        }

        #endregion

        #region CVECAROL
        public CVECAROLEntity GetCVECAROL(int? Clv_Colonia)
        {
            return CVECAROL.GetOne(Clv_Colonia);
        }

        public CVECAROLEntity GetDeepCVECAROL(int? Clv_Calle, int? Clv_Colonia)
        {
            return CVECAROL.GetOneDeep(Clv_Calle, Clv_Colonia);
        }

        public IEnumerable<CVECAROLEntity> GetCVECAROLList()
        {
            return CVECAROL.GetAll();
        }

        public SoftvList<CVECAROLEntity> GetCVECAROLPagedList(int page, int pageSize)
        {
            return CVECAROL.GetPagedList(page, pageSize);
        }

        public SoftvList<CVECAROLEntity> GetCVECAROLPagedListXml(int page, int pageSize, String xml)
        {
            return CVECAROL.GetPagedList(page, pageSize, xml);
        }

        public int AddCVECAROL(CVECAROLEntity objCVECAROL)
        {
            return CVECAROL.Add(objCVECAROL);
        }

        public int UpdateCVECAROL(CVECAROLEntity objCVECAROL)
        {
            return CVECAROL.Edit(objCVECAROL);
        }

        public int DeleteCVECAROL(int? Clv_Colonia)
        {
            return CVECAROL.Delete(Clv_Colonia);
        }

        #endregion

        #region CALLE
        public CALLEEntity GetCALLE(int? Clv_Calle)
        {
            return CALLE.GetOne(Clv_Calle);
        }

        public CALLEEntity GetDeepCALLE(int? Clv_Calle)
        {
            return CALLE.GetOneDeep(Clv_Calle);
        }

        public IEnumerable<CALLEEntity> GetCALLEList()
        {
            return CALLE.GetAll();
        }

        public SoftvList<CALLEEntity> GetCALLEPagedList(int page, int pageSize)
        {
            return CALLE.GetPagedList(page, pageSize);
        }

        public SoftvList<CALLEEntity> GetCALLEPagedListXml(int page, int pageSize, String xml)
        {
            return CALLE.GetPagedList(page, pageSize, xml);
        }

        public int AddCALLE(CALLEEntity objCALLE)
        {
            return CALLE.Add(objCALLE);
        }

        public int UpdateCALLE(CALLEEntity objCALLE)
        {
            return CALLE.Edit(objCALLE);
        }

        public int DeleteCALLE(String BaseRemoteIp, int BaseIdUser, int? Clv_Calle)
        {
            return CALLE.Delete(Clv_Calle);
        }

        #endregion




    }
}
