﻿
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    namespace Softv.Entities
    {
    /// <summary>
    /// Class                   : Softv.Entities.ClasificacionProblemaEntity.cs
    /// Generated by            : Class Generator (c) 2014
    /// Description             : ClasificacionProblema entity
    /// File                    : ClasificacionProblemaEntity.cs
    /// Creation date           : 28/07/2016
    /// Creation time           : 06:23 p. m.
    ///</summary>
    [DataContract]
    [Serializable]
    public class ClasificacionProblemaEntity : BaseEntity
    {
    #region Attributes
    
      /// <summary>
      /// Property ClvProblema
      /// </summary>
      [DataMember]
      public long? ClvProblema { get; set; }
      /// <summary>
      /// Property Descripcion
      /// </summary>
      [DataMember]
      public String Descripcion { get; set; }
      /// <summary>
      /// Property Activo
      /// </summary>
      [DataMember]
      public bool? Activo { get; set; }
    #endregion
    }
    }

  