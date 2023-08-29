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
    public class ClassroomItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;

        public UIClassroomListViewItem classroomListViewItem;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(ClassroomItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            classroomListViewItem = root.GetComponent<UIClassroomListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/text_classroom_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/text_classroom_description", out textDescription);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var classroom = model as ClassroomItemModel;

            classroomListViewItem.ClassroomModel = classroom.ClassroomModel;
            classroomListViewItem.ClassroomModel.ItemIndexOSA = model.id;
            classroomListViewItem.ClassroomModel.ViewsHolder = baseVH as ClassroomItemViewsHolder;

            textName.text = classroom.ClassroomModel.Name;
            textDescription.text = classroom.ClassroomModel.Description;

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
