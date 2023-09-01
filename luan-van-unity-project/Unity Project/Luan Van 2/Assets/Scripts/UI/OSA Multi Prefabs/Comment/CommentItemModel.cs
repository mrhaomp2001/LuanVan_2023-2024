using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class CommentItemModel : BaseModel
    {
        [SerializeField] private UICommentModel commentModel;

        public UICommentModel CommentModel { get => commentModel; set => commentModel = value; }
    }
}
