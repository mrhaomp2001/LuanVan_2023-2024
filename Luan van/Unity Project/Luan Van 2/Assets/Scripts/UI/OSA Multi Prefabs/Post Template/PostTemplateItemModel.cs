using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LuanVan.OSA
{
    public class PostTemplateItemModel : BaseModel
    {
        [SerializeField] private UIPostTemplateModel postTemplateModel;

        public UIPostTemplateModel PostTemplateModel { get => postTemplateModel; set => postTemplateModel = value; }
    }
}
