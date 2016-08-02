
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
      public interface IMotAtenTel
      {
        [OperationContract]
        MotAtenTelEntity GetMotAtenTel(int? Clv_Motivo);
        [OperationContract]
        MotAtenTelEntity GetDeepMotAtenTel(int? Clv_Motivo);
        [OperationContract]
        IEnumerable<MotAtenTelEntity> GetMotAtenTelList();
        [OperationContract]
        SoftvList<MotAtenTelEntity> GetMotAtenTelPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<MotAtenTelEntity> GetMotAtenTelPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddMotAtenTel(MotAtenTelEntity objMotAtenTel);
        [OperationContract]
        int UpdateMotAtenTel(MotAtenTelEntity objMotAtenTel);
        [OperationContract]
        int DeleteMotAtenTel(String BaseRemoteIp, int BaseIdUser,int? Clv_Motivo);
        
      }
    }

  