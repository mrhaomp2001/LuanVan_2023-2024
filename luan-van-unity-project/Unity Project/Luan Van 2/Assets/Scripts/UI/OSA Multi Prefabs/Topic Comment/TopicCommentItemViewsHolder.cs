using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using frame8.Logic.Misc.Other.Extensions;
using UnityEngine.UI;
using System;
using TMPro;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class TopicCommentItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UITopicCommentListViewItem topicCommentListViewItem;

        public TextMeshProUGUI textName;
        public TextMeshProUGUI textCreateDate;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textLikeCount;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(TopicCommentItemModel);
        }
        public override void CollectViews()
        {
            base.CollectViews();

            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            topicCommentListViewItem = root.GetComponent<UITopicCommentListViewItem>();


            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_create_date", out textCreateDate);

            root.GetComponentAtPath("layout_listview_item_content/layout_body/text_content", out textContent);

            root.GetComponentAtPath("layout_listview_item_content/layout_footer/text_vote_count", out textLikeCount);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var topicComment = model as TopicCommentItemModel;

            topicCommentListViewItem.TopicCommentModel = topicComment.TopicCommentModel;
            topicCommentListViewItem.TopicCommentModel.ViewsHolder = baseVH as TopicCommentItemViewsHolder;

            textName.text = topicCommentListViewItem.TopicCommentModel.UserFullName;
            textContent.text = topicCommentListViewItem.TopicCommentModel.Content;
            textLikeCount.text = topicCommentListViewItem.TopicCommentModel.LikeCount.ToString();


            if (DateTime.TryParse(topicCommentListViewItem.TopicCommentModel.CreatedAt, out DateTime createDate)) { }

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

            if (timeSpanCreateDate.TotalDays > 7)
            {
                textCreateDate.text = createDate.ToString("yyyy-MM-dd HH:mm");
            }
            topicCommentListViewItem.UpdateLikeButtonColor();
            topicCommentListViewItem.UpdateViews();
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
