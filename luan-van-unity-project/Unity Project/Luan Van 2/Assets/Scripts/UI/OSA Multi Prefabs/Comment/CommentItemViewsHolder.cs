using frame8.Logic.Misc.Other.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class CommentItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UICommentListViewItem commentListViewItem;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textCreateDate;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textLikeCount;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(CommentItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            commentListViewItem = root.GetComponent<UICommentListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_create_date", out textCreateDate);

            root.GetComponentAtPath("layout_listview_item_content/layout_body/text_content", out textContent);

            root.GetComponentAtPath("layout_listview_item_content/layout_footer/text_vote_count", out textLikeCount);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var comment = model as CommentItemModel;

            commentListViewItem.CommentModel = comment.CommentModel;
            commentListViewItem.CommentModel.ItemIndexOSA = model.id;
            commentListViewItem.CommentModel.ViewsHolder = baseVH as CommentItemViewsHolder;


            DateTime createDate = new DateTime();

            if (DateTime.TryParse(commentListViewItem.CommentModel.CreatedAt, out createDate)) { }

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

            if (timeSpanCreateDate.TotalDays >= 1 && timeSpanCreateDate.TotalDays <= 3)
            {
                textCreateDate.text = timeSpanCreateDate.Days.ToString() + " ngày trước";
            }

            if (timeSpanCreateDate.TotalDays > 3)
            {
                textCreateDate.text = createDate.ToString("yyyy-MM-dd HH:mm");
            }

            textName.text = commentListViewItem.CommentModel.UserFullName;

            textContent.text = commentListViewItem.CommentModel.Content;
            textLikeCount.text = commentListViewItem.CommentModel.LikeCount.ToString();

            commentListViewItem.UpdateLikeButtonColor();
            commentListViewItem.CheckAndGetOldComment();
            commentListViewItem.UpdateViews();

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
