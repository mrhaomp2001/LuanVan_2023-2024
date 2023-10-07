using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    [System.Serializable]
    public class UIProfileModel
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private string username;
        [SerializeField] private string createdAt;
        [SerializeField] private string updatedAt;
        [SerializeField] private string friendStatusToOther;
        [SerializeField] private string friendStatusToSelf;
        [SerializeField] private string avatarPath;

        [SerializeField] private int itemIndexOSA;
        [SerializeField] private ProfileItemViewsHolder viewsHolder;

        public int ItemIndexOSA { get => itemIndexOSA; set => itemIndexOSA = value; }
        public ProfileItemViewsHolder ViewsHolder { get => viewsHolder; set => viewsHolder = value; }
        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Username { get => username; set => username = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }
        public string UpdatedAt { get => updatedAt; set => updatedAt = value; }
        public string FriendStatusToOther { get => friendStatusToOther; set => friendStatusToOther = value; }
        public string FriendStatusToSelf { get => friendStatusToSelf; set => friendStatusToSelf = value; }
        public string AvatarPath { get => avatarPath; set => avatarPath = value; }
    }
}
