
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
    public interface IUniversoEncuesta
    {
        [OperationContract]
        UniversoEncuestaEntity GetUniversoEncuesta(int? Id);
        [OperationContract]
        UniversoEncuestaEntity GetDeepUniversoEncuesta(int? Id);
        [OperationContract]
        IEnumerable<UniversoEncuestaEntity> GetUniversoEncuestaList();
        [OperationContract]
        SoftvList<UniversoEncuestaEntity> GetUniversoEncuestaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<UniversoEncuestaEntity> GetUniversoEncuestaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddUniversoEncuesta(UniversoEncuestaEntity objUniversoEncuesta);
        [OperationContract]
        int UpdateUniversoEncuesta(UniversoEncuestaEntity objUniversoEncuesta);
        [OperationContract]
        int DeleteUniversoEncuesta(String BaseRemoteIp, int BaseIdUser, int? Id);

        [OperationContract]
        int ActualizarUniverso (int? Id);

    }
}

