
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
      public interface IDatosLlamada
      {
        [OperationContract]
        DatosLlamadaEntity GetDatosLlamada(int? Id);
        [OperationContract]
        DatosLlamadaEntity GetDeepDatosLlamada(int? Id);
        [OperationContract]
        IEnumerable<DatosLlamadaEntity> GetDatosLlamadaList();
        [OperationContract]
        SoftvList<DatosLlamadaEntity> GetDatosLlamadaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<DatosLlamadaEntity> GetDatosLlamadaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddDatosLlamada(DatosLlamadaEntity objDatosLlamada);
        [OperationContract]
        int UpdateDatosLlamada(DatosLlamadaEntity objDatosLlamada);
        [OperationContract]
        int DeleteDatosLlamada(String BaseRemoteIp, int BaseIdUser,int? Id);
        
      }
    }

  