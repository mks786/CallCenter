
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
      public interface ICiudadServidor
      {
        [OperationContract]
        CiudadServidorEntity GetCiudadServidor(int? Id);
        [OperationContract]
        CiudadServidorEntity GetDeepCiudadServidor(int? Id);
        [OperationContract]
        IEnumerable<CiudadServidorEntity> GetCiudadServidorList();
        [OperationContract]
        SoftvList<CiudadServidorEntity> GetCiudadServidorPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CiudadServidorEntity> GetCiudadServidorPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCiudadServidor(CiudadServidorEntity objCiudadServidor);
        [OperationContract]
        int UpdateCiudadServidor(CiudadServidorEntity objCiudadServidor);
        [OperationContract]
        int DeleteCiudadServidor(String BaseRemoteIp, int BaseIdUser,int? Id);
        
      }
    }

  