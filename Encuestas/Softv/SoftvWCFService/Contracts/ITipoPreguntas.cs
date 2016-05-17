
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
    public interface ITipoPreguntas
    {
        [OperationContract]
        TipoPreguntasEntity GetTipoPreguntas(int? IdTipoPregunta);
        [OperationContract]
        TipoPreguntasEntity GetDeepTipoPreguntas(int? IdTipoPregunta);
        [OperationContract]
        IEnumerable<TipoPreguntasEntity> GetTipoPreguntasList();
        [OperationContract]
        SoftvList<TipoPreguntasEntity> GetTipoPreguntasPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TipoPreguntasEntity> GetTipoPreguntasPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTipoPreguntas(TipoPreguntasEntity objTipoPreguntas);
        [OperationContract]
        int UpdateTipoPreguntas(TipoPreguntasEntity objTipoPreguntas);
        [OperationContract]
        int DeleteTipoPreguntas(int? IdTipoPregunta);

    }
}

