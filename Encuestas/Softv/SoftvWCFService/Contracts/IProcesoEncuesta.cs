
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
      public interface IProcesoEncuesta
      {
        [OperationContract]
        ProcesoEncuestaEntity GetProcesoEncuesta(int? IdProcesoEnc);
        [OperationContract]
        ProcesoEncuestaEntity GetDeepProcesoEncuesta(int? IdProcesoEnc);
        [OperationContract]
        IEnumerable<ProcesoEncuestaEntity> GetProcesoEncuestaList();
        [OperationContract]
        SoftvList<ProcesoEncuestaEntity> GetProcesoEncuestaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ProcesoEncuestaEntity> GetProcesoEncuestaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddProcesoEncuesta(ProcesoEncuestaEntity objProcesoEncuesta);
        [OperationContract]
        int UpdateProcesoEncuesta(ProcesoEncuestaEntity objProcesoEncuesta);
        [OperationContract]
        int DeleteProcesoEncuesta(String BaseRemoteIp, int BaseIdUser,int? IdProcesoEnc);
        
      }
    }

  