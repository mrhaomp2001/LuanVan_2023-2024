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
    public class QuestionItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public TextMeshProUGUI textContent;
        public UIQuestionListViewItem questionListViewItem;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(QuestionItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();

            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            questionListViewItem = root.GetComponent<UIQuestionListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/text_content", out textContent);
        }
        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var question = model as QuestionItemModel;

            questionListViewItem.QuestionModel = question.QuestionModel;
            questionListViewItem.QuestionModel.ItemIndexOSA = model.id;
            questionListViewItem.QuestionModel.ViewsHolder = baseVH as QuestionItemViewsHolder;

            textContent.text = question.QuestionModel.Content;

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
