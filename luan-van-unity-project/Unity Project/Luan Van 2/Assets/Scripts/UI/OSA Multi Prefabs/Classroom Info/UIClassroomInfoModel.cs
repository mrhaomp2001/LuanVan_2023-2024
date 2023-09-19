using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIClassroomInfoModel
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string description;
        [SerializeField] private string createdAt;
        [SerializeField] private string avatarPath;
        [SerializeField] private string themeColor;
        [SerializeField] private string studyStatus;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private ClassroomInfoItemViewsHolder viewsHolder;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string AvatarPath { get => avatarPath; set => avatarPath = value; }
        public string ThemeColor { get => themeColor; set => themeColor = value; }
        public string StudyStatus { get => studyStatus; set => studyStatus = value; }
        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public ClassroomInfoItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
    }
}
