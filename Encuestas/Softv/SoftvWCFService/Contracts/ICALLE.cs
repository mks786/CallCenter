
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
    public interface ICALLE
    {
        [OperationContract]
        CALLEEntity GetCALLE(int? Clv_Calle);
        [OperationContract]
        CALLEEntity GetDeepCALLE(int? Clv_Calle);
        [OperationContract]
        IEnumerable<CALLEEntity> GetCALLEList();
        [OperationContract]
        SoftvList<CALLEEntity> GetCALLEPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CALLEEntity> GetCALLEPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCALLE(CALLEEntity objCALLE);
        [OperationContract]
        int UpdateCALLE(CALLEEntity objCALLE);
        [OperationContract]
        int DeleteCALLE(String BaseRemoteIp, int BaseIdUser, int? Clv_Calle);

    }
}

