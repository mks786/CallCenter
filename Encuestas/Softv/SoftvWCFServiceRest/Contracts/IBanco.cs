
using Globals;
using Softv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

namespace SoftvWCFService.Contracts
{
    [ServiceContract]
    public interface IBanco
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBanco", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        BancoEntity GetBanco(int? IdBanco);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetDeepBanco", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        BancoEntity GetDeepBanco(int? IdBanco);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBancoList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        IEnumerable<BancoEntity> GetBancoList();
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBancoPagedList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<BancoEntity> GetBancoPagedList(int page, int pageSize);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetBancoPagedListXml", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        SoftvList<BancoEntity> GetBancoPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddBanco", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int AddBanco(BancoEntity objBanco);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateBanco", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int UpdateBanco(BancoEntity objBanco);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeleteBanco", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int DeleteBanco(int? IdBanco);

    }
}

