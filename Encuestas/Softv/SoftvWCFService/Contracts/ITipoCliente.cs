
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
    public interface ITipoCliente
    {
        [OperationContract]
        TipoClienteEntity GetTipoCliente(int? Clv_TipoCliente);
        [OperationContract]
        TipoClienteEntity GetDeepTipoCliente(int? Clv_TipoCliente);
        [OperationContract]
        IEnumerable<TipoClienteEntity> GetTipoClienteList();
        [OperationContract]
        SoftvList<TipoClienteEntity> GetTipoClientePagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TipoClienteEntity> GetTipoClientePagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTipoCliente(TipoClienteEntity objTipoCliente);
        [OperationContract]
        int UpdateTipoCliente(TipoClienteEntity objTipoCliente);
        [OperationContract]
        int DeleteTipoCliente(String BaseRemoteIp, int BaseIdUser, int? Clv_TipoCliente);

    }
}

