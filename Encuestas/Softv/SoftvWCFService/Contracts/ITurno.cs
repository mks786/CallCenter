
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
      public interface ITurno
      {
        [OperationContract]
        TurnoEntity GetTurno(int? IdTurno);
        [OperationContract]
        TurnoEntity GetDeepTurno(int? IdTurno);
        [OperationContract]
        IEnumerable<TurnoEntity> GetTurnoList();
        [OperationContract]
        SoftvList<TurnoEntity> GetTurnoPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TurnoEntity> GetTurnoPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTurno(TurnoEntity objTurno);
        [OperationContract]
        int UpdateTurno(TurnoEntity objTurno);
        [OperationContract]
        int DeleteTurno(String BaseRemoteIp, int BaseIdUser,int? IdTurno);
        
      }
    }

  