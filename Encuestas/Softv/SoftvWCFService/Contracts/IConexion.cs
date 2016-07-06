
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
    public interface IConexion
    {
        [OperationContract]
        ConexionEntity GetConexion(int? IdConexion);
        [OperationContract]
        ConexionEntity GetDeepConexion(int? IdConexion);
        [OperationContract]
        IEnumerable<ConexionEntity> GetConexionList();
        [OperationContract]
        SoftvList<ConexionEntity> GetConexionPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ConexionEntity> GetConexionPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddConexion(ConexionEntity objConexion);
        [OperationContract]
        int UpdateConexion(ConexionEntity objConexion);
        [OperationContract]
        int DeleteConexion(int? IdConexion);

    }
}

