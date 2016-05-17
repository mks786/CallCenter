
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
      public interface IRelEncuestaClientes
      {
        [OperationContract]
        RelEncuestaClientesEntity GetRelEncuestaClientes(int? IdProceso);
        [OperationContract]
        RelEncuestaClientesEntity GetDeepRelEncuestaClientes(int? IdProceso);
        [OperationContract]
        IEnumerable<RelEncuestaClientesEntity> GetRelEncuestaClientesList();
        [OperationContract]
        SoftvList<RelEncuestaClientesEntity> GetRelEncuestaClientesPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RelEncuestaClientesEntity> GetRelEncuestaClientesPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRelEncuestaClientes(RelEncuestaClientesEntity objRelEncuestaClientes);
        [OperationContract]
        int UpdateRelEncuestaClientes(RelEncuestaClientesEntity objRelEncuestaClientes);
        [OperationContract]
        int DeleteRelEncuestaClientes(int? IdProceso);
        
      }
    }

  