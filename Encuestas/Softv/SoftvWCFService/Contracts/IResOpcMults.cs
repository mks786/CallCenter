
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
    public interface IResOpcMults
    {
        [OperationContract]
        ResOpcMultsEntity GetResOpcMults(int? Id_ResOpcMult);
        [OperationContract]
        ResOpcMultsEntity GetDeepResOpcMults(int? Id_ResOpcMult);
        [OperationContract]
        IEnumerable<ResOpcMultsEntity> GetResOpcMultsList();
        [OperationContract]
        SoftvList<ResOpcMultsEntity> GetResOpcMultsPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ResOpcMultsEntity> GetResOpcMultsPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddResOpcMults(ResOpcMultsEntity objResOpcMults);
        [OperationContract]
        int UpdateResOpcMults(ResOpcMultsEntity objResOpcMults);
        [OperationContract]
        int DeleteResOpcMults(int? Id_ResOpcMult);

    }
}

