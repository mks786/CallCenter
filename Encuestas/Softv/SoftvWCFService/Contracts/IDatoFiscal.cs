
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
      public interface IDatoFiscal
      {
        [OperationContract]
          DatoFiscalEntity GetDatoFiscal(long? Contrato);
        [OperationContract]
        DatoFiscalEntity GetDeepDatoFiscal(long? Contrato);
        [OperationContract]
        IEnumerable<DatoFiscalEntity> GetDatoFiscalList();
        [OperationContract]
        SoftvList<DatoFiscalEntity> GetDatoFiscalPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<DatoFiscalEntity> GetDatoFiscalPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddDatoFiscal(DatoFiscalEntity objDatoFiscal);
        [OperationContract]
        int UpdateDatoFiscal(DatoFiscalEntity objDatoFiscal);
        [OperationContract]
        int DeleteDatoFiscal(String BaseRemoteIp, int BaseIdUser, long? Contrato);
        
      }
    }

  