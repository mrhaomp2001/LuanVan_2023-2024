using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{

    public class MessageItemModel : BaseModel
    {
        [SerializeField] private UIMessageModel messageModel;

        public UIMessageModel MessageModel { get => messageModel; set => messageModel = value; }
    }
}
