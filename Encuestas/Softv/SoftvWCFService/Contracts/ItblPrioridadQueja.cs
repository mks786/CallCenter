
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
      public interface ItblPrioridadQueja
      {
        [OperationContract]
        tblPrioridadQuejaEntity GettblPrioridadQueja(int? clvPrioridadQueja);
        [OperationContract]
        tblPrioridadQuejaEntity GetDeeptblPrioridadQueja(int? clvPrioridadQueja);
        [OperationContract]
        IEnumerable<tblPrioridadQuejaEntity> GettblPrioridadQuejaList();
        [OperationContract]
        SoftvList<tblPrioridadQuejaEntity> GettblPrioridadQuejaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<tblPrioridadQuejaEntity> GettblPrioridadQuejaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddtblPrioridadQueja(tblPrioridadQuejaEntity objtblPrioridadQueja);
        [OperationContract]
        int UpdatetblPrioridadQueja(tblPrioridadQuejaEntity objtblPrioridadQueja);
        [OperationContract]
        int DeletetblPrioridadQueja(String BaseRemoteIp, int BaseIdUser,int? clvPrioridadQueja);
        
      }
    }

  