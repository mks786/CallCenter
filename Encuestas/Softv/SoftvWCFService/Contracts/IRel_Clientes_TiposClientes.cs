
    using Globals;
    using Softv.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;

    namespace SoftvWCFService.Contracts
    {
      [ServiceContract]
      public interface IRel_Clientes_TiposClientes
      {

        [OperationContract]
        Rel_Clientes_TiposClientesEntity GetRel_Clientes_TiposClientes(long? CONTRATO);

        [OperationContract]
        Rel_Clientes_TiposClientesEntity GetDeepRel_Clientes_TiposClientes(long? CONTRATO);

        [OperationContract]
        IEnumerable<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesList();

        [OperationContract]
        SoftvList<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesPagedList(int page, int pageSize);

        [OperationContract]
        SoftvList<Rel_Clientes_TiposClientesEntity> GetRel_Clientes_TiposClientesPagedListXml(int page, int pageSize, String xml);

        [OperationContract]
        int AddRel_Clientes_TiposClientes(Rel_Clientes_TiposClientesEntity objRel_Clientes_TiposClientes);

        [OperationContract]
        int UpdateRel_Clientes_TiposClientes(Rel_Clientes_TiposClientesEntity objRel_Clientes_TiposClientes);

        [OperationContract]
        int DeleteRel_Clientes_TiposClientes(String BaseRemoteIp, int BaseIdUser, long? CONTRATO);
        
      }
    }

  