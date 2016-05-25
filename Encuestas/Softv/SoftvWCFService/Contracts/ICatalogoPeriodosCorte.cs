
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
    public interface ICatalogoPeriodosCorte
    {
        [OperationContract]
        CatalogoPeriodosCorteEntity GetCatalogoPeriodosCorte(int? Clv_Periodo);
        [OperationContract]
        CatalogoPeriodosCorteEntity GetDeepCatalogoPeriodosCorte(int? Clv_Periodo);
        [OperationContract]
        IEnumerable<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCorteList();
        [OperationContract]
        SoftvList<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCortePagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CatalogoPeriodosCorteEntity> GetCatalogoPeriodosCortePagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity objCatalogoPeriodosCorte);
        [OperationContract]
        int UpdateCatalogoPeriodosCorte(CatalogoPeriodosCorteEntity objCatalogoPeriodosCorte);
        [OperationContract]
        int DeleteCatalogoPeriodosCorte(String BaseRemoteIp, int BaseIdUser, int? Clv_Periodo);

    }
}

