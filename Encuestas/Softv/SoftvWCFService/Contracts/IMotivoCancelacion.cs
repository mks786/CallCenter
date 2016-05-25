
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
    public interface IMotivoCancelacion
    {
        [OperationContract]
        MotivoCancelacionEntity GetMotivoCancelacion(int? Clv_MOTCAN);
        [OperationContract]
        MotivoCancelacionEntity GetDeepMotivoCancelacion(int? Clv_MOTCAN);
        [OperationContract]
        IEnumerable<MotivoCancelacionEntity> GetMotivoCancelacionList();
        [OperationContract]
        SoftvList<MotivoCancelacionEntity> GetMotivoCancelacionPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<MotivoCancelacionEntity> GetMotivoCancelacionPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddMotivoCancelacion(MotivoCancelacionEntity objMotivoCancelacion);
        [OperationContract]
        int UpdateMotivoCancelacion(MotivoCancelacionEntity objMotivoCancelacion);
        [OperationContract]
        int DeleteMotivoCancelacion(String BaseRemoteIp, int BaseIdUser, int? Clv_MOTCAN);

    }
}

