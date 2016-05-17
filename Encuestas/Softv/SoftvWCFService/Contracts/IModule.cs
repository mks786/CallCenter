
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
    public interface IModule
    {
        [OperationContract]
        ModuleEntity GetModule(int? IdModule);
        [OperationContract]
        ModuleEntity GetDeepModule(int? IdModule);
        [OperationContract]
        IEnumerable<ModuleEntity> GetModuleList();
        [OperationContract]
        SoftvList<ModuleEntity> GetModulePagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<ModuleEntity> GetModulePagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddModule(ModuleEntity objModule);
        [OperationContract]
        int UpdateModule(ModuleEntity objModule);
        [OperationContract]
        int DeleteModule(int? IdModule);

    }
}

