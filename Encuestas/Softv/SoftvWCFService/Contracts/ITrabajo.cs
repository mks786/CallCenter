
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
    public interface ITrabajo
    {
        [OperationContract]
        TrabajoEntity GetTrabajo(int? Clv_Trabajo);
        [OperationContract]
        TrabajoEntity GetDeepTrabajo(int? Clv_Trabajo);
        [OperationContract]
        IEnumerable<TrabajoEntity> GetTrabajoList();
        [OperationContract]
        SoftvList<TrabajoEntity> GetTrabajoPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TrabajoEntity> GetTrabajoPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTrabajo(TrabajoEntity objTrabajo);
        [OperationContract]
        int UpdateTrabajo(TrabajoEntity objTrabajo);
        [OperationContract]
        int DeleteTrabajo(String BaseRemoteIp, int BaseIdUser, int? Clv_Trabajo);

    }
}

