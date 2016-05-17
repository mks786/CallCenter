
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
    public interface IRelEnProcesos
    {
        [OperationContract]
        RelEnProcesosEntity GetRelEnProcesos(int? IdProceso);
        [OperationContract]
        RelEnProcesosEntity GetDeepRelEnProcesos(int? IdProceso);
        [OperationContract]
        IEnumerable<RelEnProcesosEntity> GetRelEnProcesosList();
        [OperationContract]
        SoftvList<RelEnProcesosEntity> GetRelEnProcesosPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RelEnProcesosEntity> GetRelEnProcesosPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRelEnProcesos(RelEnProcesosEntity objRelEnProcesos);
        [OperationContract]
        int UpdateRelEnProcesos(RelEnProcesosEntity objRelEnProcesos);
        [OperationContract]
        int DeleteRelEnProcesos(String BaseRemoteIp, int BaseIdUser, int? IdProceso);

    }
}

