
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
      public interface IRelPreguntaEncuestas
      {
        [OperationContract]
        RelPreguntaEncuestasEntity GetRelPreguntaEncuestas(int? IdPregunta);
        [OperationContract]
        RelPreguntaEncuestasEntity GetDeepRelPreguntaEncuestas(int? IdPregunta);
        [OperationContract]
        IEnumerable<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasList();
        [OperationContract]
        SoftvList<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RelPreguntaEncuestasEntity> GetRelPreguntaEncuestasPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRelPreguntaEncuestas(RelPreguntaEncuestasEntity objRelPreguntaEncuestas);
        [OperationContract]
        int UpdateRelPreguntaEncuestas(RelPreguntaEncuestasEntity objRelPreguntaEncuestas);
        [OperationContract]
        int DeleteRelPreguntaEncuestas(String BaseRemoteIp, int BaseIdUser, int? IdPregunta);
        
      }
    }

  