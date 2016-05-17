using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Globals;
using Softv.BAL;
using Softv.Entities;
using SoftvWCFService.Contracts;

namespace SoftvWCFService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "SoftvWCFService" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione SoftvWCFService.svc o SoftvWCFService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class SoftvWCFService : IBanco
    {

        #region Banco
        public BancoEntity GetBanco(int? IdBanco)
        {
            return Banco.GetOne(IdBanco);
        }

        public BancoEntity GetDeepBanco(int? IdBanco)
        {
            return Banco.GetOneDeep(IdBanco);
        }

        public IEnumerable<BancoEntity> GetBancoList()
        {
            return Banco.GetAll();
        }

        public SoftvList<BancoEntity> GetBancoPagedList(int page, int pageSize)
        {
            return Banco.GetPagedList(page, pageSize);
        }

        public SoftvList<BancoEntity> GetBancoPagedListXml(int page, int pageSize, String xml)
        {
            return Banco.GetPagedList(page, pageSize, xml);
        }

        public int AddBanco(BancoEntity objBanco)
        {
            return Banco.Add(objBanco);
        }

        public int UpdateBanco(BancoEntity objBanco)
        {
            return Banco.Edit(objBanco);
        }

        public int DeleteBanco(int? IdBanco)
        {
            return Banco.Delete(IdBanco);
        }

        #endregion
  
    }
}
