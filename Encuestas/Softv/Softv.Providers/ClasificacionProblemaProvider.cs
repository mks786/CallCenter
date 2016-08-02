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
    /// Class                   : Softv.Providers.ClasificacionProblemaProvider
    /// Generated by            : Class Generator (c) 2014
    /// Description             : ClasificacionProblema Provider
    /// File                    : ClasificacionProblemaProvider.cs
    /// Creation date           : 28/07/2016
    /// Creation time           : 06:23 p. m.
    /// </summary>
    public abstract class ClasificacionProblemaProvider : Globals.DataAccess
    {

    /// <summary>
    /// Instance of ClasificacionProblema from DB
    /// </summary>
    private static ClasificacionProblemaProvider _Instance = null;

    private static ObjectHandle obj;
    /// <summary>
    /// Generates a ClasificacionProblema instance
    /// </summary>
    public static ClasificacionProblemaProvider Instance
    {
    get
    {
    if (_Instance == null)
    {
    obj = Activator.CreateInstance(
    SoftvSettings.Settings.ClasificacionProblema.Assembly,
    SoftvSettings.Settings.ClasificacionProblema.DataClass);
    _Instance = (ClasificacionProblemaProvider)obj.Unwrap();
    }
    return _Instance;
    }
    }

    /// <summary>
    /// Provider's default constructor
    /// </summary>
    public ClasificacionProblemaProvider()
    {
    }
    /// <summary>
    /// Abstract method to add ClasificacionProblema
    ///  /summary>
    /// <param name="ClasificacionProblema"></param>
    /// <returns></returns>
    public abstract int AddClasificacionProblema(ClasificacionProblemaEntity entity_ClasificacionProblema);

    /// <summary>
    /// Abstract method to delete ClasificacionProblema
    /// </summary>
    public abstract int DeleteClasificacionProblema(long? ClvProblema);

    /// <summary>
    /// Abstract method to update ClasificacionProblema
    /// </summary>
    public abstract int EditClasificacionProblema(ClasificacionProblemaEntity entity_ClasificacionProblema);

    /// <summary>
    /// Abstract method to get all ClasificacionProblema
    /// </summary>
    public abstract List<ClasificacionProblemaEntity> GetClasificacionProblema();

    /// <summary>
    /// Abstract method to get all ClasificacionProblema List<int> lid
    /// </summary>
    public abstract List<ClasificacionProblemaEntity> GetClasificacionProblema(List<int> lid);

    /// <summary>
    /// Abstract method to get by id
    /// </summary>
    public abstract ClasificacionProblemaEntity GetClasificacionProblemaById(long? ClvProblema);

    

    /// <summary>
    ///Get ClasificacionProblema
    ///</summary>
    public abstract SoftvList<ClasificacionProblemaEntity> GetPagedList(int pageIndex, int pageSize);

    /// <summary>
    ///Get ClasificacionProblema
    ///</summary>
    public abstract SoftvList<ClasificacionProblemaEntity> GetPagedList(int pageIndex, int pageSize,String xml);

    /// <summary>
    /// Converts data from reader to entity
    /// </summary>
    protected virtual ClasificacionProblemaEntity GetClasificacionProblemaFromReader(IDataReader reader)
    {
    ClasificacionProblemaEntity entity_ClasificacionProblema = null;
    try
    {
      entity_ClasificacionProblema = new ClasificacionProblemaEntity();
    entity_ClasificacionProblema.ClvProblema = (long?)(GetFromReader(reader,"ClvProblema"));
          entity_ClasificacionProblema.Descripcion = (String)(GetFromReader(reader,"Descripcion", IsString : true));
        entity_ClasificacionProblema.Activo = (bool?)(GetFromReader(reader,"Activo"));
          
    }
    catch (Exception ex)
    {
    throw new Exception("Error converting ClasificacionProblema data to entity", ex);
    }
    return entity_ClasificacionProblema;
    }
    
    }

    #region Customs Methods
    
    #endregion
    }

  