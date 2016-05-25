
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
    public interface ITipServ
    {
        [OperationContract]
        TipServEntity GetTipServ(int? Clv_TipSer);
        [OperationContract]
        TipServEntity GetDeepTipServ(int? Clv_TipSer);
        [OperationContract]
        IEnumerable<TipServEntity> GetTipServList();
        [OperationContract]
        SoftvList<TipServEntity> GetTipServPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<TipServEntity> GetTipServPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddTipServ(TipServEntity objTipServ);
        [OperationContract]
        int UpdateTipServ(TipServEntity objTipServ);
        [OperationContract]
        int DeleteTipServ(String BaseRemoteIp, int BaseIdUser, int? Clv_TipSer);

    }
}

