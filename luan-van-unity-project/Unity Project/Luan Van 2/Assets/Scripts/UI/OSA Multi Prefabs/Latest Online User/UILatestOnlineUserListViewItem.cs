using Com.TheFallenGames.OSA.Demos.DifferentPrefabPerOrientation.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LuanVan.OSA
{

    public class UILatestOnlineUserListViewItem : MonoBehaviour
    {
        [SerializeField] private Sprite spriteDefaultAvatar;
        [Header("UIs: ")]
        [SerializeField] private Image imageAvatar;
        [SerializeField] private Redirector redirector;
        [SerializeField] private ProfileController profileController;
        [SerializeField] private OtherUserController otherUserController;
        [SerializeField] private UILatestOnlineUserModel latestOnlineUserModel;

        public UILatestOnlineUserModel LatestOnlineUserModel { get => latestOnlineUserModel; set => latestOnlineUserModel = value; }

        public void ShowProfile()
        {
            redirector.Push("profile");
            profileController.GetUserProfile(latestOnlineUserModel.Id);
        }

        public void UpdateFriendStatus(string status)
        {
            otherUserController.UpdateFriendStatus(latestOnlineUserModel.Id, status);
        }

        public void CheckAndDownloadAvatar()
        {
            if (latestOnlineUserModel.AvatarPath != null)
            {
                if (!latestOnlineUserModel.AvatarPath.Equals(""))
                {
                    Davinci.get().load(GlobalSetting.Endpoint + latestOnlineUserModel.AvatarPath).into(imageAvatar).setFadeTime(0).start();
                }
                else
                {
                    imageAvatar.sprite = spriteDefaultAvatar;
                }
            }

        }
    }
}
