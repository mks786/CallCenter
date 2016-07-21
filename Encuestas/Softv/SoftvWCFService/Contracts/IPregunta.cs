
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
    public interface IPregunta
    {
        [OperationContract]
        PreguntaEntity GetPregunta(int? IdPregunta);
        [OperationContract]
        PreguntaEntity GetDeepPregunta(int? IdPregunta);
        [OperationContract]
        IEnumerable<PreguntaEntity> GetPreguntaList();
        [OperationContract]
        SoftvList<PreguntaEntity> GetPreguntaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<PreguntaEntity> GetPreguntaPagedListXml(int page, int pageSize, String xml);
        //[OperationContract]
        //int AddPregunta(PreguntaEntity objPregunta);
        [OperationContract]
        int UpdatePregunta(string xml);
        [OperationContract]
        int DeletePregunta(int? IdPregunta);

        [OperationContract]
        int AddPregunta(string data);

    }
}

