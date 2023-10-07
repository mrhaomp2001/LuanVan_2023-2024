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
    public class PostItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIPostListViewItem postListViewItem;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textCreateDate;
        public TextMeshProUGUI textLikeCount;
        public TextMeshProUGUI textCommentCount;
        public TextMeshProUGUI textTemplate;
        public RectTransform rectTransformVisibility;
        public Image imageTagBackground;


        public TextMeshProUGUI textTitle;
        public RectTransform containerTitle;
        public RectTransform containerImagePost;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(PostItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            postListViewItem = root.GetComponent<UIPostListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/img_background/layout_post_menu_header/layout_template/image", out imageTagBackground);
            root.GetComponentAtPath("layout_listview_item_content/img_background/layout_post_menu_header/layout_visibility", out rectTransformVisibility);

            root.GetComponentAtPath("layout_listview_item_content/layout_post_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_header/text_create_date", out textCreateDate);

            root.GetComponentAtPath("layout_listview_item_content/layout_post_body/text_content", out textContent);

            root.GetComponentAtPath("layout_listview_item_content/layout_post_footer/text_vote_count", out textLikeCount);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_footer/text_comment_count", out textCommentCount);

            root.GetComponentAtPath("layout_listview_item_content/img_background/layout_post_menu_header/layout_template/text_template", out textTemplate);

            root.GetComponentAtPath("layout_listview_item_content/layout_post_title", out containerTitle);
            root.GetComponentAtPath("layout_listview_item_content/layout_post_title/text_content", out textTitle);

            root.GetComponentAtPath("layout_listview_item_content/layout_post_image", out containerImagePost);
        }
        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var post = model as PostItemModel;

            postListViewItem.PostModel = post.PostModel;
            postListViewItem.PostModel.ItemIndexOSA = model.id;
            postListViewItem.PostModel.ViewsHolder = baseVH as PostItemViewsHolder;

            textName.text = post.PostModel.UserFullname;

            DateTime createDate = new DateTime();

            if (DateTime.TryParse(post.PostModel.CreateAt, out createDate)) { }

            TimeSpan timeSpanCreateDate = DateTime.Now - createDate;

            textCreateDate.text = "Vừa xong";

            if (timeSpanCreateDate.TotalMinutes >= 1)
            {
                textCreateDate.text = timeSpanCreateDate.Minutes.ToString() + " phút trước";
            }

            if (timeSpanCreateDate.TotalHours >= 1)
            {
                textCreateDate.text = timeSpanCreateDate.Hours.ToString() + " giờ trước";
            }

            if (timeSpanCreateDate.TotalDays >= 1)
            {
                textCreateDate.text = timeSpanCreateDate.Days.ToString() + " ngày trước";
            }

            if (timeSpanCreateDate.TotalDays > 3)
            {
                textCreateDate.text = createDate.ToString("yyyy-MM-dd HH:mm");
            }

            textContent.text = post.PostModel.Content;

            if (!postListViewItem.PostModel.ContainerOSA.Equals("topic"))
            {
                if (ColorUtility.TryParseHtmlString(postListViewItem.PostModel.ThemeColor, out Color themeColor)) { }
                imageTagBackground.color = themeColor;
            }
            textLikeCount.text = post.PostModel.LikeCount.ToString();

            textCommentCount.text = post.PostModel.CommentCount.ToString();

            if (post.PostModel.PostStatus.Equals("2"))
            {
                rectTransformVisibility.gameObject.SetActive(true);
            }
            else
            {
                rectTransformVisibility.gameObject.SetActive(false);
            }

            textTemplate.text = postListViewItem.PostModel.PosTemplateName;

            if (!post.PostModel.ImagePath.Equals(""))
            {
                containerImagePost.gameObject.SetActive(true);
                postListViewItem.CheckAndDownloadImage();
            }
            else
            {
                containerImagePost.gameObject.SetActive(false);
            }

            if (!post.PostModel.PostTitle.Equals(""))
            {
                containerTitle.gameObject.SetActive(true);
                textTitle.text = post.PostModel.PostTitle;
            }
            else
            {
                containerTitle.gameObject.SetActive(false);
                textTitle.text = "";
            }

            postListViewItem.UpdateLikeButtonColor();

            postListViewItem.CheckAndGetOldPosts();

            postListViewItem.UpdateViews();

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
