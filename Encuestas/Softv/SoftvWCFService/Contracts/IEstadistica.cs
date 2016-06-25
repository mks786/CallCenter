
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
    public interface IEstadistica
    {

        [OperationContract]
        IEnumerable<EstadisticaEntity> GetEstadisticaList(int plaza, int idencuesta, DateTime finicio, DateTime ffin);

    }
}

