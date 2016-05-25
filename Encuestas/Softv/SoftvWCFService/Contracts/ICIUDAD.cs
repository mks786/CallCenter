
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
    public interface ICIUDAD
    {
        [OperationContract]
        CIUDADEntity GetCIUDAD(int? Clv_Ciudad);
        [OperationContract]
        CIUDADEntity GetDeepCIUDAD(int? Clv_Ciudad);
        [OperationContract]
        IEnumerable<CIUDADEntity> GetCIUDADList();
        [OperationContract]
        SoftvList<CIUDADEntity> GetCIUDADPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<CIUDADEntity> GetCIUDADPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCIUDAD(CIUDADEntity objCIUDAD);
        [OperationContract]
        int UpdateCIUDAD(CIUDADEntity objCIUDAD);
        [OperationContract]
        int DeleteCIUDAD(String BaseRemoteIp, int BaseIdUser, int? Clv_Ciudad);

    }
}

