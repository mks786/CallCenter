using SoftvMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Softv.Entities;
using System.Data.SqlClient;
using Globals;
using System.Globalization;

namespace SoftvMVC.Controllers
{
    public partial class LlamadasReportesController : BaseController, IDisposable
    {
        // GET: LlamadasReporte
        public ActionResult Index()
        {
            return View();
        }

        
    }
}