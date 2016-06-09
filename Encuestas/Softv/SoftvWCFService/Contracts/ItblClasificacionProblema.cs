
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
      public interface ItblClasificacionProblema
      {
        [OperationContract]
        tblClasificacionProblemaEntity GettblClasificacionProblema(long? clvProblema);
        [OperationContract]
        tblClasificacionProblemaEntity GetDeeptblClasificacionProblema(long? clvProblema);
        [OperationContract]
        IEnumerable<tblClasificacionProblemaEntity> GettblClasificacionProblemaList();
        [OperationContract]
        SoftvList<tblClasificacionProblemaEntity> GettblClasificacionProblemaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<tblClasificacionProblemaEntity> GettblClasificacionProblemaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddtblClasificacionProblema(tblClasificacionProblemaEntity objtblClasificacionProblema);
        [OperationContract]
        int UpdatetblClasificacionProblema(tblClasificacionProblemaEntity objtblClasificacionProblema);
        [OperationContract]
        int DeletetblClasificacionProblema(String BaseRemoteIp, int BaseIdUser,long? clvProblema);
        
      }
    }

  