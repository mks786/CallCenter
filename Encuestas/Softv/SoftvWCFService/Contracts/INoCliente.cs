
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
      public interface INoCliente
      {
        [OperationContract]
        NoClienteEntity GetNoCliente(int? Id);
        [OperationContract]
        NoClienteEntity GetDeepNoCliente(int? Id);
        [OperationContract]
        IEnumerable<NoClienteEntity> GetNoClienteList();
        [OperationContract]
        SoftvList<NoClienteEntity> GetNoClientePagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<NoClienteEntity> GetNoClientePagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddNoCliente(NoClienteEntity objNoCliente);
        [OperationContract]
        int UpdateNoCliente(NoClienteEntity objNoCliente);
        [OperationContract]
        int DeleteNoCliente(String BaseRemoteIp, int BaseIdUser,int? Id);
        
      }
    }

  