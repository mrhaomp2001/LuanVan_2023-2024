using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using frame8.Logic.Misc.Other.Extensions;
using System;
using TMPro;

namespace LuanVan.OSA
{

    public class AnswerItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public TextMeshProUGUI textContent;
        public Image imageAnswerOutline;
        public UIAnswerListViewItem answerListViewItem;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(AnswerItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            answerListViewItem = root.GetComponent<UIAnswerListViewItem>();
            root.GetComponentAtPath("img_backround/img_button_simple_outline", out imageAnswerOutline);
            root.GetComponentAtPath("layout_listview_item_content/text_content", out textContent);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var answer = model as AnswerItemModel;

            answerListViewItem.AnswerModel = answer.AnswerModel;
            answerListViewItem.AnswerModel.ItemIndexOSA = model.id;
            answerListViewItem.AnswerModel.ViewsHolder = baseVH as AnswerItemViewsHolder;

            textContent.text = answer.AnswerModel.Content;

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
