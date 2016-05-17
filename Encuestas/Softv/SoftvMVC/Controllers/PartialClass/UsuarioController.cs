using Softv.Entities;
using SoftvMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoftvMVC.Controllers
{
    public partial class UsuarioController
    {

        //login de app
        [HttpPost, ActionName("logon")]
        public ActionResult logon(string user, string pass)
        {
            List<UsuarioEntity> list = null;
            List<UsuarioEntity> listUsuario = new List<UsuarioEntity>();

            UsuarioEntity objUser = proxy.GetusuarioByUserAndPass(user,pass);
            //proxy.GetUsuarioList().ToList().ForEach(x =>
            //listUsuario.Add(x));
            //list = listUsuario.Where(x => x.Usuario.ToUpper().Equals(user.ToUpper()) && x.Password.ToUpper().Equals(pass.ToUpper()) && x.Estado == true).ToList();
            if (objUser != null)
            {
                if (proxyRole.GetRole(objUser.IdRol).Estado == true)
                {
                    Session["Access"] = "OK";
                    Session["username"] = objUser.Usuario;
                    Session["idusuario"] = objUser.IdUsuario;
                    Session["Usuario"] = objUser;
                    ViewBag.access = true;
                    RememberMyCookies(objUser);


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


        // ACCESS

        public ActionResult AccessSuccess(string user, string pass)
        {
            List<ModuleEntity> lstModuleFilter = new List<ModuleEntity>();
            List<ModuleEntity> Lm = new List<ModuleEntity>();
            List<UsuarioEntity> listUsuario = new List<UsuarioEntity>();
            proxy.GetUsuarioList().ToList().ForEach(x =>
            listUsuario.Add(x));
            listUsuario = listUsuario.Where(x => x.Usuario.ToUpper().Equals(user.ToUpper()) && x.Password.ToUpper().Equals(pass.ToUpper()) && x.Estado == true).ToList();
            if (listUsuario.Count > 0)
            {
                List<PermisoEntity> lstPermiso =
                                            proxyPermiso.GetXmlPermiso(Globals.SerializeTool.Serialize<PermisoEntity>(new PermisoEntity() { IdRol = listUsuario.FirstOrDefault().IdRol })).ToList();

                lstModuleFilter = (from c in proxyModule.GetModuleList()
                                   where (from o in lstPermiso
                                          select o.IdModule).Contains(c.IdModule)
                                   select c).ToList();
                lstPermiso.ForEach(XPermiso => lstModuleFilter.Where(x => x.IdModule.HasValue).Where(x => x.IdModule == XPermiso.IdModule).ToList().ForEach(y => y.Permiso = XPermiso));

                Lm = lstModuleFilter.Where(x => x.ModulePath == "PermisoAgregarPiezas").ToList(); ;
                if (Lm.Count > 0 && Lm.FirstOrDefault().Permiso.OptAdd == true)
                {

                    Session["AccessPNP"] = "OK";
                    var result = new { Success = "Tue", Message = "success" };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    Session["AccessPNP"] = null;
                    var result = new { Success = "False", Message = "No Cuentas con los permisos de agregar Partes No programadas" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }

            }
            var re = new { Success = "False", Message = "Usuario y/o Constraseña incorrectos" };
            return Json(re, JsonRequestBehavior.AllowGet);
        }


        //log out de app
        public ActionResult logout()
        {
            Session["Access"] = null;
            Session["lasturl"] = null;
            Response.Cookies["usuario"].Value = null;
            Response.Cookies["password"].Value = null;
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ChangePassword(string passactual, string nuevopass, int idusuario)
        {

            UsuarioEntity objusuario = null;
            objusuario = proxy.GetUsuario(idusuario);
            if (objusuario != null)
            {
                if (passactual == objusuario.Password)
                {

                    objusuario.BaseRemoteIp = RemoteIp;
                    objusuario.BaseIdUser = LoggedUserName;
                    objusuario.Password = nuevopass;

                    try
                    {
                        proxy.UpdateUsuario(objusuario);
                    }
                    catch (Exception ex)
                    {
                        AssingMessageScript(ex.Message, "error", "Error", true);
                        CheckNotify();
                    }

                    objusuario.Password = nuevopass;
                    Session["message"] = "Se modifico la contraseña del usuario' " + objusuario.Usuario + " ' en el sistema.";
                    Session["typemsj"] = "success";
                    Session["titlemsj"] = "Éxito";
                    Session["notify"] = true;
                    //return View();
                    //CheckNotify();
                    var result = new { tipomsj = "error", titulomsj = "Aviso", Success = "True", Message = "contraseña actual incorrecta" };
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    var result = new { tipomsj = "error", titulomsj = "Aviso", Success = "False", Message = "contraseña actual incorrecta" };
                    return Json(result, JsonRequestBehavior.AllowGet);
                    //contraseña actual incorrecta
                }

            }
            else
            {
                var result = new { tipomsj = "error", titulomsj = "Aviso", Success = "False", Message = "no se encontro el id del usuario" };
                return Json(result, JsonRequestBehavior.AllowGet);
                //no se encontro el id del usuario
            }

        }

        public ActionResult ChangePasswordAdmin()
        {
            return View();
        }


    }
}