using LuanVan.OSA;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class UICommentListViewItem : MonoBehaviour
    {
        [SerializeField] private UICommentModel commentModel;

        public UICommentModel CommentModel { get => commentModel; set => commentModel = value; }
    }
}
