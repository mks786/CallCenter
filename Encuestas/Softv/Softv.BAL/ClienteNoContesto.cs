﻿
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.ComponentModel;
    using System.Linq;
    using Softv.Providers;
    using Softv.Entities;
    using Globals;

    /// <summary>
    /// Class                   : Softv.BAL.Client.cs
    /// Generated by            : Class Generator (c) 2014
    /// Description             : ClienteNoContestoBussines
    /// File                    : ClienteNoContestoBussines.cs
    /// Creation date           : 18/08/2016
    /// Creation time           : 10:58 a. m.
    ///</summary>
    namespace Softv.BAL
    {

    [DataObject]
    [Serializable]
    public class ClienteNoContesto
    {

    #region Constructors
    public ClienteNoContesto(){}
    #endregion

    /// <summary>
    ///Adds ClienteNoContesto
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    public static int Add(ClienteNoContestoEntity objClienteNoContesto)
  {
  int result = ProviderSoftv.ClienteNoContesto.AddClienteNoContesto(objClienteNoContesto);
    return result;
    }

    /// <summary>
    ///Delete ClienteNoContesto
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    public static int Delete(int? IdNoContesto)
    {
    int resultado = ProviderSoftv.ClienteNoContesto.DeleteClienteNoContesto(IdNoContesto);
    return resultado;
    }

    /// <summary>
    ///Update ClienteNoContesto
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Update, true)]
    public static int Edit(ClienteNoContestoEntity objClienteNoContesto)
    {
    int result = ProviderSoftv.ClienteNoContesto.EditClienteNoContesto(objClienteNoContesto);
    return result;
    }

    /// <summary>
    ///Get ClienteNoContesto
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static List<ClienteNoContestoEntity> GetAll()
    {
    List<ClienteNoContestoEntity> entities = new List<ClienteNoContestoEntity> ();
    entities = ProviderSoftv.ClienteNoContesto.GetClienteNoContesto();
    
    return entities ?? new List<ClienteNoContestoEntity>();
    }

    /// <summary>
    ///Get ClienteNoContesto List<lid>
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static List<ClienteNoContestoEntity> GetAll(List<int> lid)
    {
    List<ClienteNoContestoEntity> entities = new List<ClienteNoContestoEntity> ();
    entities = ProviderSoftv.ClienteNoContesto.GetClienteNoContesto(lid);    
    return entities ?? new List<ClienteNoContestoEntity>();
    }

    /// <summary>
    ///Get ClienteNoContesto By Id
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static ClienteNoContestoEntity GetOne(int? IdNoContesto)
    {
    ClienteNoContestoEntity result = ProviderSoftv.ClienteNoContesto.GetClienteNoContestoById(IdNoContesto);
    
    return result;
    }

    /// <summary>
    ///Get ClienteNoContesto By Id
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select)]
    public static ClienteNoContestoEntity GetOneDeep(int? IdNoContesto)
    {
    ClienteNoContestoEntity result = ProviderSoftv.ClienteNoContesto.GetClienteNoContestoById(IdNoContesto);
    
    return result;
    }
    

    
      /// <summary>
      ///Get ClienteNoContesto
      ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static SoftvList<ClienteNoContestoEntity> GetPagedList(int pageIndex, int pageSize)
    {
    SoftvList<ClienteNoContestoEntity> entities = new SoftvList<ClienteNoContestoEntity>();
    entities = ProviderSoftv.ClienteNoContesto.GetPagedList(pageIndex, pageSize);
    
    return entities ?? new SoftvList<ClienteNoContestoEntity>();
    }

    /// <summary>
    ///Get ClienteNoContesto
    ///</summary>
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public static SoftvList<ClienteNoContestoEntity> GetPagedList(int pageIndex, int pageSize,String xml)
    {
    SoftvList<ClienteNoContestoEntity> entities = new SoftvList<ClienteNoContestoEntity>();
    entities = ProviderSoftv.ClienteNoContesto.GetPagedList(pageIndex, pageSize,xml);
    
    return entities ?? new SoftvList<ClienteNoContestoEntity>();
    }


    }




    #region Customs Methods
    
    #endregion
    }
  