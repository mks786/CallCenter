
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
      public interface ITap
      {
        [OperationContract]
        TapEntity GetTap(int? IdTap);
        [OperationContract]
        TapEntity GetDeepTap(int? IdTap);
        [OperationContract]
        IEnumerable<TapEntity> GetTapList();
        [OperationContract]
        SoftvList<TapEntity> GetTapPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TapEntity> GetTapPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTap(TapEntity objTap);
        [OperationContract]
        int UpdateTap(TapEntity objTap);
        [OperationContract]
        int DeleteTap(String BaseRemoteIp, int BaseIdUser,int? IdTap);
        
      }
    }

  