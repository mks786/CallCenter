using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class TimeUtility
    {
        public static string ConvertirTime(string hora)
        {
            string horaconvertida = "";
            if (hora.Contains("M"))
            {
                horaconvertida = hora.Replace("PT", "").Replace("H", ":").Replace("M", "");
            }
            else
            {
                horaconvertida = hora.Replace("PT", "").Replace("H", ":00");
            }
            return horaconvertida;
        }
        public static bool OverLapTime(DateTime tStartA, DateTime tEndA, DateTime tStartB, DateTime tEndB)
        {
            return (tStartA < tEndB && tStartB < tEndA);
        }
    }
}
