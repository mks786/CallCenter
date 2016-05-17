using Softv.Entities;
using SoftvMVC.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;


namespace SoftvMVC.Controllers
{
    public partial class BaseController : Controller
    {
        private SoftvService.ModuleClient proxy = null;
        private SoftvService.PermisoClient proxyPermiso = null;
        private SoftvService.UsuarioClient proxyUsuario = null;
        private SoftvService.RoleClient proxyRol = null;

        public String RemoteIp { get; set; }
        public int LoggedUserName { get; set; }
        public BaseController()
        {
            RemoteIp = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(RemoteIp))
            {
                RemoteIp = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                RemoteIp = "Undefined";
            }
            if (System.Web.HttpContext.Current.Session["username"] != null)
            {
                LoggedUserName = int.Parse(System.Web.HttpContext.Current.Session["idusuario"].ToString());
            }
            else
            {
                LoggedUserName = 0;
            }
            proxy = new SoftvService.ModuleClient();
            proxyPermiso = new SoftvService.PermisoClient();
            proxyUsuario = new SoftvService.UsuarioClient();
            proxyRol = new SoftvService.RoleClient();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            const string culture = "es-ES";
            CultureInfo ci = (CultureInfo)CultureInfo.GetCultureInfo(culture).Clone();
            ci.NumberFormat.NumberDecimalSeparator = ".";
            ci.NumberFormat.CurrencyGroupSeparator = ",";
            ci.NumberFormat.CurrencySymbol = "$";
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        new public void Dispose()
        {
            if (proxy != null)
            {
                if (proxy.State != System.ServiceModel.CommunicationState.Closed)
                {
                    proxy.Close();
                }
            }
        }
        protected override void ExecuteCore()
        {

            ValidateCookiesUser();
            Session["POptAdd"] = true;
            Session["POptUpdate"] = true;
            Session["POptDelete"] = true;
            List<ModuleEntity> lstModule = null;
            if (HttpContext != null && HttpContext.User.Identity.IsAuthenticated)
            {


            }
            if (this.Session == null || this.Session["Access"] != null)
            {
                UsuarioEntity um = new UsuarioEntity();
                List<ModuleEntity> lstModuleFilter = new List<ModuleEntity>();
                um = (UsuarioEntity)Session["Usuario"];
                if (um != null)
                {
                    if (proxyUsuario.GetUsuario(um.IdUsuario).Estado == true && proxyRol.GetRole(um.IdRol).Estado == true)
                    {
                        List<PermisoEntity> lstPermiso =
                                            proxyPermiso.GetXmlPermiso(Globals.SerializeTool.Serialize<PermisoEntity>(new PermisoEntity() { IdRol = um.IdRol })).ToList();


                        lstModuleFilter = (from c in BuildMenu()
                                           where (from o in lstPermiso
                                                  select o.IdModule).Contains(c.IdModule)
                                           select c).ToList();

                        lstPermiso.ForEach(XPermiso => lstModuleFilter.Where(x => x.IdModule.HasValue).Where(x => x.IdModule == XPermiso.IdModule).ToList().ForEach(y => y.Permiso = XPermiso));


                        lstModule = (from c in lstModuleFilter
                                     where c.Permiso.OptSelect == true
                                     select c).ToList();

                        //lstPermiso.ForEach(Xpermiso => lstMoldeFilter.Where(x => x.IdModule.HasValue).Where(x => x.IdModule == Xpermiso.IdModule).ToList().ForEach(y => y.Permiso = Xpermiso));
                        lstPermiso.ForEach(Xpermiso => lstPermiso.Where(z => z.OptAdd == true || z.OptDelete == true || z.OptSelect == true || z.OptUpdate == true).ToList());

                    }
                    else
                    {
                        Session.RemoveAll();
                    }

                }


                proxy = new SoftvService.ModuleClient();
                List<ModuleEntity> lm = lstModule;
                //ViewBag.Permisos = lstModuleFilter;
                ViewBag.Menu2 = lm;
                TipoCambioPass();

            }
            if (this.Session == null || this.Session["Access"] == null)
            {
                Session["Access"] = "NG";
                // RedirectToRoute("Home/Access");
            }
            int culture = 0;
            if (this.Session == null || this.Session["CurrentCulture"] == null)
            {
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings["Culture"], out culture);
                this.Session["CurrentCulture"] = culture;
            }

            else
            {
                culture = (int)this.Session["CurrentCulture"];

            }
            //
            //SessionManager.CurrentCulture = culture;
            //
            // Invokes the action in the current controller context.
            //

            base.ExecuteCore();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        private List<ModuleEntity> BuildMenu()
        {

            return proxy.GetModuleList();
        }

        public void CheckNotify()
        {
            bool notify = false;
            if (Session["notify"] != null)
            {
                notify = (bool)Session["notify"];
            }
            if (notify == true)
            {
                ViewBag.notify = true;

            }
            else
            {
                ViewBag.notify = false;
            }
        }

        public void TipoCambioPass()
        {
            List<ModuleEntity> lm = ViewBag.Menu2;
            if (lm != null)
            {
                lm = lm.Where(x => x.ModulePath.ToUpper() == "Usuario".ToUpper()).ToList();

                if (lm.Count > 0)
                {
                    //if (!lm.FirstOrDefault().Permiso.OptAdd)
                    //{
                    //    ViewData["POptAdd"] = false;
                    //    ViewBag.POptAdd = false;
                    //}

                    if (lm.FirstOrDefault().Permiso.OptUpdate)
                    {
                        ViewData["cambioadmin"] = true;

                    }
                    else
                    {
                        ViewData["cambioadmin"] = false;
                    }

                    //if (!lm.FirstOrDefault().Permiso.OptDelete)
                    //{
                    //    ViewData["POptDelete"] = false;
                    //    ViewBag.POptDelete = false;
                    //}
                }
                else
                {
                    ViewData["cambioadmin"] = false;
                }


            }
        }

        public void PermisosAcceso(string nombremodulo)
        {
            List<ModuleEntity> lm = ViewBag.Menu2;
            if (lm != null)
            {
                lm = lm.Where(x => x.ModulePath.ToUpper() == nombremodulo.ToUpper()).ToList();

                if (lm.Count > 0)
                {
                    if (!lm.FirstOrDefault().Permiso.OptAdd)
                    {
                        ViewData["POptAdd"] = false;
                        ViewBag.POptAdd = false;

                    }

                    if (!lm.FirstOrDefault().Permiso.OptUpdate)
                    {
                        ViewData["POptUpdate"] = false;
                        ViewBag.POptUpdate = false;
                    }

                    if (!lm.FirstOrDefault().Permiso.OptDelete)
                    {
                        ViewData["POptDelete"] = false;
                        ViewBag.POptDelete = false;
                    }
                }
                else
                {
                    ViewData["NoAut"] = true;
                }

            }
        }

        public void PermisosAccesoEmbarque(string nombremodulo, string NombrePagina)
        {
            List<ModuleEntity> lm = ViewBag.Menu2;
            if (lm != null)
            {
                lm = lm.Where(x => x.ModulePath.ToUpper() == nombremodulo.ToUpper() && x.ModuleView.ToUpper() == NombrePagina.ToUpper()).ToList();

                if (lm.Count > 0)
                {
                    if (!lm.FirstOrDefault().Permiso.OptAdd)
                    {
                        ViewData["POptAdd"] = false;
                        ViewBag.POptAdd = false;

                    }

                    if (!lm.FirstOrDefault().Permiso.OptUpdate)
                    {
                        ViewData["POptUpdate"] = false;
                        ViewBag.POptUpdate = false;
                    }

                    if (!lm.FirstOrDefault().Permiso.OptDelete)
                    {
                        ViewData["POptDelete"] = false;
                        ViewBag.POptDelete = false;
                    }
                }
                else
                {
                    ViewData["NoAut"] = true;
                }

            }
        }


        public void PermisosAccesoDeniedCreate(string nombremodulo)
        {
            List<ModuleEntity> lm = ViewBag.Menu2;
            if (lm != null)
            {
                lm = lm.Where(x => x.ModulePath.ToUpper() == nombremodulo.ToUpper()).ToList();

                if (lm.Count > 0)
                {
                    if (!lm.FirstOrDefault().Permiso.OptAdd)
                    {
                        ViewData["NoAut"] = true;

                    }
                    else
                    {
                        ViewData["NoAut"] = false;
                    }

                }
                else
                {
                    ViewData["NoAut"] = true;
                }

            }
        }



        public void PermisosAccesoDeniedEdit(string nombremodulo)
        {
            List<ModuleEntity> lm = ViewBag.Menu2;
            if (lm != null)
            {
                lm = lm.Where(x => x.ModulePath.ToUpper() == nombremodulo.ToUpper()).ToList();

                if (lm.Count > 0)
                {

                    if (!lm.FirstOrDefault().Permiso.OptUpdate)
                    {
                        ViewData["NoAut"] = true;

                    }
                    else
                    {
                        ViewData["NoAut"] = false;
                    }

                }
                else
                {
                    ViewData["NoAut"] = true;
                }

            }
        }
        public void AssingMessageScript(String message, string typemsj, string titlemsj, bool notify)
        {
            Session["message"] = message;
            Session["typemsj"] = typemsj;
            Session["titlemsj"] = titlemsj;
            Session["notify"] = notify;
        }
        public void ValidateCookiesUser()
        {
            var userInCookie = Request.Cookies["usuario"];
            var passInCookie = Request.Cookies["password"];

            if (userInCookie != null && passInCookie != null)
            {
                //Request.Cookies.Clear();
                logon(userInCookie.Value.ToString(), passInCookie.Value.ToString());
            }
        }
        public void RememberMyCookies(UsuarioEntity objUsuario)
        {
            var Username = new HttpCookie("usuario");
            var Password = new HttpCookie("password");
            Password.Value = objUsuario.Password;
            Username.Value = objUsuario.Usuario;
            Password.Expires = DateTime.Now.AddHours(1);
            Username.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Add(Username);
            Response.Cookies.Add(Password);
        }

        public ActionResult logon(string user, string pass)
        {
            List<UsuarioEntity> list = null;
            List<UsuarioEntity> listUsuario = new List<UsuarioEntity>();
            proxyUsuario.GetUsuarioList().ToList().ForEach(x =>
            listUsuario.Add(x));
            list = listUsuario.Where(x => x.Usuario.ToUpper().Equals(user.ToUpper()) && x.Password.ToUpper().Equals(pass.ToUpper()) && x.Estado == true).ToList();
            if (list.Count > 0)
            {
                if (proxyRol.GetRole(list.FirstOrDefault().IdRol).Estado == true)
                {
                    Session["Access"] = "OK";
                    Session["username"] = list[0].Usuario;
                    Session["idusuario"] = list[0].IdUsuario;
                    Session["Usuario"] = list[0];
                    ViewBag.access = true;
                    RememberMyCookies(list.FirstOrDefault());


                }
                else
                {
                    Response.Cookies["usuario"].Value = null;
                    Response.Cookies["password"].Value = null;
                    var result = new { Success = "False", Message = "Consulta con tu Administrador el Rol que se Asignó a tu Usuario" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }


            }
            else
            {
                Response.Cookies["usuario"].Value = null;
                Response.Cookies["password"].Value = null;
                var result = new { Success = "False", Message = "Revisa tu Usuario y contraseña " };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}