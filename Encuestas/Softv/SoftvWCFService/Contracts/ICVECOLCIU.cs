
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
    public interface ICVECOLCIU
    {
        [OperationContract]
        CVECOLCIUEntity GetCVECOLCIU(int? Clv_Ciudad);
        [OperationContract]
        CVECOLCIUEntity GetDeepCVECOLCIU(int? Clv_Colonia, int? Clv_Ciudad);
        [OperationContract]
        IEnumerable<CVECOLCIUEntity> GetCVECOLCIUList();
        [OperationContract]
        SoftvList<CVECOLCIUEntity> GetCVECOLCIUPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CVECOLCIUEntity> GetCVECOLCIUPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCVECOLCIU(CVECOLCIUEntity objCVECOLCIU);
        [OperationContract]
        int UpdateCVECOLCIU(CVECOLCIUEntity objCVECOLCIU);
        [OperationContract]
        int DeleteCVECOLCIU(int? Clv_Ciudad);

    }
}

