using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UIClassroomInfoListViewItem : MonoBehaviour
    {
        [SerializeField] private Image imageAvatar;
        [SerializeField] private Sprite spriteDefaultAvatar;
        [SerializeField] private ClassroomController classroomController;
        [SerializeField] private StudyDocumentController studyDocumentController;
        [SerializeField] private MainGameplayController mainGameplayController;

        [SerializeField] private UIClassroomInfoModel classroomInfoModel;
        public UIClassroomInfoModel ClassroomInfoModel { get => classroomInfoModel; set => classroomInfoModel = value; }

        public void UpdateStudyClassroomStatus(string status)
        {
            classroomController.UpdateStudyClassroomStatus(status, classroomInfoModel);

        }

        public void ShowDocuments()
        {
            studyDocumentController.GetClassroomDocuments(classroomInfoModel.Id);
        }

        public void CheckAndDownloadAvatar()
        {
            if (!classroomInfoModel.AvatarPath.Equals(""))
            {
                Davinci.get().load(GlobalSetting.Endpoint + classroomInfoModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
            }
            else
            {
                imageAvatar.sprite = spriteDefaultAvatar;
            }
        }
        public void ShowQuestionCollections()
        {
            mainGameplayController.ShowQuestionCollections();
        }

        public void GetClassroomRanks(string type)
        {
            classroomController.GetClassroomRanks(type);
        }
    }
}
