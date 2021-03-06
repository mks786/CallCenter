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
    /// Class                   : Softv.Providers.MotAtenTelProvider
    /// Generated by            : Class Generator (c) 2014
    /// Description             : MotAtenTel Provider
    /// File                    : MotAtenTelProvider.cs
    /// Creation date           : 27/07/2016
    /// Creation time           : 10:21 a. m.
    /// </summary>
    public abstract class MotAtenTelProvider : Globals.DataAccess
    {

    /// <summary>
    /// Instance of MotAtenTel from DB
    /// </summary>
    private static MotAtenTelProvider _Instance = null;

    private static ObjectHandle obj;
    /// <summary>
    /// Generates a MotAtenTel instance
    /// </summary>
    public static MotAtenTelProvider Instance
    {
    get
    {
    if (_Instance == null)
    {
    obj = Activator.CreateInstance(
    SoftvSettings.Settings.MotAtenTel.Assembly,
    SoftvSettings.Settings.MotAtenTel.DataClass);
    _Instance = (MotAtenTelProvider)obj.Unwrap();
    }
    return _Instance;
    }
    }

    /// <summary>
    /// Provider's default constructor
    /// </summary>
    public MotAtenTelProvider()
    {
    }
    /// <summary>
    /// Abstract method to add MotAtenTel
    ///  /summary>
    /// <param name="MotAtenTel"></param>
    /// <returns></returns>
    public abstract int AddMotAtenTel(MotAtenTelEntity entity_MotAtenTel);

    /// <summary>
    /// Abstract method to delete MotAtenTel
    /// </summary>
    public abstract int DeleteMotAtenTel(int? Clv_Motivo);

    /// <summary>
    /// Abstract method to update MotAtenTel
    /// </summary>
    public abstract int EditMotAtenTel(MotAtenTelEntity entity_MotAtenTel);

    /// <summary>
    /// Abstract method to get all MotAtenTel
    /// </summary>
    public abstract List<MotAtenTelEntity> GetMotAtenTel();

    /// <summary>
    /// Abstract method to get all MotAtenTel List<int> lid
    /// </summary>
    public abstract List<MotAtenTelEntity> GetMotAtenTel(List<int> lid);

    /// <summary>
    /// Abstract method to get by id
    /// </summary>
    public abstract MotAtenTelEntity GetMotAtenTelById(int? Clv_Motivo);

    

    /// <summary>
    ///Get MotAtenTel
    ///</summary>
    public abstract SoftvList<MotAtenTelEntity> GetPagedList(int pageIndex, int pageSize);

    /// <summary>
    ///Get MotAtenTel
    ///</summary>
    public abstract SoftvList<MotAtenTelEntity> GetPagedList(int pageIndex, int pageSize,String xml);

    /// <summary>
    /// Converts data from reader to entity
    /// </summary>
    protected virtual MotAtenTelEntity GetMotAtenTelFromReader(IDataReader reader)
    {
    MotAtenTelEntity entity_MotAtenTel = null;
    try
    {
      entity_MotAtenTel = new MotAtenTelEntity();
    entity_MotAtenTel.Clv_Motivo = (int?)(GetFromReader(reader,"Clv_Motivo"));
          entity_MotAtenTel.Descripcion = (String)(GetFromReader(reader,"Descripcion", IsString : true));
        
    }
    catch (Exception ex)
    {
    throw new Exception("Error converting MotAtenTel data to entity", ex);
    }
    return entity_MotAtenTel;
    }
    
    }

    #region Customs Methods
    
    #endregion
    }

  