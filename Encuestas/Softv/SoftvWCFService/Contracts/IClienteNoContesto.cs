
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
      public interface IClienteNoContesto
      {
        [OperationContract]
        ClienteNoContestoEntity GetClienteNoContesto(int? IdNoContesto);
        [OperationContract]
        ClienteNoContestoEntity GetDeepClienteNoContesto(int? IdNoContesto);
        [OperationContract]
        IEnumerable<ClienteNoContestoEntity> GetClienteNoContestoList();
        [OperationContract]
        SoftvList<ClienteNoContestoEntity> GetClienteNoContestoPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ClienteNoContestoEntity> GetClienteNoContestoPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddClienteNoContesto(ClienteNoContestoEntity objClienteNoContesto);
        [OperationContract]
        int UpdateClienteNoContesto(ClienteNoContestoEntity objClienteNoContesto);
        [OperationContract]
        int DeleteClienteNoContesto(String BaseRemoteIp, int BaseIdUser,int? IdNoContesto);
        
      }
    }

  