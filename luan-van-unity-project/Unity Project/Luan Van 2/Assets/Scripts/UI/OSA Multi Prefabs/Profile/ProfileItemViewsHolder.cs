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
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var profile = model as ProfileItemModel;

            profileListViewItem.ProfileModel = profile.ProfileModel;
            profileListViewItem.ProfileModel.ItemIndexOSA = model.id;
            profileListViewItem.ProfileModel.ViewsHolder = baseVH as ProfileItemViewsHolder;

            textName.text = profileListViewItem.ProfileModel.Name;

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
