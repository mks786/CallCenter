
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
    public interface ICliente_Apellido
    {
        [OperationContract]
        Cliente_ApellidoEntity GetCliente_Apellido(long? Contrato);
        [OperationContract]
        Cliente_ApellidoEntity GetDeepCliente_Apellido(long? Contrato);
        [OperationContract]
        IEnumerable<Cliente_ApellidoEntity> GetCliente_ApellidoList();
        [OperationContract]
        SoftvList<Cliente_ApellidoEntity> GetCliente_ApellidoPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<Cliente_ApellidoEntity> GetCliente_ApellidoPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCliente_Apellido(Cliente_ApellidoEntity objCliente_Apellido);
        [OperationContract]
        int UpdateCliente_Apellido(Cliente_ApellidoEntity objCliente_Apellido);
        [OperationContract]
        int DeleteCliente_Apellido(String BaseRemoteIp, int BaseIdUser, long? Contrato);

    }
}

