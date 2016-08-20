﻿
    using SoftvMVC.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using PagedList;
    using Softv.Entities;
    using Globals;

    namespace SoftvMVC.Controllers
    {
      /// <summary>
      /// Class                   : SoftvMVC.Controllers.ClienteNoContestoController.cs
      /// Generated by            : Class Generator (c) 2015
      /// Description             : ClienteNoContestoController
      /// File                    : ClienteNoContestoController.cs
      /// Creation date           : 18/08/2016
      /// Creation time           : 10:58 a. m.
      ///</summary>
    public partial class ClienteNoContestoController : BaseController, IDisposable
    {
    private SoftvService.ClienteNoContestoClient proxy = null;
    
    public ClienteNoContestoController()
    {
    
    proxy = new SoftvService.ClienteNoContestoClient();
    
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

    public ActionResult Index(int? page, int? pageSize)
    {
    PermisosAcceso("ClienteNoContesto");
    ViewData["Title"] = "ClienteNoContesto";
    ViewData["Message"] = "ClienteNoContesto";
    int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
    int pageNumber = (page ?? 1);
    SoftvList<ClienteNoContestoEntity> listResult = proxy.GetClienteNoContestoPagedListXml(pageNumber, pSize, SerializeTool.Serialize<ClienteNoContestoEntity>(new ClienteNoContestoEntity()));    
        
    
    CheckNotify();
    ViewBag.CustomScriptsDefault = BuildScriptsDefault("ClienteNoContesto");
    return View(new StaticPagedList<ClienteNoContestoEntity>(listResult.ToList(), pageNumber, pSize, listResult.totalCount));
    }

    public ActionResult Details(int id = 0)
    {
    ClienteNoContestoEntity objClienteNoContesto = proxy.GetClienteNoContesto(id);
    if (objClienteNoContesto == null)
    {
    return HttpNotFound();
    }
    return PartialView(objClienteNoContesto);
    }

    public ActionResult Create()
    {
    PermisosAccesoDeniedCreate("ClienteNoContesto");
    ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
    
    return View();
    }

    [HttpPost]
    public ActionResult Create(ClienteNoContestoEntity objClienteNoContesto)
    {
    if (ModelState.IsValid)
    {
    
    objClienteNoContesto.BaseRemoteIp = RemoteIp;
    objClienteNoContesto.BaseIdUser = LoggedUserName;
    int result = proxy.AddClienteNoContesto(objClienteNoContesto);
    if (result == -1)
    {
    
    AssingMessageScript("El ClienteNoContesto ya existe en el sistema.", "error", "Error", true);
    CheckNotify();
    return View(objClienteNoContesto);
    }
    if (result > 0)
    {
    AssingMessageScript("Se dio de alta el ClienteNoContesto en el sistema.", "success", "Éxito", true);
    return RedirectToAction("Index");
    }

    }
    return View(objClienteNoContesto);
    }

    public ActionResult Edit(int id = 0)
    {
    PermisosAccesoDeniedEdit("ClienteNoContesto");
    ViewBag.CustomScriptsPageValid = BuildScriptPageValid();
    ClienteNoContestoEntity objClienteNoContesto = proxy.GetClienteNoContesto(id);
    
    if (objClienteNoContesto == null)
    {
    return HttpNotFound();
    }
    return View(objClienteNoContesto);
    }

    //
    // POST: /ClienteNoContesto/Edit/5
    [HttpPost]
    public ActionResult Edit(ClienteNoContestoEntity objClienteNoContesto)
    {
    if (ModelState.IsValid)
    {
    objClienteNoContesto.BaseRemoteIp = RemoteIp;
    objClienteNoContesto.BaseIdUser = LoggedUserName;
    int result = proxy.UpdateClienteNoContesto(objClienteNoContesto);
    if (result == -1)
    {
     ClienteNoContestoEntity objClienteNoContestoOld = proxy.GetClienteNoContesto(objClienteNoContesto.IdNoContesto);
    
      AssingMessageScript("El ClienteNoContesto ya existe en el sistema, .", "error", "Error", true);
    CheckNotify();
    return View(objClienteNoContesto);
    }
    if (result > 0)
    {
    AssingMessageScript("El ClienteNoContesto se modifico en el sistema.", "success", "Éxito", true);
    CheckNotify();
    return RedirectToAction("Index");
    }
    return RedirectToAction("Index");
    }
    return View(objClienteNoContesto);
    }

    public ActionResult QuickIndex(int? page, int? pageSize, int?  IdProcesoEnc,int?  IdEncuesta,long?  Contrato,DateTime?  FechaApli,int?  IdPlaza)
    {    
    int pageNumber = (page ?? 1);
    int pSize = pageSize ?? SoftvMVC.Properties.Settings.Default.pagnum;
      SoftvList<ClienteNoContestoEntity> listResult =  null;
      List<ClienteNoContestoEntity> listClienteNoContesto = new List<ClienteNoContestoEntity>();
      ClienteNoContestoEntity objClienteNoContesto = new ClienteNoContestoEntity();
      ClienteNoContestoEntity objGetClienteNoContesto = new ClienteNoContestoEntity();

    
          if ((IdProcesoEnc != null))
        
      {
      objClienteNoContesto.IdProcesoEnc =  IdProcesoEnc;
      }      
    
          if ((IdEncuesta != null))
        
      {
      objClienteNoContesto.IdEncuesta =  IdEncuesta;
      }      
    
          if ((Contrato != null))
        
      {
      objClienteNoContesto.Contrato =  Contrato;
      }      
    
          if ((FechaApli != null))
        
      {
      objClienteNoContesto.FechaApli =  FechaApli;
      }      
    
          if ((IdPlaza != null))
        
      {
      objClienteNoContesto.IdPlaza =  IdPlaza;
      }      
    
    pageNumber = pageNumber == 0 ? 1 : pageNumber;
    listResult = proxy.GetClienteNoContestoPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objClienteNoContesto));
    if (listResult.Count == 0)
    {
    int tempPageNumber = (int)(listResult.totalCount / pSize);
    pageNumber = (int)(listResult.totalCount / pSize) == 0 ? 1 : tempPageNumber;
    listResult = proxy.GetClienteNoContestoPagedListXml(pageNumber, pSize, Globals.SerializeTool.Serialize(objClienteNoContesto));
    }
    listResult.ToList().ForEach(x => listClienteNoContesto.Add(x));
    
    var ClienteNoContestoAsIPagedList = new StaticPagedList<ClienteNoContestoEntity>(listClienteNoContesto, pageNumber, pSize, listResult.totalCount);
    if (ClienteNoContestoAsIPagedList.Count > 0 )
    {
    return PartialView(ClienteNoContestoAsIPagedList);
    }
    else
    {
    var result = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "No se encontraron registros con los criterios de búsqueda ingresados." };
    return Json(result, JsonRequestBehavior.AllowGet);
    }
    }

    public ActionResult Delete(int id = 0)
    {
    int result = proxy.DeleteClienteNoContesto(RemoteIp, LoggedUserName, id);
    if (result > 0)
    {
    var resultOk = new { tipomsj = "success", titulomsj = "Aviso", Success = "True", Message = "Registro de ClienteNoContesto Eliminado." };
    return Json(resultOk, JsonRequestBehavior.AllowGet);
    }
    else
    {
    var resultNg = new { tipomsj = "warning", titulomsj = "Aviso", Success = "False", Message = "El Registro de ClienteNoContesto No puede ser Eliminado validar dependencias." };
    return Json(resultNg, JsonRequestBehavior.AllowGet);
    }
    }


    }

    }

  