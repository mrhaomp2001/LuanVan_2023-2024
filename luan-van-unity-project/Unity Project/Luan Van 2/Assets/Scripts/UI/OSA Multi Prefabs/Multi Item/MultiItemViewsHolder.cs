using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class MultiItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIMultiListViewItem multiListViewItem;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(MultiItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            multiListViewItem = root.GetComponent<UIMultiListViewItem>();
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var multiItem = model as MultiItemModel;

            multiListViewItem.MultiModel = multiItem.MultiModel;
            multiListViewItem.MultiModel.ViewsHolder = baseVH as MultiItemViewsHolder;

            multiListViewItem.UpdateView();
            MarkForRebuild();
        }

        public override void MarkForRebuild()
        {
            base.MarkForRebuild();
            contentSizeFitter.enabled = true;
        }

        public override void UnmarkForRebuild()
        {

            contentSizeFitter.enabled = false;
            base.UnmarkForRebuild();
        }
    }
}
