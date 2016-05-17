
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
    public interface IUsuario
    {
        [OperationContract]
        UsuarioEntity GetUsuario(int? IdUsuario);
        [OperationContract]
        UsuarioEntity GetDeepUsuario(int? IdUsuario);
        [OperationContract]
        IEnumerable<UsuarioEntity> GetUsuarioList();
        [OperationContract]
        SoftvList<UsuarioEntity> GetUsuarioPagedList(int page, int pageSize);
        [OperationContract]
        SoftvList<UsuarioEntity> GetUsuarioPagedListXml(int page, int pageSize, String xml);
        [OperationContract]
        int AddUsuario(UsuarioEntity objUsuario);
        [OperationContract]
        int UpdateUsuario(UsuarioEntity objUsuario);
        [OperationContract]
        int DeleteUsuario(int? IdUsuario);

        [OperationContract]
        int ChangeStateUsuario(UsuarioEntity objUsuario, bool State);

        [OperationContract]
        UsuarioEntity GetusuarioByUserAndPass(string Usuario, string Pass);

    }
}

