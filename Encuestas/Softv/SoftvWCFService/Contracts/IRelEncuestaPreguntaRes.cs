
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
    public interface IRelEncuestaPreguntaRes
    {
        [OperationContract]
        RelEncuestaPreguntaResEntity GetRelEncuestaPreguntaRes(int? Id);
        [OperationContract]
        RelEncuestaPreguntaResEntity GetDeepRelEncuestaPreguntaRes(int? Id);
        [OperationContract]
        IEnumerable<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResList();
        [OperationContract]
        SoftvList<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RelEncuestaPreguntaResEntity> GetRelEncuestaPreguntaResPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRelEncuestaPreguntaRes(RelEncuestaPreguntaResEntity objRelEncuestaPreguntaRes);
        [OperationContract]
        int UpdateRelEncuestaPreguntaRes(RelEncuestaPreguntaResEntity objRelEncuestaPreguntaRes);
        [OperationContract]
        int DeleteRelEncuestaPreguntaRes(String BaseRemoteIp, int BaseIdUser, int? Id);

    }
}

