using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class UIPostListViewItem : MonoBehaviour
    {
        [SerializeField] private UIPostModel postModel;
        public UIPostModel PostModel { get => postModel; set => postModel = value; }
    }
}
