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
    public class PostTemplateItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIPostTemplateListViewItem postTemplateListViewItem;
        public TextMeshProUGUI textName;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(PostTemplateItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            postTemplateListViewItem = root.GetComponent<UIPostTemplateListViewItem>();
            root.GetComponentAtPath("layout_listview_item_content/text_content", out textName);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var template = model as PostTemplateItemModel;

            postTemplateListViewItem.PostTemplateModel = template.PostTemplateModel;
            postTemplateListViewItem.PostTemplateModel.ItemIndexOSA = model.id;
            postTemplateListViewItem.PostTemplateModel.ViewsHolder = baseVH as PostTemplateItemViewsHolder;

            textName.text = template.PostTemplateModel.Name;

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
