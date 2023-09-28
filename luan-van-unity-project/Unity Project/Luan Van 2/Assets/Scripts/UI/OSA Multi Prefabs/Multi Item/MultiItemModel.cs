using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuanVan.OSA
{
    public class MultiItemModel : BaseModel
    {
        [SerializeField] private UIMultiModel multiModel = new UIMultiModel();

        public UIMultiModel MultiModel { get => multiModel; set => multiModel = value; }
    }
}
