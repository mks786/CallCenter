
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
    public interface IRelPreguntaOpcMults
    {
        [OperationContract]
        RelPreguntaOpcMultsEntity GetRelPreguntaOpcMults(int? IdPregunta);
        [OperationContract]
        RelPreguntaOpcMultsEntity GetDeepRelPreguntaOpcMults(int? IdPregunta);
        [OperationContract]
        IEnumerable<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsList();
        [OperationContract]
        SoftvList<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RelPreguntaOpcMultsEntity> GetRelPreguntaOpcMultsPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRelPreguntaOpcMults(RelPreguntaOpcMultsEntity objRelPreguntaOpcMults);
        [OperationContract]
        int UpdateRelPreguntaOpcMults(RelPreguntaOpcMultsEntity objRelPreguntaOpcMults);
        [OperationContract]
        int DeleteRelPreguntaOpcMults(String BaseRemoteIp, int BaseIdUser, int? IdPregunta);

    }
}

