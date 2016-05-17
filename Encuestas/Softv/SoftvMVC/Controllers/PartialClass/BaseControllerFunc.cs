using Softv.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftvMVC.Controllers
{
    public partial class BaseController
    {
        public bool validateString(String Str)
        {
            if ((Str != null && Str.ToString().Trim() != ""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SelectList GetEnumList<T>()
        {
            List<EnumEntity> lstEnum = new List<EnumEntity>();
            lstEnum.AddRange((from T tc in Enum.GetValues(typeof(T))
                              select new EnumEntity() { Id = (int)(object)tc, Nombre = tc.ToString() }).ToList());

            return new SelectList(lstEnum, "Id", "Nombre");
        }
    }
}