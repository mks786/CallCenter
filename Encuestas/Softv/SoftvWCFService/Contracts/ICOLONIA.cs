
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
    public interface ICOLONIA
    {
        [OperationContract]
        COLONIAEntity GetCOLONIA(int? Clv_Colonia);
        [OperationContract]
        COLONIAEntity GetDeepCOLONIA(int? Clv_Colonia);
        [OperationContract]
        IEnumerable<COLONIAEntity> GetCOLONIAList();
        [OperationContract]
        SoftvList<COLONIAEntity> GetCOLONIAPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<COLONIAEntity> GetCOLONIAPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddCOLONIA(COLONIAEntity objCOLONIA);
        [OperationContract]
        int UpdateCOLONIA(COLONIAEntity objCOLONIA);
        [OperationContract]
        int DeleteCOLONIA(String BaseRemoteIp, int BaseIdUser, int? Clv_Colonia);

    }
}

