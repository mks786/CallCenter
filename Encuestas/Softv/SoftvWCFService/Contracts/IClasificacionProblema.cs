
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
      public interface IClasificacionProblema
      {
        [OperationContract]
        ClasificacionProblemaEntity GetClasificacionProblema(long? ClvProblema);
        [OperationContract]
        ClasificacionProblemaEntity GetDeepClasificacionProblema(long? ClvProblema);
        [OperationContract]
        IEnumerable<ClasificacionProblemaEntity> GetClasificacionProblemaList();
        [OperationContract]
        SoftvList<ClasificacionProblemaEntity> GetClasificacionProblemaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ClasificacionProblemaEntity> GetClasificacionProblemaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddClasificacionProblema(ClasificacionProblemaEntity objClasificacionProblema);
        [OperationContract]
        int UpdateClasificacionProblema(ClasificacionProblemaEntity objClasificacionProblema);
        [OperationContract]
        int DeleteClasificacionProblema(String BaseRemoteIp, int BaseIdUser,long? ClvProblema);
        
      }
    }

  