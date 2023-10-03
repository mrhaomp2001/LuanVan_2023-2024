using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace LuanVan.OSA
{
    public class QuestionCollectionItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIQuestionCollectionListViewItem questionCollectionListViewItem;
        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(QuestionCollectionItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            questionCollectionListViewItem = root.GetComponent<UIQuestionCollectionListViewItem>();
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var questionCollection = model as QuestionCollectionItemModel;

            questionCollectionListViewItem.QuestionCollectionModel = questionCollection.QuestionCollectionModel;
            questionCollectionListViewItem.QuestionCollectionModel.ViewsHolder = baseVH as QuestionCollectionItemViewsHolder;

            questionCollectionListViewItem.UpdateViews();
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
