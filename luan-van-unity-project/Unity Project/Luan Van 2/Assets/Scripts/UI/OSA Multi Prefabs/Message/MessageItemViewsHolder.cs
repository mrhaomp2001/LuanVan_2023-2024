using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class MessageItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIMessageListViewItem messageListViewItem;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(MessageItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();

            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            messageListViewItem = root.GetComponent<UIMessageListViewItem>();
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var message = model as MessageItemModel;

            messageListViewItem.MessageModel = message.MessageModel;
            messageListViewItem.MessageModel.ViewsHolder = baseVH as MessageItemViewsHolder;

            messageListViewItem.UpdateViews();
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
