using frame8.Logic.Misc.Other.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class PostItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIPostListViewItem postListViewItem;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textCreateDate;
        public TextMeshProUGUI textLikeCount;
        public TextMeshProUGUI textCommentCount;
        public Image imageBackground;


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
            root.GetComponentAtPath("layout_listview_item_content/img_background", out imageBackground);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_footer/text_vote_count", out textLikeCount);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_footer/text_comment_count", out textCommentCount);
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

            Color themeColor = Color.white;

            if (ColorUtility.TryParseHtmlString(postListViewItem.PostModel.ThemeColor, out themeColor))
            {

            }
            imageBackground.color = themeColor;

            textLikeCount.text = post.PostModel.LikeCount.ToString();

            textCommentCount.text = post.PostModel.CommentCount.ToString();

            postListViewItem.UpdateLikeButtonColor();

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
