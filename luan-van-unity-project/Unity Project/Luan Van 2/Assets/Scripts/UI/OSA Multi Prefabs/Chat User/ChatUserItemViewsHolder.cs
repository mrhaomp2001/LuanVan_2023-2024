using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class ChatUserItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIChatUserListViewItem chatUserListViewItem;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(ChatUserItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            chatUserListViewItem = root.GetComponent<UIChatUserListViewItem>();
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var chatUser = model as ChatUserItemModel;

            chatUserListViewItem.ChatUserModel = chatUser.ChatUserModel;
            chatUserListViewItem.ChatUserModel.ViewsHolder = baseVH as ChatUserItemViewsHolder;

            chatUserListViewItem.UpdateViews();
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
