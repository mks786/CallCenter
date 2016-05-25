
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
    public interface ICVECAROL
    {
        [OperationContract]
        CVECAROLEntity GetCVECAROL(int? Clv_Colonia);
        [OperationContract]
        CVECAROLEntity GetDeepCVECAROL(int? Clv_Calle, int? Clv_Colonia);
        [OperationContract]
        IEnumerable<CVECAROLEntity> GetCVECAROLList();
        [OperationContract]
        SoftvList<CVECAROLEntity> GetCVECAROLPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CVECAROLEntity> GetCVECAROLPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCVECAROL(CVECAROLEntity objCVECAROL);
        [OperationContract]
        int UpdateCVECAROL(CVECAROLEntity objCVECAROL);
        [OperationContract]
        int DeleteCVECAROL(int? Clv_Colonia);

    }
}

