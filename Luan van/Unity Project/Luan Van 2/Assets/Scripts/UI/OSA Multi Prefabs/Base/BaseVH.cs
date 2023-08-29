using Com.TheFallenGames.OSA.Core;
using Com.TheFallenGames.OSA.Demos.MultiplePrefabs.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public abstract class BaseVH : BaseItemViewsHolder
    {
        public abstract bool CanPresentModelType(Type modelType);

        /// <inheritdoc/>
        public override void CollectViews()
        {
            base.CollectViews();
        }


        /// <summary>
        /// Called to update the views from the specified model. Overriden by inheritors to update their own views after casting the model to its known type
        /// </summary>
        public virtual void UpdateViews(BaseModel model, BaseVH baseVH)
        {

        }
    }
}