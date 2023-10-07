using frame8.Logic.Misc.Other.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class ProfileItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIProfileListViewItem profileListViewItem;

        public TextMeshProUGUI textName;
        public TextMeshProUGUI textFriendStatus;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(ProfileItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();

            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            profileListViewItem = root.GetComponent<UIProfileListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_body/container_button/btn_add_friend/text_friend_status", out textFriendStatus);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var profile = model as ProfileItemModel;

            profileListViewItem.ProfileModel = profile.ProfileModel;
            profileListViewItem.ProfileModel.ItemIndexOSA = model.id;
            profileListViewItem.ProfileModel.ViewsHolder = baseVH as ProfileItemViewsHolder;

            textName.text = profileListViewItem.ProfileModel.Name;

            if (profileListViewItem.ProfileModel.FriendStatusToOther.Equals("1") || profileListViewItem.ProfileModel.FriendStatusToOther.Equals(""))
            {
                textFriendStatus.text = "Kết bạn";
            }

            if (profileListViewItem.ProfileModel.FriendStatusToOther.Equals("2"))
            {
                if (profileListViewItem.ProfileModel.FriendStatusToSelf.Equals("2"))
                {
                    textFriendStatus.text = "Bạn bè";
                }

                if (profileListViewItem.ProfileModel.FriendStatusToSelf.Equals("3"))
                {
                    textFriendStatus.text = "Đã gửi lời mời";
                }
            }

            if (profileListViewItem.ProfileModel.FriendStatusToOther.Equals("3"))
            {
                textFriendStatus.text = "Chờ bạn chấp nhận";
            }
            profileListViewItem.UpdateViews();
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
