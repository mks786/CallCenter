
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
      public interface IBusquedaIndividual
      {
        [OperationContract]
        BusquedaIndividualEntity GetBusquedaIndividual(int? Id);
        [OperationContract]
        BusquedaIndividualEntity GetDeepBusquedaIndividual(int? Id);
        [OperationContract]
        IEnumerable<BusquedaIndividualEntity> GetBusquedaIndividualList();
        [OperationContract]
        SoftvList<BusquedaIndividualEntity> GetBusquedaIndividualPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<BusquedaIndividualEntity> GetBusquedaIndividualPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddBusquedaIndividual(BusquedaIndividualEntity objBusquedaIndividual);
        [OperationContract]
        int UpdateBusquedaIndividual(BusquedaIndividualEntity objBusquedaIndividual);
        [OperationContract]
        int DeleteBusquedaIndividual(String BaseRemoteIp, int BaseIdUser,int? Id);
        
      }
    }

  