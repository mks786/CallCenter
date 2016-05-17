
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
    public interface IPermiso
    {
        [OperationContract]
        SoftvList<PermisoEntity> GetXmlPermiso(String xml);
        [OperationContract]
        int MargePermiso(int BaseIdUser, String BaseRemoteIp, String xml);
    }
}

