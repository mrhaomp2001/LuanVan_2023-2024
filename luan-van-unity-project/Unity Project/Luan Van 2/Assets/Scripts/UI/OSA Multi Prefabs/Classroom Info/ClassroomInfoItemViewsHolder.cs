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
    public class ClassroomInfoItemViewsHolder : BaseVH
    {
        public ContentSizeFitter contentSizeFitter;
        public UIClassroomInfoListViewItem classroomInfoListViewItem;

        public TextMeshProUGUI textName;
        public TextMeshProUGUI textDescription;
        public RectTransform containerClassroomRegister;
        public RectTransform containerClassroomUtilities;

        public override bool CanPresentModelType(Type modelType)
        {
            return modelType == typeof(ClassroomInfoItemModel);
        }

        public override void CollectViews()
        {
            base.CollectViews();
            contentSizeFitter = root.GetComponent<ContentSizeFitter>();
            classroomInfoListViewItem = root.GetComponent<UIClassroomInfoListViewItem>();

            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_name", out textName);
            root.GetComponentAtPath("layout_listview_item_content/layout_header/text_description", out textDescription);

            root.GetComponentAtPath("layout_listview_item_content/layout_classroom_register", out containerClassroomRegister);
            root.GetComponentAtPath("layout_listview_item_content/layout_classroom_utilities", out containerClassroomUtilities);
        }

        public override void UpdateViews(BaseModel model, BaseVH baseVH)
        {
            base.UpdateViews(model, baseVH);

            var classroomInfo = model as ClassroomInfoItemModel;

            classroomInfoListViewItem.ClassroomInfoModel = classroomInfo.ClassroomInfoModel;
            classroomInfoListViewItem.ClassroomInfoModel.ViewsHolder = baseVH as ClassroomInfoItemViewsHolder;

            textName.text = classroomInfoListViewItem.ClassroomInfoModel.Name;
            textDescription.text = classroomInfoListViewItem.ClassroomInfoModel.Description;

            if (!classroomInfoListViewItem.ClassroomInfoModel.StudyStatus.Equals("2"))
            {
                containerClassroomRegister.gameObject.SetActive(true);
                containerClassroomUtilities.gameObject.SetActive(false);
            }
            else
            {
                containerClassroomRegister.gameObject.SetActive(false);
                containerClassroomUtilities.gameObject.SetActive(true);
            }

            classroomInfoListViewItem.CheckAndDownloadAvatar();

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
