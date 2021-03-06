﻿
    using System;
    using System.Text;
    using System.Data;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Runtime.Remoting;
    using Softv.Entities;
    using SoftvConfiguration;
    using Globals;

    namespace Softv.Providers
    {
    /// <summary>
    /// Class                   : Softv.Providers.ClienteNoContestoProvider
    /// Generated by            : Class Generator (c) 2014
    /// Description             : ClienteNoContesto Provider
    /// File                    : ClienteNoContestoProvider.cs
    /// Creation date           : 18/08/2016
    /// Creation time           : 10:58 a. m.
    /// </summary>
    public abstract class ClienteNoContestoProvider : Globals.DataAccess
    {

    /// <summary>
    /// Instance of ClienteNoContesto from DB
    /// </summary>
    private static ClienteNoContestoProvider _Instance = null;

    private static ObjectHandle obj;
    /// <summary>
    /// Generates a ClienteNoContesto instance
    /// </summary>
    public static ClienteNoContestoProvider Instance
    {
    get
    {
    if (_Instance == null)
    {
    obj = Activator.CreateInstance(
    SoftvSettings.Settings.ClienteNoContesto.Assembly,
    SoftvSettings.Settings.ClienteNoContesto.DataClass);
    _Instance = (ClienteNoContestoProvider)obj.Unwrap();
    }
    return _Instance;
    }
    }

    /// <summary>
    /// Provider's default constructor
    /// </summary>
    public ClienteNoContestoProvider()
    {
    }
    /// <summary>
    /// Abstract method to add ClienteNoContesto
    ///  /summary>
    /// <param name="ClienteNoContesto"></param>
    /// <returns></returns>
    public abstract int AddClienteNoContesto(ClienteNoContestoEntity entity_ClienteNoContesto);

    /// <summary>
    /// Abstract method to delete ClienteNoContesto
    /// </summary>
    public abstract int DeleteClienteNoContesto(int? IdNoContesto);

    /// <summary>
    /// Abstract method to update ClienteNoContesto
    /// </summary>
    public abstract int EditClienteNoContesto(ClienteNoContestoEntity entity_ClienteNoContesto);

    /// <summary>
    /// Abstract method to get all ClienteNoContesto
    /// </summary>
    public abstract List<ClienteNoContestoEntity> GetClienteNoContesto();

    /// <summary>
    /// Abstract method to get all ClienteNoContesto List<int> lid
    /// </summary>
    public abstract List<ClienteNoContestoEntity> GetClienteNoContesto(List<int> lid);

    /// <summary>
    /// Abstract method to get by id
    /// </summary>
    public abstract ClienteNoContestoEntity GetClienteNoContestoById(int? IdNoContesto);

    

    /// <summary>
    ///Get ClienteNoContesto
    ///</summary>
    public abstract SoftvList<ClienteNoContestoEntity> GetPagedList(int pageIndex, int pageSize);

    /// <summary>
    ///Get ClienteNoContesto
    ///</summary>
    public abstract SoftvList<ClienteNoContestoEntity> GetPagedList(int pageIndex, int pageSize,String xml);

    /// <summary>
    /// Converts data from reader to entity
    /// </summary>
    protected virtual ClienteNoContestoEntity GetClienteNoContestoFromReader(IDataReader reader)
    {
    ClienteNoContestoEntity entity_ClienteNoContesto = null;
    try
    {
      entity_ClienteNoContesto = new ClienteNoContestoEntity();
    entity_ClienteNoContesto.IdNoContesto = (int?)(GetFromReader(reader,"IdNoContesto"));
          entity_ClienteNoContesto.IdProcesoEnc = (int?)(GetFromReader(reader,"IdProcesoEnc"));
          entity_ClienteNoContesto.IdEncuesta = (int?)(GetFromReader(reader,"IdEncuesta"));
          entity_ClienteNoContesto.Contrato = (long?)(GetFromReader(reader,"Contrato"));
          entity_ClienteNoContesto.FechaApli = (DateTime?)(GetFromReader(reader,"FechaApli"));
          entity_ClienteNoContesto.IdPlaza = (int?)(GetFromReader(reader,"IdPlaza"));
          
    }
    catch (Exception ex)
    {
    throw new Exception("Error converting ClienteNoContesto data to entity", ex);
    }
    return entity_ClienteNoContesto;
    }
    
    }

    #region Customs Methods
    
    #endregion
    }

  