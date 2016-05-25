
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
    public interface IEncuesta
    {
        [OperationContract]
        EncuestaEntity GetEncuesta(int? IdEncuesta);

        [OperationContract]
        EncuestaEntity GetDeepEncuesta(int? IdEncuesta);

        [OperationContract]
        IEnumerable<EncuestaEntity> GetEncuestaList();

        [OperationContract]
        SoftvList<EncuestaEntity> GetEncuestaPagedList(int page, int pageSize);

        [OperationContract]
        SoftvList<EncuestaEntity> GetEncuestaPagedListXml(int page, int pageSize, String xml);

        [OperationContract]

        int AddEncuesta(string data);

        [OperationContract]
        int UpdateEncuesta(string data);

        [OperationContract]
        int DeleteEncuesta(int? IdEncuesta);




        [OperationContract]
        int? AddEncuestaRel(EncuestaEntity lstEncuesta, List<PreguntaEntity> PreguntaAdd);




    }
}

