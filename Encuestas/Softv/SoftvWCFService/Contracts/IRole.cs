
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
    public interface IRole
    {
        [OperationContract]
        RoleEntity GetRole(int? IdRol);
        [OperationContract]
        RoleEntity GetDeepRole(int? IdRol);
        [OperationContract]
        IEnumerable<RoleEntity> GetRoleList();
        [OperationContract]
        SoftvList<RoleEntity> GetRolePagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<RoleEntity> GetRolePagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddRole(RoleEntity objRole);
        [OperationContract]
        int UpdateRole(RoleEntity objRole);
        [OperationContract]
        int DeleteRole(int? IdRol);

        [OperationContract]
        int ChangeStateRole(RoleEntity objRole, bool State);

    }
}

