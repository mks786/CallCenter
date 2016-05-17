
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
      public interface ICLIENTE
      {
        [OperationContract]
        CLIENTEEntity GetCLIENTE(long? CONTRATO);
        [OperationContract]
        CLIENTEEntity GetDeepCLIENTE(long? CONTRATO);
        [OperationContract]
        IEnumerable<CLIENTEEntity> GetCLIENTEList();
        [OperationContract]
        SoftvList<CLIENTEEntity> GetCLIENTEPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CLIENTEEntity> GetCLIENTEPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCLIENTE(CLIENTEEntity objCLIENTE);
        [OperationContract]
        int UpdateCLIENTE(CLIENTEEntity objCLIENTE);
        [OperationContract]
        int DeleteCLIENTE(String BaseRemoteIp, int BaseIdUser,long? CONTRATO);
        
      }
    }

  