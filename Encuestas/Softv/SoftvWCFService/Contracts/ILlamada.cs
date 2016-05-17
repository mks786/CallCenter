
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
      public interface ILlamada
      {
        [OperationContract]
        LlamadaEntity GetLlamada(int? IdLlamada);
        [OperationContract]
        LlamadaEntity GetDeepLlamada(int? IdLlamada);
        [OperationContract]
        IEnumerable<LlamadaEntity> GetLlamadaList();
        [OperationContract]
        SoftvList<LlamadaEntity> GetLlamadaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<LlamadaEntity> GetLlamadaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddLlamada(LlamadaEntity objLlamada);
        [OperationContract]
        int UpdateLlamada(LlamadaEntity objLlamada);
        [OperationContract]
        int DeleteLlamada(String BaseRemoteIp, int BaseIdUser,int? IdLlamada);
        
      }
    }

  