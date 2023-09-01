using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public abstract class BaseModel
    {
        #region Data Fields
        /// <summary>
        /// Common data field for all derived models
        /// </summary>
        public int id;
        public bool hasPendingSizeChange;
        #endregion

        #region View State
        /// <summary>
        /// Assigned in the constructor. 
        /// It's related to the visual state and not a data field per-se, 
        /// but the gains in performance are huge if it's declared here, 
        /// compared to being managed in a separate array or class
        /// </summary>
        public Type CachedType { get; private set; }
        #endregion

        public BaseModel()
        { 
            CachedType = GetType(); 
        }
    }
}
