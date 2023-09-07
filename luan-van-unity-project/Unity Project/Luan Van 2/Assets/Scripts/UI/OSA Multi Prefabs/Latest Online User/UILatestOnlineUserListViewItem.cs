using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class UILatestOnlineUserListViewItem : MonoBehaviour
    {
        [SerializeField] private UILatestOnlineUserModel latestOnlineUserModel;

        public UILatestOnlineUserModel LatestOnlineUserModel { get => latestOnlineUserModel; set => latestOnlineUserModel = value; }
    }
}
