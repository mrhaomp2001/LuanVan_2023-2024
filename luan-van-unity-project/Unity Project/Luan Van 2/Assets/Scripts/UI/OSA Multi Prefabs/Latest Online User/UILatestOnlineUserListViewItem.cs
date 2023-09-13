using Com.TheFallenGames.OSA.Demos.DifferentPrefabPerOrientation.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class UILatestOnlineUserListViewItem : MonoBehaviour
    {
        [SerializeField] private Redirector redirector;
        [SerializeField] private ProfileController profileController;
        [SerializeField] private UILatestOnlineUserModel latestOnlineUserModel;

        public UILatestOnlineUserModel LatestOnlineUserModel { get => latestOnlineUserModel; set => latestOnlineUserModel = value; }

        public void ShowProfile()
        {
            redirector.Push("profile");
            profileController.GetUserProfile(latestOnlineUserModel.Id);
        }
    }
}
