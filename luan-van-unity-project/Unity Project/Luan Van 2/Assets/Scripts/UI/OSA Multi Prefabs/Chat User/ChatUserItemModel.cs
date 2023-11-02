using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class ChatUserItemModel : BaseModel
    {
        [SerializeField] private UIChatUserModel chatUserModel;

        public UIChatUserModel ChatUserModel { get => chatUserModel; set => chatUserModel = value; }
    }
}
