using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using frame8.Logic.Misc.Other.Extensions;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class LatestOnlineUserItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UILatestOnlineUserListViewItem latestOnlineUserListViewItem;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textUpdatedDate;
        public RectTransform containerAddfriend;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(LatestOnlineUserItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();

            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            latestOnlineUserListViewItem = root.GetComponent<UILatestOnlineUserListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/text_latest_time_login", out textUpdatedDate);

            root.GetComponentAtPath("layout_listview_add_friend", out containerAddfriend);

        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var user = model as LatestOnlineUserItemModel;

            latestOnlineUserListViewItem.LatestOnlineUserModel = user.LatestOnlineUserModel;
            latestOnlineUserListViewItem.LatestOnlineUserModel.ItemIndexOSA = model.id;
            latestOnlineUserListViewItem.LatestOnlineUserModel.ViewsHolder = baseVH as LatestOnlineUserItemViewsHolder;

            DateTime createDate = new DateTime();

            if (DateTime.TryParse(latestOnlineUserListViewItem.LatestOnlineUserModel.UpdatedAt, out createDate)) { }

            TimeSpan timeSpanCreateDate = DateTime.Now - createDate;

            textUpdatedDate.text = "Vừa xong";

            if (timeSpanCreateDate.TotalMinutes >= 1)
            {
                textUpdatedDate.text = timeSpanCreateDate.Minutes.ToString() + " phút trước";
            }

            if (timeSpanCreateDate.TotalHours >= 1)
            {
                textUpdatedDate.text = timeSpanCreateDate.Hours.ToString() + " giờ trước";
            }

            if (timeSpanCreateDate.TotalDays >= 1 && timeSpanCreateDate.TotalDays <= 3)
            {
                textUpdatedDate.text = timeSpanCreateDate.Days.ToString() + " ngày trước";
            }

            if (timeSpanCreateDate.TotalDays > 3)
            {
                textUpdatedDate.text = createDate.ToString("yyyy-MM-dd HH:mm");
            }

            textName.text = latestOnlineUserListViewItem.LatestOnlineUserModel.Name;
            textUpdatedDate.text = "Đăng nhập: " + textUpdatedDate.text;

            if (latestOnlineUserListViewItem.LatestOnlineUserModel.ContainerOSA.Equals("waitingFriend"))
            {
                containerAddfriend.gameObject.SetActive(true);
            }
            else
            {
                containerAddfriend.gameObject.SetActive(false);
            }

            latestOnlineUserListViewItem.CheckAndDownloadAvatar();

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
