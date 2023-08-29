using frame8.Logic.Misc.Other.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LuanVan.OSA
{
    public class PostItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIPostListViewItem postListViewItem;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textCreateDate;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(PostItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            postListViewItem = root.GetComponent<UIPostListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/layout_post_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_header/text_create_date", out textCreateDate);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_body/text_content", out textContent);
        }
        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var post = model as PostItemModel;

            postListViewItem.PostModel = post.PostModel;
            postListViewItem.PostModel.ItemIndexOSA = model.id;
            postListViewItem.PostModel.ViewsHolder = baseVH as PostItemViewsHolder;

            textName.text = post.PostModel.UserFullname;

            DateTime postCreateDate = new DateTime();

            if (DateTime.TryParse(post.PostModel.CreateAt, out postCreateDate)) { }

            textCreateDate.text = postCreateDate.ToString("yyyy-MM-dd HH:mm");
            textContent.text = post.PostModel.Content;

            MarkForRebuild();
        }

        public override void MarkForRebuild()
        {
            base.MarkForRebuild();

            contentSizeFitter.enabled = true;
        }

        public override void UnmarkForRebuild()
        {
            contentSizeFitter.enabled = true;

            base.UnmarkForRebuild();
        }

    }
}
