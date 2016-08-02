
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
    public interface IServicio
    {
        [OperationContract]
        ServicioEntity GetServicio(int? Clv_Servicio);
        [OperationContract]
        ServicioEntity GetDeepServicio(int? Clv_Servicio);
        [OperationContract]
        IEnumerable<ServicioEntity> GetServicioList();
        [OperationContract]
        SoftvList<ServicioEntity> GetServicioPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ServicioEntity> GetServicioPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddServicio(ServicioEntity objServicio);
        [OperationContract]
        int UpdateServicio(ServicioEntity objServicio);
        [OperationContract]
        int DeleteServicio(String BaseRemoteIp, int BaseIdUser, int? Clv_Servicio);

    }
}

