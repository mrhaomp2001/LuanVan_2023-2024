using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using frame8.Logic.Misc.Other.Extensions;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEditor.Experimental.GraphView;

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


            DateTime postCreateDate = new DateTime();

            if (DateTime.TryParse(commentListViewItem.CommentModel.CreatedAt, out postCreateDate)) { }

            textCreateDate.text = postCreateDate.ToString("yyyy-MM-dd HH:mm");

            textName.text = commentListViewItem.CommentModel.UserFullName;

            textContent.text = commentListViewItem.CommentModel.Content;
            textLikeCount.text = commentListViewItem.CommentModel.LikeCount.ToString();

            commentListViewItem.UpdateLikeButtonColor();

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
