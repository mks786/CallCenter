
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
    public interface IQueja
    {
        [OperationContract]
        QuejaEntity GetQueja(long? Clv_Queja);
        [OperationContract]
        QuejaEntity GetDeepQueja(long? Clv_Queja);
        [OperationContract]
        IEnumerable<QuejaEntity> GetQuejaList();
        [OperationContract]
        SoftvList<QuejaEntity> GetQuejaPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<QuejaEntity> GetQuejaPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddQueja(QuejaEntity objQueja);
        [OperationContract]
        int UpdateQueja(QuejaEntity objQueja);
        [OperationContract]
        int DeleteQueja(String BaseRemoteIp, int BaseIdUser, long? Clv_Queja);

    }
}

