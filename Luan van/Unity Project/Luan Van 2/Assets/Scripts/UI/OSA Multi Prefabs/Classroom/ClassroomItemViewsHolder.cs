using frame8.Logic.Misc.Other.Extensions;
using Library;
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
        public Image imageBackground;
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
            root.GetComponentAtPath("img_backround", out imageBackground);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var classroom = model as ClassroomItemModel;

            classroomListViewItem.ClassroomModel = classroom.ClassroomModel;
            classroomListViewItem.ClassroomModel.ItemIndexOSA = model.id;
            classroomListViewItem.ClassroomModel.ViewsHolder = baseVH as ClassroomItemViewsHolder;

            classroomListViewItem.CheckAndDownloadAvatar();

            Color themeColor = Color.white;

            if (ColorUtility.TryParseHtmlString(classroomListViewItem.ClassroomModel.ThemeColor, out themeColor))
            {

            }

            textName.text = classroom.ClassroomModel.Name;
            textDescription.text = classroom.ClassroomModel.Description;
            imageBackground.color = themeColor;

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
