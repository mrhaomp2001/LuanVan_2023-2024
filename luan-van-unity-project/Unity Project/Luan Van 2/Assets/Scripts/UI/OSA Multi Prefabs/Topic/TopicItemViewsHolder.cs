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
    public class TopicItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UITopicListViewItem topicListViewItem;

        public TextMeshProUGUI textName;
        public TextMeshProUGUI textContent;
        public TextMeshProUGUI textCreateDate;
        public TextMeshProUGUI textLikeCount;
        public TextMeshProUGUI textCommentCount;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(TopicItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            topicListViewItem = root.GetComponent<UITopicListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_create_date", out textCreateDate);

            root.GetComponentAtPath("layout_listview_item_content/layout_body/text_content", out textContent);

            root.GetComponentAtPath("layout_listview_item_content/layout_footer/text_vote_count", out textLikeCount);
            root.GetComponentAtPath("layout_listview_item_content/layout_footer/text_comment_count", out textCommentCount);

        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var topic = model as TopicItemModel;

            topicListViewItem.TopicModel = topic.TopicModel;
            topicListViewItem.TopicModel.ViewsHolder = baseVH as TopicItemViewsHolder;


            if (DateTime.TryParse(topic.TopicModel.CreateAt, out DateTime createDate)) { }

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

            textName.text = topicListViewItem.TopicModel.UserFullname;

            textContent.text = topicListViewItem.TopicModel.Content;

            textCommentCount.text = topicListViewItem.TopicModel.CommentCount.ToString();

            textLikeCount.text = topicListViewItem.TopicModel.LikeCount.ToString();

            topicListViewItem.UpdateLikeButtonColor();

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
